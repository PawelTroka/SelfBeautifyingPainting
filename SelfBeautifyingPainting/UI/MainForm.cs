using System;
using System.Linq;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using SelfBeautifyingPainting.Painting;
using SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings;
using SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ColorProbabilityMode;
using SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ColorsMarkovMode;
using SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ShapeMode;

namespace SelfBeautifyingPainting.UI
{
    using SelfBeautifyingPainting.Detection;

    using WpfFaceDetectionTest;

    public partial class MainForm : Form
    {
        private Painting.SelfBeautifyingPaintings.SelfBeautifyingPainting _selfBeautifyingPainting;
        private PaintingMode mode;

        private SmileDetection smileDetection;

        private SmileDetectionControl smileDetectionControl;

        private long counter=0;

        public MainForm()
        {
            InitializeComponent();
            InitModes();
           // InitFullscreen();
            //InitVideo();
            InitPainting();

            //timer1.Start();
            // videoSourcePlayer1.NewFrame += VideoSourcePlayer1_NewFrame;
            smileDetectionControl = new SmileDetectionControl();
            elementHost1.Child = smileDetectionControl;

        }

        private void VideoSourcePlayer1_NewFrame(object sender, ref System.Drawing.Bitmap frame)
        {
        //    counter++;

          //  if (counter > 10)
            {
                smileDetection.Detect(ref frame);
              //  counter = 0;
               
            }
            //return;
        }

        private void InitModes()
        {
            mode = PaintingMode.ColorsWithProbability;

            toolStripComboBox1.Items.AddRange(Enum.GetValues(typeof (PaintingMode)).Cast<object>().ToArray());
            toolStripComboBox1.SelectedItem = mode;
        }

        private void InitPainting()
        {
            var workingArea = Screen.GetWorkingArea(this);

            switch (mode)
            {
                case PaintingMode.GoogleTopicsImages:
                    _selfBeautifyingPainting = new GoogleTopicsSelfBeautifyingPainting(workingArea.Width,
                        workingArea.Height);
                    break;
                case PaintingMode.ColorsWithProbability:
                    _selfBeautifyingPainting = new ColorsWithProbabilitySelfBeautifyingPainting(workingArea.Width,
                        workingArea.Height);
                    break;
                case PaintingMode.ColorsWithMarkovModel:
                    _selfBeautifyingPainting = new ColorsMarkovModelSelfBeautifyingPainting(workingArea.Width,
                        workingArea.Height);
                    break;
                case PaintingMode.Shapes:
                    _selfBeautifyingPainting = new ShapeSelfBeautifyingPainting(workingArea.Width,
                        workingArea.Height);
                    break;

                case PaintingMode.Colors:
                case PaintingMode.GoogleImagesRelated:


                default:
                    throw new ArgumentOutOfRangeException();
            }


            pictureBox1.Image = _selfBeautifyingPainting.Painting;

            _selfBeautifyingPainting.ImageChanged += (o, e) => pictureBox1.Invalidate();

            smileDetection = new SmileDetection();
        }


        private void InitFullscreen()
        {
            MinimumSize = Screen.PrimaryScreen.Bounds.Size;
            MaximumSize = Screen.PrimaryScreen.Bounds.Size;

            ///////////////////////////////// this.TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            //this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            MaximizeBox = false;

            // Set the MinimizeBox to false to remove the minimize box.
            MinimizeBox = false;

            // Set the start position of the form to the center of the screen.
            //this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitVideo()
        {
            var filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            
            var videoSource = new VideoCaptureDevice(filterInfoCollection[0].MonikerString);
            videoSource.VideoResolution = videoSource.VideoCapabilities[4];
          //  this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            videoSourcePlayer1.VideoSource = videoSource;
            //vide
            videoSourcePlayer1.Start();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoSourcePlayer1.Stop();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var me = (MouseEventArgs) e;
            var coordinates = me.Location;

            _selfBeautifyingPainting.ReviewPainting(coordinates.X, coordinates.Y, me.Button == MouseButtons.Left);
            //left button means we liked it
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mode == (PaintingMode) toolStripComboBox1.SelectedItem) return;
            mode = (PaintingMode) toolStripComboBox1.SelectedItem;
            InitPainting();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        //    var frame = videoSourcePlayer1.GetCurrentVideoFrame();
            
            
           // smileDetection.Detect(frame);

            //detectionPictureBox.Invalidate();
        }
    }
}