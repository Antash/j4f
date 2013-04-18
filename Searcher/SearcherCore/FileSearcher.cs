using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SearcherExtensibility;
using System.Diagnostics;

namespace SearcherCore
{
	internal class FileFoundArgs : EventArgs
	{
		internal string FileName { get; set; }
	}

	internal class FileSearcher
	{
		private readonly IFileProcessor _proc;

		private readonly HashSet<string> _visitedPaths;
		private readonly HashSet<string> _foundFiles;

		private CancellationToken _ct;

		internal FileSearcher(CancellationToken ct)
		{
			_ct = ct;
			_visitedPaths = new HashSet<string>();
			_foundFiles = new HashSet<string>();
		}

		internal FileSearcher(CancellationToken ct, IFileProcessor proc)
			: this(ct)
		{
			_proc = proc;
		}

		#region File found event declaration

		internal delegate void OnFileFoundDelegate(object sender, FileFoundArgs e);
		internal event OnFileFoundDelegate OnFileFound;

		private void FileFound(string fileName)
		{
			if (OnFileFound != null)
			{
				OnFileFound(this, new FileFoundArgs { FileName = fileName });
			}
		}

		#endregion

		internal void Search(SearchManager.FileSearchParam param)
		{
			if (string.IsNullOrEmpty(param.RootDir))
			{
				var drives = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed);
				foreach (var drive in drives)
				{
					Search(drive.RootDirectory, param.SearchPattern);
				}
			}
			else
			{
				Search(new DirectoryInfo(param.RootDir), param.SearchPattern);
			}
		}

		private void Search(DirectoryInfo root, string pattern)
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
			if (_ct.IsCancellationRequested)
			{
				Debug.WriteLine("Search canseled");
				_ct.ThrowIfCancellationRequested();
			}
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
				Debug.WriteLine("{0} : {1}", root.FullName, ex);
			}
		}
	}
}
