using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SearcherExtensibility;
using System.Diagnostics;

namespace SearcherCore
{
	internal class FileSearcher
	{
		private readonly IFileProcessor _proc;

		private readonly HashSet<string> _visitedPaths;
		private readonly HashSet<string> _foundFiles;

		private CancellationToken _ct;

		internal FileSearcher(int id, CancellationToken ct)
		{
			Id = id;
			_ct = ct;
			_visitedPaths = new HashSet<string>();
			_foundFiles = new HashSet<string>();
		}

		internal FileSearcher(int id, CancellationToken ct, IFileProcessor proc)
			: this(id, ct)
		{
			_proc = proc;
		}

		internal int Id { get; private set; }

		#region event declaration

		internal event OnFileFoundDelegate OnFileFound;

		private void FileFound(string fileName)
		{
			OnFileFoundDelegate handler = OnFileFound;
			if (handler != null)
			{
				handler(this, new FileFoundArgs
				{ 
					FileName = fileName,
					SearcherId = Id
				});
			}
		}

		internal delegate void OnSearchStartedDelegate(object sender, EventArgs e);
		internal event OnSearchStartedDelegate OnSearchStarted;

		private void SearchStarted()
		{
			OnSearchStartedDelegate handler = OnSearchStarted;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}

		#endregion

		internal void Search(FileSearchParam param)
		{
			SearchStarted();
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
				// search for any if pattern empty
				if (string.IsNullOrWhiteSpace(pattern))
					pattern += "*";
				// search ignoring extention if any specified
				if (!pattern.Contains('.'))
					pattern += ".*";
				SearchInternal(root, pattern, ListFiles);
			}
			else if (_proc.Init(pattern))
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

		private void StopCheck()
		{
			if (_ct.IsCancellationRequested)
			{
				Debug.WriteLine("Search canseled");
				_ct.ThrowIfCancellationRequested();
			}
		}

		private void SearchInternal(DirectoryInfo root, string pattern, Func<DirectoryInfo, string, IEnumerable<FileInfo>> listFunc)
		{
			StopCheck();
			try
			{
				foreach (var file in listFunc(root, pattern))
				{
					StopCheck();
					// Suppose short filename and creation timestamp concztenation is unique
					var fileStamp = file.Name + file.CreationTime.Ticks;
					if ((_proc == null || _proc.ProcessFile(file.FullName)) &&
						!_foundFiles.Contains(fileStamp))
					{
						_foundFiles.Add(fileStamp);
						FileFound(file.FullName);
						Console.WriteLine("File found: {0}", file.FullName);
					}
				}
				foreach (var dir in root.EnumerateDirectories())
				{
					StopCheck();
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
