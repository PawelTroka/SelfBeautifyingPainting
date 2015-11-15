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
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.faceController1 = new Accord.Controls.Vision.FaceController();
            this.faceController2 = new Accord.Controls.Vision.FaceController();
            this.headController1 = new Accord.Controls.Vision.HeadController();
            this.headController2 = new Accord.Controls.Vision.HeadController();
            this.tabControl1.SuspendLayout();
            this.paintingTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cameraTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.paintingTabPage);
            this.tabControl1.Controls.Add(this.cameraTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(284, 261);
            this.tabControl1.TabIndex = 0;
            // 
            // paintingTabPage
            // 
            this.paintingTabPage.Controls.Add(this.pictureBox1);
            this.paintingTabPage.Location = new System.Drawing.Point(4, 22);
            this.paintingTabPage.Name = "paintingTabPage";
            this.paintingTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.paintingTabPage.Size = new System.Drawing.Size(276, 235);
            this.paintingTabPage.TabIndex = 0;
            this.paintingTabPage.Text = "Painting";
            this.paintingTabPage.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(270, 229);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // cameraTabPage
            // 
            this.cameraTabPage.Controls.Add(this.videoSourcePlayer1);
            this.cameraTabPage.Location = new System.Drawing.Point(4, 22);
            this.cameraTabPage.Name = "cameraTabPage";
            this.cameraTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.cameraTabPage.Size = new System.Drawing.Size(276, 235);
            this.cameraTabPage.TabIndex = 1;
            this.cameraTabPage.Text = "Camera";
            this.cameraTabPage.UseVisualStyleBackColor = true;
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoSourcePlayer1.Location = new System.Drawing.Point(3, 3);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(270, 229);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.paintingTabPage.ResumeLayout(false);
            this.paintingTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cameraTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

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
    }
}

