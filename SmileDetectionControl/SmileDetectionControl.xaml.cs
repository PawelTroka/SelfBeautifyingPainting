using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfFaceDetectionTest
{
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Threading;

    using Emgu.CV;
    using Emgu.CV.Structure;

    /// <summary>
    /// Interaction logic for SmileDetectionControl.xaml
    /// </summary>
    public partial class SmileDetectionControl : UserControl
    {
        private Capture capture;
        private HaarCascade mouthCascade;

        private HaarCascade faceCascade;

        private HaarCascade noseCascade;

        private DateTime lastSmile = DateTime.MaxValue;

        private Rectangle lastRect;
        DispatcherTimer timer;

        public SmileDetectionControl()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            capture = new Capture();
            mouthCascade = new HaarCascade(@"C:\Users\Jakub\Desktop\haarcascade_smile.xml");
            this.faceCascade = new HaarCascade(@"C:\Users\Jakub\Desktop\haarcascade_frontalface_default.xml");
            this.noseCascade = new HaarCascade(@"C:\Users\Jakub\Desktop\haarcascade_mcs_nose.xml");

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {



            var currentFrame = capture.QueryFrame();

            if (currentFrame == null)
            {
                return;
            }

            var grayFrame = currentFrame.Convert<Gray, byte>();

            var detectedFaces = this.faceCascade.Detect(grayFrame);

            MCvAvgComp face;
            try
            {
                face = detectedFaces[0];
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }

            // Find actual face
            foreach (var f in detectedFaces)
            {
                if ((f.rect.Width * f.rect.Height) > face.rect.Width * face.rect.Height)
                {
                    face = f;
                }
            }

            currentFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);



            var mouthArea = new Rectangle(
                face.rect.X,
                face.rect.Y + (face.rect.Height / 2),
                face.rect.Width,
                face.rect.Height / 2);

            grayFrame.ROI = mouthArea;
            //var noses = this.noseCascade.Detect(grayFrame);
            //foreach (var nose in noses)
            //{
            //    nose.rect.Offset(face.rect.X, face.rect.Y);

            //    currentFrame.Draw(nose.rect, new Bgr(Color.Blue), 2);
            //}

            var mouthsDetected = this.mouthCascade.Detect(grayFrame);

            MCvAvgComp mouth;
            try
            {
                mouth = mouthsDetected[0];
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }

            foreach (var m in mouthsDetected)
            {
                if ((m.rect.Width * m.rect.Height) > mouth.rect.Width * mouth.rect.Height)
                {
                    mouth = m;
                }
            }

            Rectangle mouthRect = mouth.rect;
            mouthRect.Offset(face.rect.X, face.rect.Y + (face.rect.Height / 2));


            var faceField = face.rect.Width * face.rect.Height;
            var mouthField = mouthRect.Width * mouthRect.Height;

            if (this.lastRect != null)
            {
                var diffField = (double)mouthField / (this.lastRect.Height * this.lastRect.Width);
                var diffY = (double)mouthRect.Y / this.lastRect.Y;
                //Debug.WriteLine((double)mouthField / faceField);
                if (((double)mouthField / faceField) > 0.09)
                {
                    //if (this.lastSmile != DateTime.MaxValue)
                    //{
                    //    var waitTime = this.lastSmile + TimeSpan.FromSeconds(1);
                    //    if (DateTime.Now > waitTime)
                    //    {
                    //        Debug.Write($"SMILE {(double)mouthField / faceField} {DateTime.Now}\n");
                    //        currentFrame.Draw(mouthRect, new Bgr(Color.Green), 2);
                    //        this.lastSmile = DateTime.Now;
                    //    }
                    //}
                    //else
                    //{
                    Debug.Write($"SMILE {(double)mouthField / faceField} {DateTime.Now}\n");
                    currentFrame.Draw(mouthRect, new Bgr(Color.Green), 2);
                    //  this.lastSmile = DateTime.Now;
                    // }

                }
                else
                {
                    currentFrame.Draw(mouthRect, new Bgr(Color.Red), 2);
                }

                //  Debug.WriteLine($"{ diffField} {diffY}");
                ////  Debug.WriteLine(diff);
                //  if (diffField > 1)
                //  {
                //      if (diffY > 1.1)
                //      {
                //          Debug.Write("SMILE\n");
                //      }

                //  }

            }
            this.lastRect = mouthRect;




            //var mouthsDetected = grayFrame.DetectHaarCascade(this.smileCascade,
            //    1.1, 10,
            //    Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
            //    new System.Drawing.Size(20, 20));
            //grayFrame.ROI = Rectangle.Empty;


            //var smiles = smileCascade.Detect(grayFrame);
            //foreach (var smile in smiles)
            //{
            //    smile.rect.Offset(face.rect.X, face.rect.Y);
            //    currentFrame.Draw(smile.rect, new Bgr(0, double.MaxValue, 0), 3);
            //}




            //var g = currentFrame.Copy(f.rect);
            //var faceFrame = g.Convert<Gray, Byte>();

            //var smiles = smileCascade.Detect(faceFrame);
            //foreach (var smile in smiles)
            //{
            //    smile.rect.Offset(f.rect.X, f.rect.Y);
            //    currentFrame.Draw(smile.rect, new Bgr(0, double.MaxValue, 0), 3);
            //}


            this.image1.Source = ToBitmapSource(currentFrame);
        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        public static BitmapSource ToBitmapSource(IImage image)
        {
            using (Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }
    }
}
