using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace SearcherCore
{
	public enum SearchType
	{
		File,
		XmlTag,
		DotNetType
	}

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

		public void Search(string pattern)
		{
			var drives = DriveInfo.GetDrives();
			foreach (var drive in drives)
			{
				Search(drive.RootDirectory.FullName, pattern);
			}
		}

		public void Search(DriveType driveType, string pattern)
		{
			var drives = DriveInfo.GetDrives().Where(d => d.DriveType == driveType);
			foreach (var drive in drives)
			{
				Search(drive.RootDirectory.FullName, pattern);
			}
		}

		public void Search(string root, string pattern)
		{
			if (_proc == null)
			{
				// search ignoring extention if any specified
				if (!pattern.Contains('.'))
					pattern += ".*";
				SearchInternal(root, pattern);
			}
			else
			{
				SearchInternal(root, pattern);
			}
		}

		private void SearchInternal(string root, string pattern)
		{
			try
			{
				var directories = from dir in Directory.EnumerateDirectories(root) select dir;
				var files = from file in Directory.EnumerateFiles(root, pattern) select file;
				foreach (var file in files)
				{

					//TODO process found files
					Console.WriteLine("File found: {0}", file);
				}
				foreach (var dir in directories)
				{
					SearchInternal(dir, pattern);
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
