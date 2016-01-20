using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfBeautifyingPainting.Detection
{
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    using Accord.Vision.Detection;

    using log4net.Appender;

    using SelfBeautifyingPainting.Properties;

    class SmileDetection
    {
        private HaarCascade faceCascade;
        private HaarCascade mouthCascade;

        private HaarObjectDetector faceDetector;
        private HaarObjectDetector mouthDetector;

        private PictureBox detectionPictureBox;

        private Bitmap detectionImage;

        private int width, height;

        public SmileDetection()
        {
            //this.detectionPictureBox = pictureBox;

     

           // this.ClearDetectionImage();

            faceCascade = HaarCascade.FromXml(this.GenerateStreamFromString(Resources.FaceCascade));
            mouthCascade = HaarCascade.FromXml(this.GenerateStreamFromString(Resources.MouthCascade));

            faceDetector = new HaarObjectDetector(faceCascade);
            mouthDetector = new HaarObjectDetector(mouthCascade);
        }

        

        public void Detect(ref Bitmap image)
        {



            //var grayFrame = currentFrame.Convert<Gray, byte>();

            // var detectedFaces = this.faceCascade.Detect(grayFrame);

            try
            {
                faceDetector.ProcessFrame(image);
            }
            catch (Exception)
            {
                return;
            }

            if (faceDetector.DetectedObjects.Length == 0) return;

            var face = this.faceDetector.DetectedObjects[0];


            foreach (var f in faceDetector.DetectedObjects)
            {
                if ((f.Width * f.Height) > face.Width * face.Height)
                {
                    face = f;
                }
            }

           // this.ClearDetectionImage();
            Graphics g = Graphics.FromImage(image);

            g.DrawRectangle(new Pen(Color.Black,3),face);


            var mouthArea = new Rectangle(
                face.X,
                face.Y + (face.Height / 2),
                face.Width,
                face.Height / 2);

            var faceBitmap = new Bitmap(face.Width, face.Height/2);

            for (int i = 0; i < face.Width; i++)
            {
                for (int j = 0; j < face.Height / 2; j++)
                {
                    faceBitmap.SetPixel(i,j, image.GetPixel(i + face.X, face.Y));
                }
            }


            mouthDetector.ProcessFrame(faceBitmap);

            if (mouthDetector.DetectedObjects.Length == 0) return;

            var mouth = this.mouthDetector.DetectedObjects[0];


            foreach (var f in mouthDetector.DetectedObjects)
            {
                if ((f.Width * f.Height) > mouth.Width * mouth.Height)
                {
                    mouth = f;
                }
            }

            mouth.Y += face.Y;
            mouth.X += face.X;

            g.DrawRectangle(new Pen(Color.Yellow, 3), mouth);

            // this.detectionPictureBox.Image = image;
            //// Find actual face


            //currentFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);




            //grayFrame.ROI = mouthArea;
            ////var noses = this.noseCascade.Detect(grayFrame);
            ////foreach (var nose in noses)
            ////{
            ////    nose.rect.Offset(face.rect.X, face.rect.Y);

            ////    currentFrame.Draw(nose.rect, new Bgr(Color.Blue), 2);
            ////}

            //var mouthsDetected = this.mouthCascade.Detect(grayFrame);

            //MCvAvgComp mouth;
            //try
            //{
            //    mouth = mouthsDetected[0];
            //}
            //catch (IndexOutOfRangeException)
            //{
            //    return;
            //}

            //foreach (var m in mouthsDetected)
            //{
            //    if ((m.rect.Width * m.rect.Height) > mouth.rect.Width * mouth.rect.Height)
            //    {
            //        mouth = m;
            //    }
            //}

            //Rectangle mouthRect = mouth.rect;
            //mouthRect.Offset(face.rect.X, face.rect.Y + (face.rect.Height / 2));


            //var faceField = face.rect.Width * face.rect.Height;
            //var mouthField = mouthRect.Width * mouthRect.Height;

            //if (this.lastRect != null)
            //{
            //    var diffField = (double)mouthField / (this.lastRect.Height * this.lastRect.Width);
            //    var diffY = (double)mouthRect.Y / this.lastRect.Y;
            //    //Debug.WriteLine((double)mouthField / faceField);
            //    if (((double)mouthField / faceField) > 0.09)
            //    {
            //        //if (this.lastSmile != DateTime.MaxValue)
            //        //{
            //        //    var waitTime = this.lastSmile + TimeSpan.FromSeconds(1);
            //        //    if (DateTime.Now > waitTime)
            //        //    {
            //        //        Debug.Write($"SMILE {(double)mouthField / faceField} {DateTime.Now}\n");
            //        //        currentFrame.Draw(mouthRect, new Bgr(Color.Green), 2);
            //        //        this.lastSmile = DateTime.Now;
            //        //    }
            //        //}
            //        //else
            //        //{
            //        Debug.Write($"SMILE {(double)mouthField / faceField} {DateTime.Now}\n");
            //        currentFrame.Draw(mouthRect, new Bgr(Color.Green), 2);
            //        //  this.lastSmile = DateTime.Now;
            //        // }

            //    }
            //    else
            //    {
            //        currentFrame.Draw(mouthRect, new Bgr(Color.Red), 2);
            //    }

            //    //  Debug.WriteLine($"{ diffField} {diffY}");
            //    ////  Debug.WriteLine(diff);
            //    //  if (diffField > 1)
            //    //  {
            //    //      if (diffY > 1.1)
            //    //      {
            //    //          Debug.Write("SMILE\n");
            //    //      }

            //    //  }

            //}
            //this.lastRect = mouthRect;




            ////var mouthsDetected = grayFrame.DetectHaarCascade(this.smileCascade,
            ////    1.1, 10,
            ////    Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
            ////    new System.Drawing.Size(20, 20));
            ////grayFrame.ROI = Rectangle.Empty;


            ////var smiles = smileCascade.Detect(grayFrame);
            ////foreach (var smile in smiles)
            ////{
            ////    smile.rect.Offset(face.rect.X, face.rect.Y);
            ////    currentFrame.Draw(smile.rect, new Bgr(0, double.MaxValue, 0), 3);
            ////}




            ////var g = currentFrame.Copy(f.rect);
            ////var faceFrame = g.Convert<Gray, Byte>();

            ////var smiles = smileCascade.Detect(faceFrame);
            ////foreach (var smile in smiles)
            ////{
            ////    smile.rect.Offset(f.rect.X, f.rect.Y);
            ////    currentFrame.Draw(smile.rect, new Bgr(0, double.MaxValue, 0), 3);
            ////}


            //this.image1.Source = ToBitmapSource(currentFrame);
            //image.

        }



        private Stream GenerateStreamFromString(string text)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
