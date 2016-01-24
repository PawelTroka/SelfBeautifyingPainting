

namespace WpfFaceDetectionTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Threading;

    using Emgu.CV;
    using Emgu.CV.Structure;

    using global::SmileDetectionControl;

    /// <summary>
    /// Interaction logic for SmileDetectionControl.xaml
    /// </summary>
    public partial class SmileDetectionControl
    {
        #region Fields

        /// <summary>
        /// The capture.
        /// </summary>
        private Capture capture;

        /// <summary>
        /// The mouth cascade.
        /// </summary>
        private HaarCascade mouthCascade;

        /// <summary>
        /// The face cascade.
        /// </summary>
        private HaarCascade faceCascade;

        /// <summary>
        /// The statistics.
        /// </summary>
        private DetectionStatistics statistics = new DetectionStatistics();

        #endregion

        public SmileDetectionControl()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            capture = new Capture();
            mouthCascade = new HaarCascade(@"C:\Users\Jakub\Desktop\haarcascade_smile.xml");
            this.faceCascade = new HaarCascade(@"C:\Users\Jakub\Desktop\haarcascade_frontalface_default.xml");
        }

        /// <summary>
        /// The detect smile.
        /// </summary>
        public void DetectSmile()
        {
            var currentFrame = this.capture.QueryFrame();
            try
            {
                var face = this.GetFace(currentFrame);
                currentFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);

                var mouth = this.GetMouth(currentFrame, face);

                if (((double)(mouth.rect.Height * mouth.rect.Width) / (face.rect.Height * face.rect.Width))
                    > this.statistics.Treshold)
                {
                    this.DrawMouth(currentFrame, face, mouth, Color.Green);
                }
                else
                {
                    this.DrawMouth(currentFrame, face, mouth, Color.Red);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.image1.Source = ToBitmapSource(currentFrame);
            }

        }

        private MCvAvgComp GetFace(Image<Bgr,byte> frame)
        {
            
            var grayFrame = frame.Convert<Gray, byte>();
            var detectedFaces = this.faceCascade.Detect(grayFrame);

            var face = detectedFaces[0];

            foreach (var possibleFace in detectedFaces)
            {
                if ((possibleFace.rect.Width * possibleFace.rect.Height) > face.rect.Width * face.rect.Height)
                {
                    face = possibleFace;
                }
            }

            return face;
        }

        private MCvAvgComp GetMouth(Image<Bgr, byte> frame, MCvAvgComp face)
        {
            var grayFrame = frame.Convert<Gray, byte>();

            var mouthArea = new Rectangle(
              face.rect.X,
              face.rect.Y + (face.rect.Height / 2),
              face.rect.Width,
              face.rect.Height / 2);

            grayFrame.ROI = mouthArea;

            var mouthsDetected = this.mouthCascade.Detect(grayFrame);

            var mouth = mouthsDetected[0];

            foreach (var possibleMouth in mouthsDetected)
            {
                if ((possibleMouth.rect.Width * possibleMouth.rect.Height) > mouth.rect.Width * mouth.rect.Height)
                {
                    mouth = possibleMouth;
                }
            }

            return mouth;
        }

        private void DrawMouth(Image<Bgr, byte> frame,MCvAvgComp face, MCvAvgComp mouth, Color color)
        {
            var mouthRect = mouth.rect;
            mouthRect.Offset(face.rect.X, face.rect.Y + (face.rect.Height / 2));
            frame.Draw(mouthRect, new Bgr(color), 2);
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
                    BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }

        public void RecordNoSmile()
        {
            var currentFrame = this.capture.QueryFrame();
            try
            {
                var face = this.GetFace(currentFrame);
                currentFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);

                var mouth = this.GetMouth(currentFrame, face);
                this.DrawMouth(currentFrame, face, mouth, Color.Yellow);

                this.statistics.noSmileStates.Add((double)(mouth.rect.Height * mouth.rect.Width) / (face.rect.Height * face.rect.Width));
                this.image1.Source = ToBitmapSource(currentFrame);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void RecordSmile()
        {
            var currentFrame = this.capture.QueryFrame();
            try
            {
                var face = this.GetFace(currentFrame);
                currentFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);

                var mouth = this.GetMouth(currentFrame, face);
                this.DrawMouth(currentFrame, face, mouth, Color.Yellow);

                this.statistics.smileStates.Add((double)(mouth.rect.Height * mouth.rect.Width) / (face.rect.Height * face.rect.Width));
                this.image1.Source = ToBitmapSource(currentFrame);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void SaveNoSmileStatistics()
        {
            this.statistics.SaveNoSmileStatistics();
        }

        public void SaveSmileStatistics()
        {
            this.statistics.SaveSmileStatistics();
        }

        public void UpdateTreshold(int value)
        {
            this.statistics.tresholdPercent = value;
        }
    }
}
