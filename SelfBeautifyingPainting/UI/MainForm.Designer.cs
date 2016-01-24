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
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.topRightButton = new System.Windows.Forms.Button();
            this.bottomRightButton = new System.Windows.Forms.Button();
            this.bottomLeftButton = new System.Windows.Forms.Button();
            this.topLeftButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.noSmileButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.paintingTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cameraTabPage.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.paintingTabPage);
            this.tabControl1.Controls.Add(this.cameraTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 109);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1047, 350);
            this.tabControl1.TabIndex = 0;
            // 
            // paintingTabPage
            // 
            this.paintingTabPage.Controls.Add(this.pictureBox1);
            this.paintingTabPage.Location = new System.Drawing.Point(4, 22);
            this.paintingTabPage.Name = "paintingTabPage";
            this.paintingTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.paintingTabPage.Size = new System.Drawing.Size(1039, 324);
            this.paintingTabPage.TabIndex = 0;
            this.paintingTabPage.Text = "Painting";
            this.paintingTabPage.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1033, 318);
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
            this.cameraTabPage.Size = new System.Drawing.Size(907, 328);
            this.cameraTabPage.TabIndex = 1;
            this.cameraTabPage.Text = "Camera";
            this.cameraTabPage.UseVisualStyleBackColor = true;
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(3, 3);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(901, 322);
            this.elementHost1.TabIndex = 1;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoSourcePlayer1.Location = new System.Drawing.Point(3, 3);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(901, 322);
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
            this.menuStrip1.Size = new System.Drawing.Size(1047, 24);
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
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.trackBar2);
            this.panel1.Controls.Add(this.topRightButton);
            this.panel1.Controls.Add(this.bottomRightButton);
            this.panel1.Controls.Add(this.bottomLeftButton);
            this.panel1.Controls.Add(this.topLeftButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.noSmileButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1047, 85);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(448, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Like percent treshold (0-100)";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(451, 34);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(156, 45);
            this.trackBar2.TabIndex = 10;
            this.trackBar2.Value = 50;
            this.trackBar2.Scroll += new System.EventHandler(this.UpdateLikePercentTreshold);
            // 
            // topRightButton
            // 
            this.topRightButton.Location = new System.Drawing.Point(800, 18);
            this.topRightButton.Name = "topRightButton";
            this.topRightButton.Size = new System.Drawing.Size(46, 26);
            this.topRightButton.TabIndex = 9;
            this.topRightButton.Text = "2";
            this.topRightButton.UseVisualStyleBackColor = true;
            // 
            // bottomRightButton
            // 
            this.bottomRightButton.Location = new System.Drawing.Point(800, 45);
            this.bottomRightButton.Name = "bottomRightButton";
            this.bottomRightButton.Size = new System.Drawing.Size(46, 26);
            this.bottomRightButton.TabIndex = 8;
            this.bottomRightButton.Text = "3";
            this.bottomRightButton.UseVisualStyleBackColor = true;
            // 
            // bottomLeftButton
            // 
            this.bottomLeftButton.Location = new System.Drawing.Point(755, 45);
            this.bottomLeftButton.Name = "bottomLeftButton";
            this.bottomLeftButton.Size = new System.Drawing.Size(46, 26);
            this.bottomLeftButton.TabIndex = 7;
            this.bottomLeftButton.Text = "4";
            this.bottomLeftButton.UseVisualStyleBackColor = true;
            // 
            // topLeftButton
            // 
            this.topLeftButton.Location = new System.Drawing.Point(755, 18);
            this.topLeftButton.Name = "topLeftButton";
            this.topLeftButton.Size = new System.Drawing.Size(46, 26);
            this.topLeftButton.TabIndex = 6;
            this.topLeftButton.Text = "1";
            this.topLeftButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Smile detection sensitivity (0-100)";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(641, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 75);
            this.button3.TabIndex = 4;
            this.button3.Text = "Start Paint";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.StartPaint);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 75);
            this.button2.TabIndex = 3;
            this.button2.Text = "Stop Paint";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.StopPaint);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(268, 34);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(156, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.Value = 50;
            this.trackBar1.Scroll += new System.EventHandler(this.ChangeSmileDetectionSensitivity);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(118, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Record Smile";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.StartSmileRecording);
            // 
            // noSmileButton
            // 
            this.noSmileButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.noSmileButton.Location = new System.Drawing.Point(118, 4);
            this.noSmileButton.Name = "noSmileButton";
            this.noSmileButton.Size = new System.Drawing.Size(113, 23);
            this.noSmileButton.TabIndex = 0;
            this.noSmileButton.Text = "Record No Smile";
            this.noSmileButton.UseVisualStyleBackColor = true;
            this.noSmileButton.Click += new System.EventHandler(this.StartNoSmileRecording);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(863, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Last review status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(863, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 459);
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
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button topRightButton;
        private System.Windows.Forms.Button bottomRightButton;
        private System.Windows.Forms.Button bottomLeftButton;
        private System.Windows.Forms.Button topLeftButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

