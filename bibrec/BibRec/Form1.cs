//----------------------------------------------------------------------------
//  Copyright (C) 2004-2012 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;

using System.Diagnostics;
using Emgu.CV.OCR;
using FaceDetection;

namespace LicensePlateRecognition
{
	public partial class LicensePlateRecognitionForm : Form
	{
		private LicensePlateDetector _licensePlateDetector;

		public LicensePlateRecognitionForm()
		{
			InitializeComponent();
			_licensePlateDetector = new LicensePlateDetector("");

			ProcessImage(new Image<Bgr, byte>(@"..\..\sample.jpg"));
			//ProcessImage(new Image<Bgr, byte>(@"..\..\s1.jpg"));
		}

		List<Rectangle> DF(Image<Bgr, byte> image)
		{
			long detectionTime;
			List<Rectangle> faces = new List<Rectangle>();
			List<Rectangle> eyes = new List<Rectangle>();
			DetectFace.Detect(image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml", faces, eyes, out detectionTime);
			return faces;

			//foreach (Rectangle eye in eyes)
			//	image.Draw(eye, new Bgr(Color.Blue), 2);

			//display the image 
			//ImageViewer.Show(image, String.Format(
			//   "Completed face and eye detection using {0} in {1} milliseconds",
			//   GpuInvoke.HasCuda ? "GPU" : "CPU",
			//   detectionTime));
		}

		private void ProcessImage(Image<Bgr, byte> image)
		{
			Stopwatch watch = Stopwatch.StartNew(); // time the detection process

			List<Image<Gray, Byte>> licensePlateImagesList = new List<Image<Gray, byte>>();
			List<Image<Gray, Byte>> filteredLicensePlateImagesList = new List<Image<Gray, byte>>();
			List<MCvBox2D> licenseBoxList = new List<MCvBox2D>();
			//List<string> words = _licensePlateDetector.DetectLicensePlate(
			//   image,
			//   licensePlateImagesList,
			//   filteredLicensePlateImagesList,
			//   licenseBoxList);

			var _ocr = new Tesseract(@".\tessdata\", "eng", 
				Tesseract.OcrEngineMode.OEM_DEFAULT, "1234567890");

			List<String> licenses = new List<String>();
			using (Image<Gray, byte> gray = image.Convert<Gray, Byte>())
			//using (Image<Gray, byte> gray = GetWhitePixelMask(img))
			using (Image<Gray, Byte> canny = new Image<Gray, byte>(gray.Size))
			using (MemStorage stor = new MemStorage())
			{
				//CvInvoke.cvCanny(gray, canny, 100, 100, 3);
				CvInvoke.cvThreshold(gray, canny, 75, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY);

				var bibs = new List<Image<Gray, Byte>>();
				var bibsreg = new List<Rectangle>();
				//imageBox1.Image = gray;
				var faces = DF(image);

				Point startPoint = new Point(10, 10);
				this.Text = "";
				foreach (Rectangle face in faces)
				{
					canny.Draw(face, new Gray(1), 2);

					
					var r = new Rectangle(face.X - face.Width/2, face.Y + face.Height * 2,
						face.Width * 2, face.Height * 3);

					
					bibsreg.Add(r);

					CvInvoke.cvSetImageROI(canny, r);

					var ni = canny.Copy();

					_ocr.Recognize(ni);

					var words = _ocr.GetCharactors();

					CvInvoke.cvResetImageROI(canny);
					this.Text += " bib:" + _ocr.GetText().Replace(" ", string.Empty);
					//AddLabelAndImage(ref startPoint, _ocr.GetText(), ni);
				}

				foreach (Rectangle r in bibsreg)
					canny.Draw(r, new Gray(1), 10);

				imageBox1.Image = canny;
				//_ocr.Recognize(canny);
				//var words = _ocr.GetCharactors();
				//this.Text = _ocr.GetText();

				//Contour<Point> contours = canny.FindContours(
				//	 Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
				//	 Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_TREE,
				//	 stor);
				//FindLicensePlate(contours, gray, canny, licensePlateImagesList, filteredLicensePlateImagesList, detectedLicensePlateRegionList, licenses);
			}


			watch.Stop(); //stop the timer
			processTimeLabel.Text = String.Format("License Plate Recognition time: {0} milli-seconds", watch.Elapsed.TotalMilliseconds);

			//panel1.Controls.Clear();
			//Point startPoint = new Point(10, 10);
			//for (int i = 0; i < words.Count; i++)
			//{
			//	AddLabelAndImage(
			//	   ref startPoint,
			//	   String.Format("License: {0}", words[i]),
			//	   licensePlateImagesList[i].ConcateVertical(filteredLicensePlateImagesList[i]));
			//	image.Draw(licenseBoxList[i], new Bgr(Color.Red), 2);
			//}

			//imageBox1.Image = image;
		}

		private void AddLabelAndImage(ref Point startPoint, String labelText, IImage image)
		{
			Label label = new Label();
			panel1.Controls.Add(label);
			label.Text = labelText;
			label.Width = 100;
			label.Height = 30;
			label.Location = startPoint;
			startPoint.Y += label.Height;

			ImageBox box = new ImageBox();
			panel1.Controls.Add(box);
			//box.ClientSize = image.Size;
			//box.Image = image;
			box.Location = startPoint;
			startPoint.Y += 15;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DialogResult result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				Image<Bgr, Byte> img;
				try
				{
					img = new Image<Bgr, byte>(openFileDialog1.FileName);
				}
				catch
				{
					MessageBox.Show(String.Format("Invalide File: {0}", openFileDialog1.FileName));
					return;
				}

				ProcessImage(img);
			}
		}
	}

}