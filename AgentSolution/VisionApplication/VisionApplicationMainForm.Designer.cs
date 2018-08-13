namespace VisionApplication
{
    partial class VisionApplicationMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisionApplicationMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnlySkinPixelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showBoundingBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCenterLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.deviceNameComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.startCameraButton = new System.Windows.Forms.ToolStripButton();
            this.stopCameraButton = new System.Windows.Forms.ToolStripButton();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.cameraViewTabPage = new System.Windows.Forms.TabPage();
            this.faceDetectionControl = new VisionApplication.FaceDetectionControl();
            this.cameraSetupTabPage = new System.Windows.Forms.TabPage();
            this.cameraSetupControl = new ImageProcessingLibrary.Cameras.CameraSetupControl();
            this.communicationLogTabPage = new System.Windows.Forms.TabPage();
            this.communicationLogColorListBox = new CustomUserControlsLibrary.ColorListBox();
            this.faceTabPage = new System.Windows.Forms.TabPage();
            this.faceDetectionColorListBox = new CustomUserControlsLibrary.ColorListBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.testTabPage = new System.Windows.Forms.TabPage();
            this.testListBox = new System.Windows.Forms.ListBox();
            this.testToolStrip = new System.Windows.Forms.ToolStrip();
            this.sendTestCommandButton = new System.Windows.Forms.ToolStripButton();
            this.testTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.cameraViewTabPage.SuspendLayout();
            this.cameraSetupTabPage.SuspendLayout();
            this.communicationLogTabPage.SuspendLayout();
            this.faceTabPage.SuspendLayout();
            this.testTabPage.SuspendLayout();
            this.testToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(637, 24);
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
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showOnlySkinPixelsToolStripMenuItem,
            this.showBoundingBoxToolStripMenuItem,
            this.showCenterLineToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // showOnlySkinPixelsToolStripMenuItem
            // 
            this.showOnlySkinPixelsToolStripMenuItem.CheckOnClick = true;
            this.showOnlySkinPixelsToolStripMenuItem.Name = "showOnlySkinPixelsToolStripMenuItem";
            this.showOnlySkinPixelsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.showOnlySkinPixelsToolStripMenuItem.Text = "Show only skin pixels";
            this.showOnlySkinPixelsToolStripMenuItem.Click += new System.EventHandler(this.showOnlySkinPixelsToolStripMenuItem_Click);
            // 
            // showBoundingBoxToolStripMenuItem
            // 
            this.showBoundingBoxToolStripMenuItem.Checked = true;
            this.showBoundingBoxToolStripMenuItem.CheckOnClick = true;
            this.showBoundingBoxToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showBoundingBoxToolStripMenuItem.Name = "showBoundingBoxToolStripMenuItem";
            this.showBoundingBoxToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.showBoundingBoxToolStripMenuItem.Text = "Show bounding box";
            this.showBoundingBoxToolStripMenuItem.Click += new System.EventHandler(this.showBoundingBoxToolStripMenuItem_Click);
            // 
            // showCenterLineToolStripMenuItem
            // 
            this.showCenterLineToolStripMenuItem.Checked = true;
            this.showCenterLineToolStripMenuItem.CheckOnClick = true;
            this.showCenterLineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showCenterLineToolStripMenuItem.Name = "showCenterLineToolStripMenuItem";
            this.showCenterLineToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.showCenterLineToolStripMenuItem.Text = "Show center line";
            this.showCenterLineToolStripMenuItem.Click += new System.EventHandler(this.showCenterLineToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.deviceNameComboBox,
            this.startCameraButton,
            this.stopCameraButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(637, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(78, 22);
            this.toolStripLabel1.Text = "Device name:";
            // 
            // deviceNameComboBox
            // 
            this.deviceNameComboBox.Name = "deviceNameComboBox";
            this.deviceNameComboBox.Size = new System.Drawing.Size(121, 25);
            this.deviceNameComboBox.SelectedIndexChanged += new System.EventHandler(this.deviceNameComboBox_SelectedIndexChanged);
            // 
            // startCameraButton
            // 
            this.startCameraButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startCameraButton.Enabled = false;
            this.startCameraButton.Image = ((System.Drawing.Image)(resources.GetObject("startCameraButton.Image")));
            this.startCameraButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startCameraButton.Name = "startCameraButton";
            this.startCameraButton.Size = new System.Drawing.Size(35, 22);
            this.startCameraButton.Text = "Start";
            this.startCameraButton.Click += new System.EventHandler(this.startCameraButton_Click);
            // 
            // stopCameraButton
            // 
            this.stopCameraButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopCameraButton.Enabled = false;
            this.stopCameraButton.Image = ((System.Drawing.Image)(resources.GetObject("stopCameraButton.Image")));
            this.stopCameraButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopCameraButton.Name = "stopCameraButton";
            this.stopCameraButton.Size = new System.Drawing.Size(35, 22);
            this.stopCameraButton.Text = "Stop";
            this.stopCameraButton.Click += new System.EventHandler(this.stopCameraButton_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.cameraViewTabPage);
            this.mainTabControl.Controls.Add(this.cameraSetupTabPage);
            this.mainTabControl.Controls.Add(this.communicationLogTabPage);
            this.mainTabControl.Controls.Add(this.faceTabPage);
            this.mainTabControl.Controls.Add(this.testTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 49);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(637, 323);
            this.mainTabControl.TabIndex = 2;
            this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedIndexChanged);
            // 
            // cameraViewTabPage
            // 
            this.cameraViewTabPage.Controls.Add(this.faceDetectionControl);
            this.cameraViewTabPage.Location = new System.Drawing.Point(4, 22);
            this.cameraViewTabPage.Name = "cameraViewTabPage";
            this.cameraViewTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.cameraViewTabPage.Size = new System.Drawing.Size(629, 297);
            this.cameraViewTabPage.TabIndex = 0;
            this.cameraViewTabPage.Text = "Camera view";
            this.cameraViewTabPage.UseVisualStyleBackColor = true;
            // 
            // faceDetectionControl
            // 
            this.faceDetectionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.faceDetectionControl.DrawDetection = true;
            this.faceDetectionControl.Location = new System.Drawing.Point(3, 3);
            this.faceDetectionControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.faceDetectionControl.Name = "faceDetectionControl";
            this.faceDetectionControl.ShowBoundingBox = true;
            this.faceDetectionControl.ShowCenterLine = true;
            this.faceDetectionControl.ShowSkinPixelsOnly = false;
            this.faceDetectionControl.Size = new System.Drawing.Size(623, 291);
            this.faceDetectionControl.TabIndex = 3;
            // 
            // cameraSetupTabPage
            // 
            this.cameraSetupTabPage.Controls.Add(this.cameraSetupControl);
            this.cameraSetupTabPage.Location = new System.Drawing.Point(4, 22);
            this.cameraSetupTabPage.Name = "cameraSetupTabPage";
            this.cameraSetupTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.cameraSetupTabPage.Size = new System.Drawing.Size(629, 297);
            this.cameraSetupTabPage.TabIndex = 1;
            this.cameraSetupTabPage.Text = "Camera setup";
            this.cameraSetupTabPage.UseVisualStyleBackColor = true;
            // 
            // cameraSetupControl
            // 
            this.cameraSetupControl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.cameraSetupControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraSetupControl.Location = new System.Drawing.Point(3, 3);
            this.cameraSetupControl.Name = "cameraSetupControl";
            this.cameraSetupControl.Size = new System.Drawing.Size(623, 291);
            this.cameraSetupControl.TabIndex = 0;
            // 
            // communicationLogTabPage
            // 
            this.communicationLogTabPage.Controls.Add(this.communicationLogColorListBox);
            this.communicationLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.communicationLogTabPage.Name = "communicationLogTabPage";
            this.communicationLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.communicationLogTabPage.Size = new System.Drawing.Size(629, 297);
            this.communicationLogTabPage.TabIndex = 2;
            this.communicationLogTabPage.Text = "Communication log";
            this.communicationLogTabPage.UseVisualStyleBackColor = true;
            // 
            // communicationLogColorListBox
            // 
            this.communicationLogColorListBox.BackColor = System.Drawing.Color.Black;
            this.communicationLogColorListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.communicationLogColorListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.communicationLogColorListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.communicationLogColorListBox.ForeColor = System.Drawing.Color.Lime;
            this.communicationLogColorListBox.FormattingEnabled = true;
            this.communicationLogColorListBox.Location = new System.Drawing.Point(3, 3);
            this.communicationLogColorListBox.Name = "communicationLogColorListBox";
            this.communicationLogColorListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.communicationLogColorListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.communicationLogColorListBox.Size = new System.Drawing.Size(623, 291);
            this.communicationLogColorListBox.TabIndex = 1;
            // 
            // faceTabPage
            // 
            this.faceTabPage.Controls.Add(this.faceDetectionColorListBox);
            this.faceTabPage.Controls.Add(this.toolStrip2);
            this.faceTabPage.Location = new System.Drawing.Point(4, 22);
            this.faceTabPage.Name = "faceTabPage";
            this.faceTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.faceTabPage.Size = new System.Drawing.Size(629, 297);
            this.faceTabPage.TabIndex = 3;
            this.faceTabPage.Text = "Face detection";
            this.faceTabPage.UseVisualStyleBackColor = true;
            // 
            // faceDetectionColorListBox
            // 
            this.faceDetectionColorListBox.BackColor = System.Drawing.Color.Black;
            this.faceDetectionColorListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.faceDetectionColorListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.faceDetectionColorListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.faceDetectionColorListBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.faceDetectionColorListBox.ForeColor = System.Drawing.Color.Lime;
            this.faceDetectionColorListBox.FormattingEnabled = true;
            this.faceDetectionColorListBox.Location = new System.Drawing.Point(3, 28);
            this.faceDetectionColorListBox.Name = "faceDetectionColorListBox";
            this.faceDetectionColorListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.faceDetectionColorListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.faceDetectionColorListBox.Size = new System.Drawing.Size(623, 266);
            this.faceDetectionColorListBox.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(623, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // testTabPage
            // 
            this.testTabPage.Controls.Add(this.testListBox);
            this.testTabPage.Controls.Add(this.testToolStrip);
            this.testTabPage.Location = new System.Drawing.Point(4, 22);
            this.testTabPage.Name = "testTabPage";
            this.testTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.testTabPage.Size = new System.Drawing.Size(629, 297);
            this.testTabPage.TabIndex = 4;
            this.testTabPage.Text = "Test";
            this.testTabPage.UseVisualStyleBackColor = true;
            // 
            // testListBox
            // 
            this.testListBox.BackColor = System.Drawing.Color.Black;
            this.testListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testListBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testListBox.ForeColor = System.Drawing.Color.Lime;
            this.testListBox.FormattingEnabled = true;
            this.testListBox.ItemHeight = 11;
            this.testListBox.Location = new System.Drawing.Point(3, 28);
            this.testListBox.Name = "testListBox";
            this.testListBox.Size = new System.Drawing.Size(623, 266);
            this.testListBox.TabIndex = 1;
            this.testListBox.SelectedIndexChanged += new System.EventHandler(this.testListBox_SelectedIndexChanged);
            // 
            // testToolStrip
            // 
            this.testToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendTestCommandButton,
            this.testTextBox});
            this.testToolStrip.Location = new System.Drawing.Point(3, 3);
            this.testToolStrip.Name = "testToolStrip";
            this.testToolStrip.Size = new System.Drawing.Size(623, 25);
            this.testToolStrip.TabIndex = 0;
            this.testToolStrip.Text = "toolStrip1";
            // 
            // sendTestCommandButton
            // 
            this.sendTestCommandButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sendTestCommandButton.Enabled = false;
            this.sendTestCommandButton.Image = ((System.Drawing.Image)(resources.GetObject("sendTestCommandButton.Image")));
            this.sendTestCommandButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sendTestCommandButton.Name = "sendTestCommandButton";
            this.sendTestCommandButton.Size = new System.Drawing.Size(59, 22);
            this.sendTestCommandButton.Text = "Send test";
            this.sendTestCommandButton.Click += new System.EventHandler(this.sendTestCommandButton_Click);
            // 
            // testTextBox
            // 
            this.testTextBox.AutoSize = false;
            this.testTextBox.Name = "testTextBox";
            this.testTextBox.Size = new System.Drawing.Size(400, 25);
            this.testTextBox.TextChanged += new System.EventHandler(this.testTextBox_TextChanged);
            // 
            // VisionApplicationMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 372);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "VisionApplicationMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vision";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VisionApplicationMainForm_FormClosing);
            this.Load += new System.EventHandler(this.VisionApplicationMainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.VisionApplicationMainForm_ResizeEnd);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.cameraViewTabPage.ResumeLayout(false);
            this.cameraSetupTabPage.ResumeLayout(false);
            this.communicationLogTabPage.ResumeLayout(false);
            this.faceTabPage.ResumeLayout(false);
            this.faceTabPage.PerformLayout();
            this.testTabPage.ResumeLayout(false);
            this.testTabPage.PerformLayout();
            this.testToolStrip.ResumeLayout(false);
            this.testToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOnlySkinPixelsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage cameraViewTabPage;
        private FaceDetectionControl faceDetectionControl;
        private System.Windows.Forms.TabPage cameraSetupTabPage;
        private ImageProcessingLibrary.Cameras.CameraSetupControl cameraSetupControl;
        private System.Windows.Forms.TabPage communicationLogTabPage;
        private CustomUserControlsLibrary.ColorListBox communicationLogColorListBox;
        private System.Windows.Forms.TabPage faceTabPage;
        private CustomUserControlsLibrary.ColorListBox faceDetectionColorListBox;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.TabPage testTabPage;
        private System.Windows.Forms.ListBox testListBox;
        private System.Windows.Forms.ToolStrip testToolStrip;
        private System.Windows.Forms.ToolStripButton sendTestCommandButton;
        private System.Windows.Forms.ToolStripTextBox testTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox deviceNameComboBox;
        private System.Windows.Forms.ToolStripButton startCameraButton;
        private System.Windows.Forms.ToolStripButton stopCameraButton;
        private System.Windows.Forms.ToolStripMenuItem showBoundingBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCenterLineToolStripMenuItem;
    }
}

