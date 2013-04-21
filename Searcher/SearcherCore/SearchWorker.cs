using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SearcherCore
{
	public class FileSearchParam
	{
		public override string ToString()
		{
			return string.Format("Searching '{0}' in '{1}' {2}",
				SearchPattern,
				string.IsNullOrEmpty(RootDir) ? "everyware" : RootDir,
				string.IsNullOrWhiteSpace(PlugName) ? string.Empty : string.Format("using {0}", PlugName));
		}

		public string PlugName { get; set; }
		public string RootDir { get; set; }
		public string SearchPattern { get; set; }
		public bool IgnoreCase { get; set; }
		public DateTime? CreationTimeFrom { get; set; }
		public DateTime? CreationTimeTo { get; set; }
		public long? SizeFrom { get; set; }
		public long? SizeTo { get; set; }
	}

	public class SearchWorker : INotifyPropertyChanged
	{
		public int Id { get; set; }
		public FileSearchParam Parameter { get; set; }

		private int _filesFound;
		public int FilesFound
		{
			get { return _filesFound; }
			set
			{
				if (value != _filesFound)
				{
					_filesFound = value;
					OnPropertyChanged();
				}
			}
		}

		private string _status;
		public string Status
		{
			get { return _status; }
			set
			{
				if (value != _status)
				{
					_status = value;
					OnPropertyChanged();
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
