using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace PGM
{
	class Program
	{
		static CultureInfo c = new CultureInfo("en-US");
		static string ext = "jpg";

		static void Main(string[] args)
		{
			DirectoryInfo d = new DirectoryInfo(@"D:\Photo_collection");
			//DirectoryInfo dd = new DirectoryInfo(@"D:\Photo_collection_new");
			DirectoryInfo dd = new DirectoryInfo(@"D:\photo");
			proc(d, dd);
		}

		static void proc(DirectoryInfo d, DirectoryInfo ddest)
		{
			foreach (var tf in d.EnumerateFiles(String.Format("*.{0}", ext)))
			{
				DateTime DateOfShot;
				using (FileStream Foto = File.Open(tf.FullName, FileMode.Open, FileAccess.Read)) // открыли файл по адресу s для чтения
				{
					BitmapDecoder decoder = JpegBitmapDecoder.Create(Foto, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default); //"распаковали" снимок и создали объект decoder
					BitmapMetadata TmpImgEXIF = (BitmapMetadata)decoder.Frames[0].Metadata.Clone(); //считали и сохранили метаданные
					DateOfShot = Convert.ToDateTime(TmpImgEXIF.DateTaken);
				}
				if (DateOfShot == DateTime.MinValue)
				{
					DateOfShot = new FileInfo(tf.FullName).LastWriteTime;
				}
				var dy = Path.Combine(ddest.FullName, DateOfShot.Year.ToString());
				if (!Directory.Exists(dy))
				{
					Directory.CreateDirectory(dy);
				}
				var dmd = Path.Combine(dy, String.Format(c, "{0:MM}_{0:MMM, d}", DateOfShot));
				if (!Directory.Exists(dmd))
				{
					Directory.CreateDirectory(dmd);
				}
				var nname = Path.Combine(dmd, String.Format("{0:yyyy-mm-dd_hhmmss}.{1}", DateOfShot, ext));
				int i = 1;
				while (File.Exists(nname))
				{
					nname = Path.Combine(dmd, String.Format("{0:yyyy-mm-dd_hhmmss}({1}).{2}", DateOfShot, i++, ext));
				}
				File.Move(tf.FullName, nname);
			}
			foreach (var td in d.EnumerateDirectories())
			{
				proc(td, ddest);
			}
		}
	}
}
