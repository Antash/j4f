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
		private SearchType _type;
		private readonly PluginManager _pluginManager;

		public FileSearcher()
		{
			_type = SearchType.File;
			_pluginManager = new PluginManager();
		}

		public FileSearcher(SearchType type)
			: this()
		{
			_type = type;
			var p = _pluginManager.GetProcessor(_type);
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
			// search ignoring extention if any specified
			if (!pattern.Contains('.'))
				pattern += ".*";
			SearchInternal(root, pattern);
		}

		private void SearchInternal(string root, string pattern)
		{
			var files = from file in Directory.EnumerateFiles(root, pattern) select file;
			var directories = from dir in Directory.EnumerateDirectories(root) select dir;

			try
			{
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
