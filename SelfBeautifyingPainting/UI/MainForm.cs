namespace SelfBeautifyingPainting.UI
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using System.Windows.Threading;

    using SelfBeautifyingPainting.Painting;
    using SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings;
    using SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ColorProbabilityMode;
    using SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ColorsMarkovMode;
    using SelfBeautifyingPainting.Painting.SelfBeautifyingPaintings.ShapeMode;
    using WpfFaceDetectionTest;

    public partial class MainForm : Form
    {
        #region Syf

        private SelfBeautifyingPainting _selfBeautifyingPainting;
        private PaintingMode mode;

        private SmileDetectionControl smileDetectionControl;

        private readonly DispatcherTimer ProcessFrameTimer =new DispatcherTimer();

        private readonly DispatcherTimer RecordNoSmileTimer = new DispatcherTimer();

        private readonly DispatcherTimer RecordSmileTimer = new DispatcherTimer();

        private readonly DispatcherTimer EndNoSmileTimer = new DispatcherTimer();

        private readonly DispatcherTimer EndSmileTimer = new DispatcherTimer();

        private long counter=0;

        public MainForm()
        {
            this.InitializeComponent();
            this.InitModes();
            this.InitTimers();
           
            //InitFullscreen();
            //InitPainting();
            smileDetectionControl = new SmileDetectionControl();
            elementHost1.Child = smileDetectionControl;
            this.trackBar1.Value = 50;

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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var me = (MouseEventArgs)e;
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
        #endregion

        #region CameraActions

        /// <summary>
        /// The init timers.
        /// </summary>
        private void InitTimers()
        {
            this.ProcessFrameTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            this.ProcessFrameTimer.Tick += (sender, args) => this.smileDetectionControl.DetectSmile();
            this.ProcessFrameTimer.Start();

            this.RecordNoSmileTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            this.RecordNoSmileTimer.Tick += (s, args) => this.smileDetectionControl.RecordNoSmile();

            this.RecordSmileTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            this.RecordSmileTimer.Tick += (s, args) => this.smileDetectionControl.RecordSmile();

            this.EndSmileTimer.Interval = new TimeSpan(0, 0, 0, 5);
            this.EndSmileTimer.Tick += (s, args) => this.StopSmileRecording();

            this.EndNoSmileTimer.Interval = new TimeSpan(0, 0, 0, 5);
            this.EndNoSmileTimer.Tick += (s, args) => this.StopNoSmileRecording();
        }

        /// <summary>
        /// The no smile button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void StartNoSmileRecording(object sender, EventArgs e)
        {
            this.ProcessFrameTimer.Stop();
            this.RecordNoSmileTimer.Start();
            
            this.EndNoSmileTimer.Start();
        }

        /// <summary>
        /// The start smile recording.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void StartSmileRecording(object sender, EventArgs e)
        {
            this.ProcessFrameTimer.Stop();
            this.RecordSmileTimer.Start();
            this.EndSmileTimer.Start();
        }

        /// <summary>
        /// The stop no smile recording.
        /// </summary>
        private void StopNoSmileRecording()
        {
            this.EndNoSmileTimer.Stop();
            this.RecordNoSmileTimer.Stop();
            this.smileDetectionControl.SaveNoSmileStatistics();
            this.ProcessFrameTimer.Start();
        }

        /// <summary>
        /// The stop smile recording.
        /// </summary>
        private void StopSmileRecording()
        {
            this.EndSmileTimer.Stop();
            this.RecordSmileTimer.Stop();
            this.smileDetectionControl.SaveSmileStatistics();
            this.ProcessFrameTimer.Start();
        }

        #endregion

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.smileDetectionControl.UpdateTreshold(this.trackBar1.Value);
        }

        /// <summary>
        /// The stop slef beutifying painting.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void StopPaint(object sender, EventArgs e)
        {
            this.ProcessFrameTimer.Stop();
        }

        /// <summary>
        /// The start paint.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void StartPaint(object sender, EventArgs e)
        {
            this.ProcessFrameTimer.Start();
        }
    }
}