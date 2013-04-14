using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace SearcherCore
{
	public class FileSearcher
	{
		private readonly SearchType _type;
		private readonly PluginManager _pluginManager;
		private readonly IFileProcessor _proc;

		public FileSearcher()
		{
			_type = SearchType.File;
			_pluginManager = new PluginManager();
		}

		public FileSearcher(SearchType type)
			: this()
		{
			_type = type;
			_proc = _pluginManager.GetProcessor(_type);
		}

		public void Search(string root, string pattern)
		{
			DirectoryInfo rootDir;
			try
			{
				rootDir = new DirectoryInfo(root);
			}
			catch (Exception)
			{
				throw;
			}
			Search(rootDir, pattern);
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
				var directories = from dir in root.EnumerateDirectories() select dir;
				var files = listFunc(root, pattern);
				
				foreach (var file in files)
				{
					var fileName = file.FullName;
					if (_proc != null && _proc.IsSuitable(fileName, pattern))
						Console.WriteLine("File found: {0}", fileName);
				}
				foreach (var dir in directories)
				{
					SearchInternal(dir, pattern, listFunc);
				}
			}
			catch (UnauthorizedAccessException)
			{

			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
