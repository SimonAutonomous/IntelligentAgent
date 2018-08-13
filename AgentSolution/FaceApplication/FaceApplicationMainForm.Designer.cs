namespace FaceApplication
{
    partial class FaceApplicationMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaceApplicationMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openEyesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shakeHeadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.faceTabPage = new System.Windows.Forms.TabPage();
            this.viewer3D = new ThreeDimensionalVisualizationLibrary.Viewer3D();
            this.communicationLogTabPage = new System.Windows.Forms.TabPage();
            this.communicationLogListBox = new CustomUserControlsLibrary.ColorListBox();
            this.menuStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.faceTabPage.SuspendLayout();
            this.communicationLogTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(382, 24);
            this.menuStrip1.TabIndex = 1;
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
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openEyesToolStripMenuItem,
            this.blinkToolStripMenuItem,
            this.nodToolStripMenuItem,
            this.shakeHeadToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // openEyesToolStripMenuItem
            // 
            this.openEyesToolStripMenuItem.Name = "openEyesToolStripMenuItem";
            this.openEyesToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.openEyesToolStripMenuItem.Text = "Open eyes";
            this.openEyesToolStripMenuItem.Click += new System.EventHandler(this.openEyesToolStripMenuItem_Click);
            // 
            // blinkToolStripMenuItem
            // 
            this.blinkToolStripMenuItem.Name = "blinkToolStripMenuItem";
            this.blinkToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.blinkToolStripMenuItem.Text = "Blink";
            this.blinkToolStripMenuItem.Click += new System.EventHandler(this.blinkToolStripMenuItem_Click);
            // 
            // nodToolStripMenuItem
            // 
            this.nodToolStripMenuItem.Name = "nodToolStripMenuItem";
            this.nodToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.nodToolStripMenuItem.Text = "Nod";
            this.nodToolStripMenuItem.Click += new System.EventHandler(this.nodToolStripMenuItem_Click);
            // 
            // shakeHeadToolStripMenuItem
            // 
            this.shakeHeadToolStripMenuItem.Name = "shakeHeadToolStripMenuItem";
            this.shakeHeadToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.shakeHeadToolStripMenuItem.Text = "Shake head";
            this.shakeHeadToolStripMenuItem.Click += new System.EventHandler(this.shakeHeadToolStripMenuItem_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.faceTabPage);
            this.mainTabControl.Controls.Add(this.communicationLogTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 24);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(382, 354);
            this.mainTabControl.TabIndex = 2;
            // 
            // faceTabPage
            // 
            this.faceTabPage.Controls.Add(this.viewer3D);
            this.faceTabPage.Location = new System.Drawing.Point(4, 22);
            this.faceTabPage.Name = "faceTabPage";
            this.faceTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.faceTabPage.Size = new System.Drawing.Size(374, 328);
            this.faceTabPage.TabIndex = 1;
            this.faceTabPage.Text = "Face";
            this.faceTabPage.UseVisualStyleBackColor = true;
            // 
            // viewer3D
            // 
            this.viewer3D.BackColor = System.Drawing.Color.Black;
            this.viewer3D.CameraDistance = 4D;
            this.viewer3D.CameraLatitude = 0.39269908169872414D;
            this.viewer3D.CameraLongitude = 0.78539816339744828D;
            this.viewer3D.CameraTarget = ((OpenTK.Vector3)(resources.GetObject("viewer3D.CameraTarget")));
            this.viewer3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer3D.Location = new System.Drawing.Point(3, 3);
            this.viewer3D.Name = "viewer3D";
            this.viewer3D.Scene = null;
            this.viewer3D.ShowSurfaces = true;
            this.viewer3D.ShowVertices = false;
            this.viewer3D.ShowWireframe = false;
            this.viewer3D.ShowWorldAxes = false;
            this.viewer3D.Size = new System.Drawing.Size(368, 322);
            this.viewer3D.TabIndex = 1;
            this.viewer3D.UseSmoothShading = false;
            this.viewer3D.VSync = false;
            // 
            // communicationLogTabPage
            // 
            this.communicationLogTabPage.Controls.Add(this.communicationLogListBox);
            this.communicationLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.communicationLogTabPage.Name = "communicationLogTabPage";
            this.communicationLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.communicationLogTabPage.Size = new System.Drawing.Size(374, 328);
            this.communicationLogTabPage.TabIndex = 0;
            this.communicationLogTabPage.Text = "Communication log";
            this.communicationLogTabPage.UseVisualStyleBackColor = true;
            // 
            // communicationLogListBox
            // 
            this.communicationLogListBox.BackColor = System.Drawing.Color.Black;
            this.communicationLogListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.communicationLogListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.communicationLogListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.communicationLogListBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.communicationLogListBox.ForeColor = System.Drawing.Color.Lime;
            this.communicationLogListBox.FormattingEnabled = true;
            this.communicationLogListBox.IntegralHeight = false;
            this.communicationLogListBox.Location = new System.Drawing.Point(3, 3);
            this.communicationLogListBox.Name = "communicationLogListBox";
            this.communicationLogListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.communicationLogListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.communicationLogListBox.Size = new System.Drawing.Size(368, 322);
            this.communicationLogListBox.TabIndex = 0;
            // 
            // FaceApplicationMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 378);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FaceApplicationMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Face application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaceApplicationMainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.faceTabPage.ResumeLayout(false);
            this.communicationLogTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openEyesToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage communicationLogTabPage;
        private CustomUserControlsLibrary.ColorListBox communicationLogListBox;
        private System.Windows.Forms.TabPage faceTabPage;
        private ThreeDimensionalVisualizationLibrary.Viewer3D viewer3D;
        private System.Windows.Forms.ToolStripMenuItem blinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shakeHeadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

