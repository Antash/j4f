using System;

namespace SearcherCore
{
	public class FileFoundArgs : EventArgs
	{
		internal int SearcherId { get; set; }
		internal string FileName { get; set; }
	}

	public delegate void OnFileFoundDelegate(object sender, FileFoundArgs e);

	public class FileSearchParam
	{
		public override string ToString()
		{
			return string.Format("Searching '{0}' in '{1}' using {2}",
				SearchPattern,
				string.IsNullOrEmpty(RootDir) ? "everyware" : RootDir,
				PlugName);
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
}
