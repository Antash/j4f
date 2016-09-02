using System;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Diagnostics;

namespace PGM
{
    class Program
    {
        private static readonly CultureInfo UsCulture = new CultureInfo("en-US");
        private static readonly string[] PhotoExt = { "jpg", "jpeg", "jpe", "png", "bmp" };
        private static readonly string[] VideoExt = { "avi", "mp4", "flv", "mov", "mkv", "3gp", "mpg", "m4v", "mp4", "swf", "wmv", "vob"};
        private static readonly TraceSource logger = new TraceSource("TraceSourceApp");

        private static bool collisionActionToAll = false;

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
                Console.WriteLine("Enter image ignore threshold (by default all files are processed):");
                var input = Console.ReadLine();
                int limit = 0;
                int.TryParse(input, out limit);
                Console.WriteLine("Input something if you want to process video files (by default photo files are processed):");
                input = Console.ReadLine();
                bool video = !string.IsNullOrEmpty(input);
                var dr = MessageBox.Show("Keep source files?", "Move or Copy", MessageBoxButtons.YesNo);
                Console.WriteLine(String.Format("Keep source images = {0}", dr == DialogResult.Yes));
                ImageProc(d, dd, dr == DialogResult.Yes, limit, video ? VideoExt : PhotoExt);
            }
            else
            {
                Console.WriteLine("Restart and choose correct folders!");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private enum DefaulAction
        {
            Copy, Skip
        }

        private static void ImageProc(DirectoryInfo d, DirectoryInfo ddest, bool saveOriginal, int sizeLimit, string[] extensions)
        {
            try
            {
                foreach (var tf in extensions.SelectMany(e => d.EnumerateFiles(string.Format("*.{0}", e))))
                {
                    logger.TraceInformation(string.Format("Processing image {0}", tf.FullName));
                    DateTime dateOfShot = DateTime.MinValue;
                    using (FileStream foto = File.Open(tf.FullName, FileMode.Open, FileAccess.Read))
                    {
                        if (sizeLimit > 0 && foto.Length < sizeLimit)
                        {
                            logger.TraceInformation(string.Format("Skipping image {0}: too small.", tf.FullName));
                            continue;
                        }
                        try
                        {
                            var decoder = BitmapDecoder.Create(foto, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);
                            var imageMetadata = decoder.Frames[0].Metadata;
                            if (imageMetadata != null)
                            {
                                var tmpImgExif = (BitmapMetadata)imageMetadata.Clone();
                                dateOfShot = Convert.ToDateTime(tmpImgExif.DateTaken);
                            }
                        }
                        catch
                        {
                            // Do nothing
                            logger.TraceEvent(TraceEventType.Error, 1, string.Format("Image metadata cannot be fetched: {0}", tf.FullName));
                        }
                    }
                    // if there is no chance to read shot taken date, or if it is simply missing use LastWriteDate instead
                    if (dateOfShot == DateTime.MinValue)
                    {
                        dateOfShot = new FileInfo(tf.FullName).LastWriteTime;
                    }
                    var dy = Path.Combine(ddest.FullName, dateOfShot.Year.ToString(CultureInfo.InvariantCulture));
                    if (!Directory.Exists(dy))
                    {
                        Directory.CreateDirectory(dy);
                    }
                    var dmd = Path.Combine(dy, string.Format(UsCulture, "{0:MM}_{0:MMM, d}", dateOfShot));
                    if (!Directory.Exists(dmd))
                    {
                        Directory.CreateDirectory(dmd);
                    }
                    var nname = Path.Combine(dmd, string.Format("{0:yyyy-mm-dd_hhmmss}{1}", dateOfShot, Path.GetExtension(tf.FullName)));

                    if (File.Exists(nname))
                    {
                        if (!collisionActionToAll)
                        {
                            var dr = MessageBox.Show("Copy this file anyway?", "File with the same name exists!",
                                                     MessageBoxButtons.YesNo);
                            var drmemo = MessageBox.Show("Repeat to all?", "Save selected option",
                                 MessageBoxButtons.YesNo);
                            collisionActionToAll = drmemo == DialogResult.Yes;

                            if (dr != DialogResult.Yes)
                            {
                                continue;
                            }
                        }
                        nname = ChooseName(nname, dmd, dateOfShot);
                    }

                    try
                    {
                        logger.TraceInformation(string.Format("Copying image {0} to {1}", tf.Name, nname));
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
                        logger.TraceEvent(TraceEventType.Error, 1, string.Format("Error during operation: {0};\r\n{1}", e.Message, e.StackTrace));
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                logger.TraceEvent(TraceEventType.Error, 1, string.Format("Access denied! {0}", e.Message));
            }
            try
            {
                foreach (var td in d.EnumerateDirectories())
                {
                    ImageProc(td, ddest, saveOriginal, sizeLimit, extensions);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                logger.TraceEvent(TraceEventType.Error, 1, string.Format("Access denied! {0}", e.Message));
            }
        }

        private static string ChooseName(string fileName, string baseDir, DateTime dateOfShot)
        {
            string newName = fileName;
            int i = 1;
            while (File.Exists(newName))
            {
                newName = Path.Combine(baseDir, string.Format("{0:yyyy-mm-dd_hhmmss}({1}){2}", dateOfShot, i++, Path.GetExtension(fileName)));
            }
            return newName;
        }
    }
}
