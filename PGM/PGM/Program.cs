using System;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace PGM
{
    class Program
	{
        static readonly CultureInfo UsCulture = new CultureInfo("en-US");
        private const string Ext = "jpg";

        [STAThread]
		static void Main(string[] args)
		{
            DirectoryInfo d = null, dd = null;
            var inp = new FolderBrowserDialog
                          {
                              Description = "Choose folder with images needed to be import"
                          };
            if (inp.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(String.Format("Source directory selected: \r\n\t{0}", inp.SelectedPath));
                d = new DirectoryInfo(inp.SelectedPath);
            }
            var outp = new FolderBrowserDialog
                           {
                               Description = "Choose target directory"
                           };
            if (outp.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(String.Format("Destenation directory selected: \r\n\t{0}", outp.SelectedPath));
                dd = new DirectoryInfo(outp.SelectedPath);
            }
            if (dd != null && d != null)
            {
                DialogResult dr = MessageBox.Show("Move or Copy", "Keep source files?", MessageBoxButtons.YesNo);
                Console.WriteLine(String.Format("Keep source images = {0}", dr == DialogResult.Yes));
                ImageProc(d, dd, dr == DialogResult.Yes);
            }
            else
            {
                Console.WriteLine("Restart and choose correct folders!");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
		}

        private static void ImageProc(DirectoryInfo d, DirectoryInfo ddest, bool saveOriginal)
        {
            foreach (var tf in d.EnumerateFiles(String.Format("*.{0}", Ext)))
            {
                Console.WriteLine(String.Format("Processing image {0}", tf.Name));
                DateTime dateOfShot = DateTime.MinValue;
                using (FileStream foto = File.Open(tf.FullName, FileMode.Open, FileAccess.Read)) // открыли файл по адресу s для чтения
                {
                    var decoder = BitmapDecoder.Create(foto, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default); //"распаковали" снимок и создали объект decoder
                    var imageMetadata = decoder.Frames[0].Metadata;
                    if (imageMetadata != null)
                    {
                        var tmpImgExif = (BitmapMetadata)imageMetadata.Clone(); //считали и сохранили метаданные
                        dateOfShot = Convert.ToDateTime(tmpImgExif.DateTaken);
                    }
                }
                if (dateOfShot == DateTime.MinValue)
                {
                    dateOfShot = new FileInfo(tf.FullName).LastWriteTime;
                }
                var dy = Path.Combine(ddest.FullName, dateOfShot.Year.ToString(CultureInfo.InvariantCulture));
                if (!Directory.Exists(dy))
                {
                    Directory.CreateDirectory(dy);
                }
                var dmd = Path.Combine(dy, String.Format(UsCulture, "{0:MM}_{0:MMM, d}", dateOfShot));
                if (!Directory.Exists(dmd))
                {
                    Directory.CreateDirectory(dmd);
                }
                var nname = Path.Combine(dmd, String.Format("{0:yyyy-mm-dd_hhmmss}.{1}", dateOfShot, Ext));
                int i = 1;
                while (File.Exists(nname))
                {
                    nname = Path.Combine(dmd, String.Format("{0:yyyy-mm-dd_hhmmss}({1}).{2}", dateOfShot, i++, Ext));
                }
                try
                {
                    Console.WriteLine(String.Format("Copying image {0} to {1}", tf.Name, nname));
                    if (saveOriginal)
                    {
                        File.Copy(tf.FullName, nname);
                    }
                    else
                    {
                        File.Move(tf.FullName, nname);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(String.Format("Error during operation: {0};\r\n{1}", e.Message, e.StackTrace));
                }
            }
            foreach (var td in d.EnumerateDirectories())
            {
                ImageProc(td, ddest, saveOriginal);
            }
        }
	}
}
