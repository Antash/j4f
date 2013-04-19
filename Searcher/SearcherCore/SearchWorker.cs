using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SearcherCore
{
	public class SearchWorker : INotifyPropertyChanged
	{
		public int Id { get; set; }
		public string Parameter { get; set; }

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
