using System;
using System.Linq;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using SelfBeautifyingPainting.Painting;

namespace SelfBeautifyingPainting.UI
{
    public partial class MainForm : Form
    {
        private Painting.SelfBeautifyingPainting _selfBeautifyingPainting;
        private PaintingMode mode;

        public MainForm()
        {
            InitializeComponent();
            InitModes();
            InitFullscreen();
            InitVideo();
            InitPainting();
        }

        private void InitModes()
        {
            mode = PaintingMode.ColorsWithMarkovModel;

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
                case PaintingMode.Colors:
                case PaintingMode.GoogleImagesRelated:

                default:
                    throw new ArgumentOutOfRangeException();
            }


            pictureBox1.Image = _selfBeautifyingPainting.Painting;

            _selfBeautifyingPainting.ImageChanged += (o, e) => pictureBox1.Invalidate();
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

            videoSourcePlayer1.VideoSource = videoSource;
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
    }
}