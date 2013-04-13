using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace SearcherCore
{
    public class FileSearcher : IFileProcessor
    {
	    public SearcherType GetSearcherType()
	    {
		    return SearcherType.FileSearcher;
	    }

	    public void Search()
	    {
		    var drives = DriveInfo.GetDrives();
		    foreach (var drive in drives)
		    {
				var files =
				from file in Directory.EnumerateFiles(drive.RootDirectory.FullName, "*.jpg", SearchOption.AllDirectories)
			    select file;
							foreach (var file in files) ;
		    }

		    //   ListBox.Items.Add(file);
		    //TextBlock.Text = "Найдено файлов: " + files.Count<string>().ToString();
	    }
    }
}
