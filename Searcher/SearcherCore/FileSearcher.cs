﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SearcherExtensibility;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SearcherCore
{
	public class FileFoundArgs : EventArgs
	{
		public string FileName { get; set; }
	}

	public class FileSearcher
	{
		private static readonly PluginManager _pluginManager = new PluginManager();
		private readonly IFileProcessor _proc;
		private readonly PluginType _type;

		private readonly HashSet<string> _visitedPaths;
		private readonly HashSet<string> _foundFiles;

		public FileSearcher()
		{
			_visitedPaths = new HashSet<string>();
			_foundFiles = new HashSet<string>();
		}

		public FileSearcher(PluginType type)
			: base()
		{
			_type = type;
			_proc = _pluginManager.GetProcessor(type);
		}

		public static int LoadPlugins(string path)
		{
			return _pluginManager.LoadPlugins(path);
		}

		public static string[] GetPluginList()
		{
			return _pluginManager.GetPluginList().Select(p => p.ToString()).ToArray();
		}

		#region File found event declaration

		public delegate void OnFileFoundDelegate(object sender, FileFoundArgs e);
		public event OnFileFoundDelegate OnFileFound;

		public void FileFound(string fileName)
		{
			if (OnFileFound != null)
			{
				OnFileFound(this, new FileFoundArgs() { FileName = fileName });
			}
		}

		#endregion

		#region Search public overrides

		public void Search(string root, string pattern)
		{
			Search(new DirectoryInfo(root), pattern);
		}

		public void Search(string pattern)
		{
			var drives = DriveInfo.GetDrives();
			foreach (var drive in drives)
			{
				Search(drive.RootDirectory, pattern);
			}
		}

		public void Search(DriveType driveType, string pattern)
		{
			var drives = DriveInfo.GetDrives().Where(d => d.DriveType == driveType);
			foreach (var drive in drives)
			{
				Search(drive.RootDirectory, pattern);
			}
		}

		public void Search(DirectoryInfo root, string pattern)
		{
			if (_proc == null)
			{
				// search ignoring extention if any specified
				if (!pattern.Contains('.'))
					pattern += ".*";
				SearchInternal(root, pattern, ListFiles);
			}
			else
			{
				SearchInternal(root, pattern, ListFilesForPlugin);
			}
		}

		#endregion

		private IEnumerable<FileInfo> ListFilesForPlugin(DirectoryInfo root, string pattern)
		{
			return _proc.FileExtentionPatterns.Any() ?
				_proc.FileExtentionPatterns.SelectMany(root.EnumerateFiles) :
				root.EnumerateFiles();
		}

		private IEnumerable<FileInfo> ListFiles(DirectoryInfo root, string pattern)
		{
			return root.EnumerateFiles(pattern);
		}

		private void SearchInternal(DirectoryInfo root, string pattern, Func<DirectoryInfo, string, IEnumerable<FileInfo>> listFunc)
		{
			try
			{
				foreach (var file in listFunc(root, pattern))
				{
					// Suppose short filename and creation timestamp concztenation is unique
					var fileStamp = file.Name + file.CreationTime.Ticks;
					if ((_proc == null || _proc.IsSuitable(file.FullName, pattern)) &&
						!_foundFiles.Contains(fileStamp))
					{
						_foundFiles.Add(fileStamp);
						FileFound(file.FullName);
						Console.WriteLine("File found: {0}", file.FullName);
					}
				}
				foreach (var dir in root.EnumerateDirectories())
				{
					// Suppose short filename and creation timestamp concztenation is unique
					var dirStamp = dir.Name + dir.CreationTime.Ticks;
					if (!_visitedPaths.Contains(dirStamp))
					{
						// Prevent step into rephase points more than once
						if ((File.GetAttributes(dir.FullName) & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
							_visitedPaths.Add(dirStamp);

						SearchInternal(dir, pattern, listFunc);
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				Debug.WriteLine("Insufficient privileges: {0}", root.FullName);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("{0} : {1}", root.FullName, ex.ToString());
			}
		}
	}
}
