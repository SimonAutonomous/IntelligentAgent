namespace VideoProcessingApplication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.cameraViewTabPage = new System.Windows.Forms.TabPage();
            this.cameraViewControl = new ImageProcessingLibrary.Cameras.CameraViewControl();
            this.gestureViewTabPage = new System.Windows.Forms.TabPage();
            this.motionDetectionControl = new ImageProcessingLibrary.MotionDetection.MotionDetectionControl();
            this.setupTabPage = new System.Windows.Forms.TabPage();
            this.cameraSetupControl = new ImageProcessingLibrary.Cameras.CameraSetupControl();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.cameraViewTabPage.SuspendLayout();
            this.gestureViewTabPage.SuspendLayout();
            this.setupTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(654, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startButton,
            this.stopButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(654, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // startButton
            // 
            this.startButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(35, 22);
            this.startButton.Text = "Start";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopButton.Enabled = false;
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(35, 22);
            this.stopButton.Text = "Stop";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.cameraViewTabPage);
            this.mainTabControl.Controls.Add(this.gestureViewTabPage);
            this.mainTabControl.Controls.Add(this.setupTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Enabled = false;
            this.mainTabControl.Location = new System.Drawing.Point(0, 49);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(654, 512);
            this.mainTabControl.TabIndex = 2;
            this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedIndexChanged);
            // 
            // cameraViewTabPage
            // 
            this.cameraViewTabPage.Controls.Add(this.cameraViewControl);
            this.cameraViewTabPage.Location = new System.Drawing.Point(4, 22);
            this.cameraViewTabPage.Name = "cameraViewTabPage";
            this.cameraViewTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.cameraViewTabPage.Size = new System.Drawing.Size(646, 486);
            this.cameraViewTabPage.TabIndex = 0;
            this.cameraViewTabPage.Text = "Camera view";
            this.cameraViewTabPage.UseVisualStyleBackColor = true;
            // 
            // cameraViewControl
            // 
            this.cameraViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraViewControl.Location = new System.Drawing.Point(3, 3);
            this.cameraViewControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cameraViewControl.Name = "cameraViewControl";
            this.cameraViewControl.Size = new System.Drawing.Size(640, 480);
            this.cameraViewControl.TabIndex = 0;
            // 
            // gestureViewTabPage
            // 
            this.gestureViewTabPage.Controls.Add(this.motionDetectionControl);
            this.gestureViewTabPage.Location = new System.Drawing.Point(4, 22);
            this.gestureViewTabPage.Name = "gestureViewTabPage";
            this.gestureViewTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.gestureViewTabPage.Size = new System.Drawing.Size(646, 486);
            this.gestureViewTabPage.TabIndex = 2;
            this.gestureViewTabPage.Text = "Gesture view";
            this.gestureViewTabPage.UseVisualStyleBackColor = true;
            // 
            // motionDetectionControl
            // 
            this.motionDetectionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motionDetectionControl.FrameRate = 25;
            this.motionDetectionControl.Location = new System.Drawing.Point(3, 3);
            this.motionDetectionControl.Name = "motionDetectionControl";
            this.motionDetectionControl.Size = new System.Drawing.Size(640, 480);
            this.motionDetectionControl.TabIndex = 0;
            // 
            // setupTabPage
            // 
            this.setupTabPage.BackColor = System.Drawing.Color.Black;
            this.setupTabPage.Controls.Add(this.cameraSetupControl);
            this.setupTabPage.Location = new System.Drawing.Point(4, 22);
            this.setupTabPage.Name = "setupTabPage";
            this.setupTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.setupTabPage.Size = new System.Drawing.Size(646, 486);
            this.setupTabPage.TabIndex = 1;
            this.setupTabPage.Text = "Camera setup";
            // 
            // cameraSetupControl
            // 
            this.cameraSetupControl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.cameraSetupControl.Location = new System.Drawing.Point(8, 6);
            this.cameraSetupControl.Name = "cameraSetupControl";
            this.cameraSetupControl.Size = new System.Drawing.Size(1039, 455);
            this.cameraSetupControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 561);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Video processing application (c) 2016, Mattias Wahde, mattias.wahde@chalmers.se";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.cameraViewTabPage.ResumeLayout(false);
            this.gestureViewTabPage.ResumeLayout(false);
            this.setupTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage cameraViewTabPage;
        private ImageProcessingLibrary.Cameras.CameraViewControl cameraViewControl;
        private System.Windows.Forms.TabPage setupTabPage;
        private System.Windows.Forms.ToolStripButton startButton;
        private ImageProcessingLibrary.Cameras.CameraSetupControl cameraSetupControl;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.TabPage gestureViewTabPage;
        private ImageProcessingLibrary.MotionDetection.MotionDetectionControl motionDetectionControl;
    }
}

