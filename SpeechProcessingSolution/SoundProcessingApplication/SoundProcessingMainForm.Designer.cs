namespace SoundProcessingApplication
{
    partial class SoundProcessingMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundProcessingMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.recordingButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.raiseVolumeButton = new System.Windows.Forms.ToolStripButton();
            this.reduceVolumeButton = new System.Windows.Forms.ToolStripButton();
            this.playButton = new System.Windows.Forms.ToolStripButton();
            this.soundVisualizer = new AudioLibrary.Visualization.SoundVisualizer();
            this.filterToolStrip = new System.Windows.Forms.ToolStrip();
            this.lowPassFilterButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.lowpassFilterCutoffTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.highpassFilterButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.highpassFilterCutoffTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.filterToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(906, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSoundToolStripMenuItem,
            this.saveSoundToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadSoundToolStripMenuItem
            // 
            this.loadSoundToolStripMenuItem.Name = "loadSoundToolStripMenuItem";
            this.loadSoundToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadSoundToolStripMenuItem.Text = "Load sound";
            this.loadSoundToolStripMenuItem.Click += new System.EventHandler(this.loadSoundToolStripMenuItem_Click);
            // 
            // saveSoundToolStripMenuItem
            // 
            this.saveSoundToolStripMenuItem.Enabled = false;
            this.saveSoundToolStripMenuItem.Name = "saveSoundToolStripMenuItem";
            this.saveSoundToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveSoundToolStripMenuItem.Text = "Save sound";
            this.saveSoundToolStripMenuItem.Click += new System.EventHandler(this.saveSoundToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordingButton,
            this.toolStripSeparator1,
            this.raiseVolumeButton,
            this.reduceVolumeButton,
            this.playButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(906, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // recordingButton
            // 
            this.recordingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.recordingButton.Image = ((System.Drawing.Image)(resources.GetObject("recordingButton.Image")));
            this.recordingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recordingButton.Name = "recordingButton";
            this.recordingButton.Size = new System.Drawing.Size(89, 22);
            this.recordingButton.Text = "Start recording";
            this.recordingButton.Click += new System.EventHandler(this.recordingButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // raiseVolumeButton
            // 
            this.raiseVolumeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.raiseVolumeButton.Enabled = false;
            this.raiseVolumeButton.Image = ((System.Drawing.Image)(resources.GetObject("raiseVolumeButton.Image")));
            this.raiseVolumeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.raiseVolumeButton.Name = "raiseVolumeButton";
            this.raiseVolumeButton.Size = new System.Drawing.Size(81, 22);
            this.raiseVolumeButton.Text = "Raise volume";
            this.raiseVolumeButton.Click += new System.EventHandler(this.raiseVolumeButton_Click);
            // 
            // reduceVolumeButton
            // 
            this.reduceVolumeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.reduceVolumeButton.Enabled = false;
            this.reduceVolumeButton.Image = ((System.Drawing.Image)(resources.GetObject("reduceVolumeButton.Image")));
            this.reduceVolumeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reduceVolumeButton.Name = "reduceVolumeButton";
            this.reduceVolumeButton.Size = new System.Drawing.Size(93, 22);
            this.reduceVolumeButton.Text = "Reduce volume";
            this.reduceVolumeButton.Click += new System.EventHandler(this.reduceVolumeButton_Click);
            // 
            // playButton
            // 
            this.playButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.playButton.Enabled = false;
            this.playButton.Image = ((System.Drawing.Image)(resources.GetObject("playButton.Image")));
            this.playButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(33, 22);
            this.playButton.Text = "Play";
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // soundVisualizer
            // 
            this.soundVisualizer.BackColor = System.Drawing.Color.Black;
            this.soundVisualizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soundVisualizer.Location = new System.Drawing.Point(0, 49);
            this.soundVisualizer.MarkerList = null;
            this.soundVisualizer.Name = "soundVisualizer";
            this.soundVisualizer.Size = new System.Drawing.Size(906, 438);
            this.soundVisualizer.TabIndex = 2;
            // 
            // filterToolStrip
            // 
            this.filterToolStrip.Enabled = false;
            this.filterToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lowPassFilterButton,
            this.toolStripLabel1,
            this.lowpassFilterCutoffTextBox,
            this.toolStripSeparator2,
            this.highpassFilterButton,
            this.toolStripLabel2,
            this.highpassFilterCutoffTextBox});
            this.filterToolStrip.Location = new System.Drawing.Point(0, 49);
            this.filterToolStrip.Name = "filterToolStrip";
            this.filterToolStrip.Size = new System.Drawing.Size(906, 25);
            this.filterToolStrip.TabIndex = 3;
            this.filterToolStrip.Text = "toolStrip2";
            // 
            // lowPassFilterButton
            // 
            this.lowPassFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lowPassFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("lowPassFilterButton.Image")));
            this.lowPassFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lowPassFilterButton.Name = "lowPassFilterButton";
            this.lowPassFilterButton.Size = new System.Drawing.Size(83, 22);
            this.lowPassFilterButton.Text = "Lowpass filter";
            this.lowPassFilterButton.Click += new System.EventHandler(this.lowPassFilterButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(100, 22);
            this.toolStripLabel1.Text = "Cutoff frequency:";
            // 
            // lowpassFilterCutoffTextBox
            // 
            this.lowpassFilterCutoffTextBox.Name = "lowpassFilterCutoffTextBox";
            this.lowpassFilterCutoffTextBox.Size = new System.Drawing.Size(50, 25);
            this.lowpassFilterCutoffTextBox.Tag = "";
            this.lowpassFilterCutoffTextBox.Text = "300";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // highpassFilterButton
            // 
            this.highpassFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.highpassFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("highpassFilterButton.Image")));
            this.highpassFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.highpassFilterButton.Name = "highpassFilterButton";
            this.highpassFilterButton.Size = new System.Drawing.Size(87, 22);
            this.highpassFilterButton.Text = "Highpass filter";
            this.highpassFilterButton.Click += new System.EventHandler(this.highpassFilterButton_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(100, 22);
            this.toolStripLabel2.Text = "Cutoff frequency:";
            // 
            // highpassFilterCutoffTextBox
            // 
            this.highpassFilterCutoffTextBox.Name = "highpassFilterCutoffTextBox";
            this.highpassFilterCutoffTextBox.Size = new System.Drawing.Size(50, 25);
            this.highpassFilterCutoffTextBox.Tag = "";
            this.highpassFilterCutoffTextBox.Text = "300";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // SoundProcessingMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 487);
            this.Controls.Add(this.filterToolStrip);
            this.Controls.Add(this.soundVisualizer);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SoundProcessingMainForm";
            this.Text = "Sound processing application";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.filterToolStrip.ResumeLayout(false);
            this.filterToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton recordingButton;
        private AudioLibrary.Visualization.SoundVisualizer soundVisualizer;
        private System.Windows.Forms.ToolStrip filterToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton raiseVolumeButton;
        private System.Windows.Forms.ToolStripButton reduceVolumeButton;
        private System.Windows.Forms.ToolStripButton playButton;
        private System.Windows.Forms.ToolStripButton lowPassFilterButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox lowpassFilterCutoffTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton highpassFilterButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox highpassFilterCutoffTextBox;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

