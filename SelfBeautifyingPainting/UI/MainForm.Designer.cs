namespace SelfBeautifyingPainting.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.paintingTabPage = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cameraTabPage = new System.Windows.Forms.TabPage();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.faceController1 = new Accord.Controls.Vision.FaceController();
            this.faceController2 = new Accord.Controls.Vision.FaceController();
            this.headController1 = new Accord.Controls.Vision.HeadController();
            this.headController2 = new Accord.Controls.Vision.HeadController();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.noSmileButton = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.tabControl1.SuspendLayout();
            this.paintingTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cameraTabPage.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.paintingTabPage);
            this.tabControl1.Controls.Add(this.cameraTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 83);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(446, 442);
            this.tabControl1.TabIndex = 0;
            // 
            // paintingTabPage
            // 
            this.paintingTabPage.Controls.Add(this.pictureBox1);
            this.paintingTabPage.Location = new System.Drawing.Point(4, 22);
            this.paintingTabPage.Name = "paintingTabPage";
            this.paintingTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.paintingTabPage.Size = new System.Drawing.Size(438, 416);
            this.paintingTabPage.TabIndex = 0;
            this.paintingTabPage.Text = "Painting";
            this.paintingTabPage.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(432, 410);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // cameraTabPage
            // 
            this.cameraTabPage.Controls.Add(this.elementHost1);
            this.cameraTabPage.Controls.Add(this.videoSourcePlayer1);
            this.cameraTabPage.Location = new System.Drawing.Point(4, 22);
            this.cameraTabPage.Name = "cameraTabPage";
            this.cameraTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.cameraTabPage.Size = new System.Drawing.Size(438, 416);
            this.cameraTabPage.TabIndex = 1;
            this.cameraTabPage.Text = "Camera";
            this.cameraTabPage.UseVisualStyleBackColor = true;
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(3, 3);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(432, 410);
            this.elementHost1.TabIndex = 1;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoSourcePlayer1.Location = new System.Drawing.Point(3, 3);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(432, 410);
            this.videoSourcePlayer1.TabIndex = 0;
            this.videoSourcePlayer1.Text = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            // 
            // faceController1
            // 
            this.faceController1.SynchronizingObject = this;
            // 
            // faceController2
            // 
            this.faceController2.SynchronizingObject = this;
            // 
            // headController1
            // 
            this.headController1.AngleMax = 6.28318530717959D;
            this.headController1.AngleMin = 0D;
            this.headController1.ScaleMax = 277.12812921102D;
            this.headController1.ScaleMin = 0D;
            this.headController1.SynchronizingObject = this;
            this.headController1.XAxisMax = 320;
            this.headController1.XAxisMin = 0;
            this.headController1.YAxisMax = 240;
            this.headController1.YAxisMin = 0;
            // 
            // headController2
            // 
            this.headController2.AngleMax = 6.28318530717959D;
            this.headController2.AngleMin = 0D;
            this.headController2.ScaleMax = 277.12812921102D;
            this.headController2.ScaleMin = 0D;
            this.headController2.SynchronizingObject = this;
            this.headController2.XAxisMax = 320;
            this.headController2.XAxisMin = 0;
            this.headController2.YAxisMax = 240;
            this.headController2.YAxisMin = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.modeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(446, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.fileToolStripMenuItem.Text = "Application";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.noSmileButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(446, 59);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(197, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Smile";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.StartSmileRecording);
            // 
            // noSmileButton
            // 
            this.noSmileButton.Location = new System.Drawing.Point(197, 4);
            this.noSmileButton.Name = "noSmileButton";
            this.noSmileButton.Size = new System.Drawing.Size(75, 23);
            this.noSmileButton.TabIndex = 0;
            this.noSmileButton.Text = "No smile";
            this.noSmileButton.UseVisualStyleBackColor = true;
            this.noSmileButton.Click += new System.EventHandler(this.StartNoSmileRecording);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(278, 4);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(156, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 525);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.tabControl1.ResumeLayout(false);
            this.paintingTabPage.ResumeLayout(false);
            this.paintingTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cameraTabPage.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage paintingTabPage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage cameraTabPage;
        private Accord.Controls.Vision.FaceController faceController1;
        private Accord.Controls.Vision.FaceController faceController2;
        private Accord.Controls.Vision.HeadController headController1;
        private Accord.Controls.Vision.HeadController headController2;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button noSmileButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

