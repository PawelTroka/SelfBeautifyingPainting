namespace WpfFaceDetectionTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media.Imaging;

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

        /// <summary>
        /// The smile occurences.
        /// </summary>
        private List<bool> smileOccurences = new List<bool>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SmileDetectionControl"/> class.
        /// </summary>
        public SmileDetectionControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The to bitmap source.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The <see cref="BitmapSource"/>.
        /// </returns>
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
                    this.smileOccurences.Add(true);
                    this.DrawMouth(currentFrame, face, mouth, Color.Green);
                }
                else
                {
                    this.smileOccurences.Add(false);
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

        /// <summary>
        /// The record no smile.
        /// </summary>
        public void RecordNoSmile()
        {
            var currentFrame = this.capture.QueryFrame();
            try
            {
                var face = this.GetFace(currentFrame);
                currentFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);

                var mouth = this.GetMouth(currentFrame, face);
                this.DrawMouth(currentFrame, face, mouth, Color.Yellow);

                this.statistics.NoSmileStates.Add((double)(mouth.rect.Height * mouth.rect.Width) / (face.rect.Height * face.rect.Width));
                this.image1.Source = ToBitmapSource(currentFrame);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// The record smile.
        /// </summary>
        public void RecordSmile()
        {
            var currentFrame = this.capture.QueryFrame();
            try
            {
                var face = this.GetFace(currentFrame);
                currentFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);

                var mouth = this.GetMouth(currentFrame, face);
                this.DrawMouth(currentFrame, face, mouth, Color.Yellow);

                this.statistics.SmileStates.Add((double)(mouth.rect.Height * mouth.rect.Width) / (face.rect.Height * face.rect.Width));
                this.image1.Source = ToBitmapSource(currentFrame);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// The save no smile statistics.
        /// </summary>
        public void SaveNoSmileStatistics()
        {
            this.statistics.SaveNoSmileStatistics();
        }

        /// <summary>
        /// The save smile statistics.
        /// </summary>
        public void SaveSmileStatistics()
        {
            this.statistics.SaveSmileStatistics();
        }

        /// <summary>
        /// The update treshold.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void UpdateTreshold(int value)
        {
            this.statistics.ThresholdPercent = value;
        }

        /// <summary>
        /// The get like result.
        /// </summary>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double GetLikeResult()
        {
            var likes = this.smileOccurences.Count(x => x);
            return (double)likes / this.smileOccurences.Count;
        }

        /// <summary>
        /// The clear review results.
        /// </summary>
        public void ClearReviewResults()
        {
            this.smileOccurences = new List<bool>();
        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        /// <summary>
        /// The get mouth.
        /// </summary>
        /// <param name="frame">
        /// The frame.
        /// </param>
        /// <param name="face">
        /// The face.
        /// </param>
        /// <returns>
        /// The <see cref="MCvAvgComp"/>.
        /// </returns>
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

        /// <summary>
        /// The get face.
        /// </summary>
        /// <param name="frame">
        /// The frame.
        /// </param>
        /// <returns>
        /// The <see cref="MCvAvgComp"/>.
        /// </returns>
        private MCvAvgComp GetFace(Image<Bgr, byte> frame)
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

        /// <summary>
        /// The draw mouth.
        /// </summary>
        /// <param name="frame">
        /// The frame.
        /// </param>
        /// <param name="face">
        /// The face.
        /// </param>
        /// <param name="mouth">
        /// The mouth.
        /// </param>
        /// <param name="color">
        /// The color.
        /// </param>
        private void DrawMouth(Image<Bgr, byte> frame, MCvAvgComp face, MCvAvgComp mouth, Color color)
        {
            var mouthRect = mouth.rect;
            mouthRect.Offset(face.rect.X, face.rect.Y + (face.rect.Height / 2));
            frame.Draw(mouthRect, new Bgr(color), 2);
        }

        /// <summary>
        /// The window_ loaded.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.capture = new Capture();
            this.mouthCascade = new HaarCascade(@"C:\Users\Jakub\Desktop\haarcascade_smile.xml");
            this.faceCascade = new HaarCascade(@"C:\Users\Jakub\Desktop\haarcascade_frontalface_default.xml");
        }
    }
}
