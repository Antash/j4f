using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SearcherExtensibility;

namespace SearcherCore
{
	public class FileSearcher
	{
		private static readonly PluginManager PluginManager = new PluginManager();
		private readonly IFileProcessor _proc;
		private readonly HashSet<string> _visitedPaths;

		public FileSearcher()
		{
			_visitedPaths = new HashSet<string>();
		}

		public FileSearcher(SearchType type)
		{
			_proc = PluginManager.GetProcessor(type);
		}

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
					//TODO process found file
					var fileName = file.FullName;
					if (_proc == null || _proc.IsSuitable(fileName, pattern))
						Console.WriteLine("File found: {0}", fileName);
				}
				foreach (var dir in root.EnumerateDirectories())
				{
					var dirStamp = dir.Name + dir.CreationTime;
					if (!_visitedPaths.Contains(dirStamp))
					{
						if ((File.GetAttributes(dir.FullName) & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
							_visitedPaths.Add(dirStamp);

						SearchInternal(dir, pattern, listFunc);
					}
				}
			}
			catch (UnauthorizedAccessException ex)
			{
			//	Console.WriteLine("Insufficient privileges: {0}", ex.ToString());
			}
			catch (Exception ex)
			{
			//	Console.WriteLine(ex.ToString());
			}
		}
	}
}
