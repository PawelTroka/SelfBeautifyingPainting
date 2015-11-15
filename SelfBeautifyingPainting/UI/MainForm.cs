using System.Drawing;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using SelfBeautifyingPainting.Painting;

namespace SelfBeautifyingPainting.UI
{
    public partial class MainForm : Form
    {
        private Painting.SelfBeautifyingPainting selfBeautifyingPainting;
        public MainForm()
        {
            InitializeComponent();

            InitFullscreen();
            InitVideo();
            InitPainting();
        }

        private void InitPainting()
        {
            var workingArea = Screen.GetWorkingArea(this);

            selfBeautifyingPainting = new Painting.SelfBeautifyingPainting(workingArea.Width,workingArea.Height) {Mode = PaintingMode.GoogleTopicsImages};

            this.pictureBox1.Image = selfBeautifyingPainting.Painting;

            selfBeautifyingPainting.ImageChanged += (o, e) => pictureBox1.Invalidate();
        }


        private void InitFullscreen()
        {
            MinimumSize = Screen.PrimaryScreen.Bounds.Size;
            MaximumSize = Screen.PrimaryScreen.Bounds.Size;

            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;

            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;

            // Set the start position of the form to the center of the screen.
            //this.StartPosition = FormStartPosition.CenterScreen;


        }

        private void InitVideo()
        {
            var filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            VideoCaptureDevice videoSource = new VideoCaptureDevice(filterInfoCollection[0].MonikerString);

            videoSourcePlayer1.VideoSource = videoSource;
            videoSourcePlayer1.Start();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoSourcePlayer1.Stop();
            
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;

            selfBeautifyingPainting.ReviewPainting(coordinates.X, coordinates.Y, me.Button == MouseButtons.Left);//left button means we liked it
        }
    }
}
