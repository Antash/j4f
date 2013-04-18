using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
		private readonly ManualResetEvent _eventLocker;

		private readonly HashSet<string> _visitedPaths;
		private readonly HashSet<string> _foundFiles;

		internal FileSearcher()
		{
			_visitedPaths = new HashSet<string>();
			_foundFiles = new HashSet<string>();
			_eventLocker = new ManualResetEvent(true);
		}

		internal FileSearcher(IFileProcessor proc)
			: this()
		{
			_proc = proc;
		}

		internal void PauseSearch()
		{
			_eventLocker.Reset();
		}

		internal void ResumeSearch()
		{
			_eventLocker.Set();
		}

		#region File found event declaration

		internal delegate void OnFileFoundDelegate(object sender, FileFoundArgs e);
		internal event OnFileFoundDelegate OnFileFound;

		internal void FileFound(string fileName)
		{
			if (OnFileFound != null)
			{
				OnFileFound(this, new FileFoundArgs { FileName = fileName });
			}
		}

		#endregion

		#region Search overrides

		internal Task Search(SearchManager.FileSearchParam param)
		{
			Search(param.RootDir, param.SearchPattern);
			return null;
		}

		private void Search(string root, string pattern)
		{
			Search(new DirectoryInfo(root), pattern);
		}

		private void Search(string pattern)
		{
			var drives = DriveInfo.GetDrives();
			foreach (var drive in drives)
			{
				Search(drive.RootDirectory, pattern);
			}
		}

		private void Search(DriveType driveType, string pattern)
		{
			var drives = DriveInfo.GetDrives().Where(d => d.DriveType == driveType);
			foreach (var drive in drives)
			{
				Search(drive.RootDirectory, pattern);
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
				// Wait if thread paused
				_eventLocker.WaitOne();
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
