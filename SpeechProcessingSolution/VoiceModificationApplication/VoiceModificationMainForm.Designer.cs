namespace VoiceModificationApplication
{
    partial class VoiceModificationMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoiceModificationMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parameterSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.voiceSelectionComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.sentenceTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.speakButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.soundTypeIdentificationButton = new System.Windows.Forms.ToolStripButton();
            this.findPitchPeriodsButton = new System.Windows.Forms.ToolStripButton();
            this.findPitchMarksButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.playSoundButton = new System.Windows.Forms.ToolStripButton();
            this.modifySoundButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.topFractionTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.relativeStartPitchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.relativeEndPitchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.adjustDurationComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.relativeDurationLabel = new System.Windows.Forms.ToolStripLabel();
            this.relativeDurationTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.speechVisualizer = new SpeechSynthesisLibrary.Visualization.SpeechVisualizer();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1051, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSoundToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveSoundToolStripMenuItem
            // 
            this.saveSoundToolStripMenuItem.Enabled = false;
            this.saveSoundToolStripMenuItem.Name = "saveSoundToolStripMenuItem";
            this.saveSoundToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveSoundToolStripMenuItem.Text = "Save sound";
            this.saveSoundToolStripMenuItem.Click += new System.EventHandler(this.saveSoundToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parameterSettingsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // parameterSettingsToolStripMenuItem
            // 
            this.parameterSettingsToolStripMenuItem.Name = "parameterSettingsToolStripMenuItem";
            this.parameterSettingsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.parameterSettingsToolStripMenuItem.Text = "Parameter settings";
            this.parameterSettingsToolStripMenuItem.Click += new System.EventHandler(this.parameterSettingsToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.voiceSelectionComboBox,
            this.toolStripLabel2,
            this.sentenceTextBox,
            this.speakButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1051, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Voice:";
            // 
            // voiceSelectionComboBox
            // 
            this.voiceSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.voiceSelectionComboBox.Name = "voiceSelectionComboBox";
            this.voiceSelectionComboBox.Size = new System.Drawing.Size(400, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel2.Text = "Sentence:";
            // 
            // sentenceTextBox
            // 
            this.sentenceTextBox.Name = "sentenceTextBox";
            this.sentenceTextBox.Size = new System.Drawing.Size(300, 25);
            this.sentenceTextBox.Text = "Hello";
            // 
            // speakButton
            // 
            this.speakButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.speakButton.Image = ((System.Drawing.Image)(resources.GetObject("speakButton.Image")));
            this.speakButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.speakButton.Name = "speakButton";
            this.speakButton.Size = new System.Drawing.Size(42, 22);
            this.speakButton.Text = "Speak";
            this.speakButton.Click += new System.EventHandler(this.speakButton_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soundTypeIdentificationButton,
            this.findPitchPeriodsButton,
            this.findPitchMarksButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 49);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1051, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // soundTypeIdentificationButton
            // 
            this.soundTypeIdentificationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.soundTypeIdentificationButton.Enabled = false;
            this.soundTypeIdentificationButton.Image = ((System.Drawing.Image)(resources.GetObject("soundTypeIdentificationButton.Image")));
            this.soundTypeIdentificationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.soundTypeIdentificationButton.Name = "soundTypeIdentificationButton";
            this.soundTypeIdentificationButton.Size = new System.Drawing.Size(113, 22);
            this.soundTypeIdentificationButton.Text = "Identify sound type";
            this.soundTypeIdentificationButton.Click += new System.EventHandler(this.soundTypeIdentificationButton_Click);
            // 
            // findPitchPeriodsButton
            // 
            this.findPitchPeriodsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.findPitchPeriodsButton.Enabled = false;
            this.findPitchPeriodsButton.Image = ((System.Drawing.Image)(resources.GetObject("findPitchPeriodsButton.Image")));
            this.findPitchPeriodsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findPitchPeriodsButton.Name = "findPitchPeriodsButton";
            this.findPitchPeriodsButton.Size = new System.Drawing.Size(106, 22);
            this.findPitchPeriodsButton.Text = "Find pitch periods";
            this.findPitchPeriodsButton.Click += new System.EventHandler(this.findPitchPeriodsButton_Click);
            // 
            // findPitchMarksButton
            // 
            this.findPitchMarksButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.findPitchMarksButton.Enabled = false;
            this.findPitchMarksButton.Image = ((System.Drawing.Image)(resources.GetObject("findPitchMarksButton.Image")));
            this.findPitchMarksButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findPitchMarksButton.Name = "findPitchMarksButton";
            this.findPitchMarksButton.Size = new System.Drawing.Size(99, 22);
            this.findPitchMarksButton.Text = "Find pitch marks";
            this.findPitchMarksButton.Click += new System.EventHandler(this.findPitchMarksButton_Click);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playSoundButton,
            this.modifySoundButton,
            this.toolStripLabel5,
            this.topFractionTextBox,
            this.toolStripSeparator1,
            this.toolStripLabel3,
            this.relativeStartPitchTextBox,
            this.toolStripLabel4,
            this.relativeEndPitchTextBox,
            this.toolStripLabel6,
            this.adjustDurationComboBox,
            this.relativeDurationLabel,
            this.relativeDurationTextBox});
            this.toolStrip3.Location = new System.Drawing.Point(0, 74);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1051, 25);
            this.toolStrip3.TabIndex = 3;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // playSoundButton
            // 
            this.playSoundButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.playSoundButton.Enabled = false;
            this.playSoundButton.Image = ((System.Drawing.Image)(resources.GetObject("playSoundButton.Image")));
            this.playSoundButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playSoundButton.Name = "playSoundButton";
            this.playSoundButton.Size = new System.Drawing.Size(69, 22);
            this.playSoundButton.Text = "Play sound";
            this.playSoundButton.Click += new System.EventHandler(this.playSoundButton_Click);
            // 
            // modifySoundButton
            // 
            this.modifySoundButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.modifySoundButton.Enabled = false;
            this.modifySoundButton.Image = ((System.Drawing.Image)(resources.GetObject("modifySoundButton.Image")));
            this.modifySoundButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modifySoundButton.Name = "modifySoundButton";
            this.modifySoundButton.Size = new System.Drawing.Size(85, 22);
            this.modifySoundButton.Text = "Modify sound";
            this.modifySoundButton.Click += new System.EventHandler(this.modifySoundButton_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(75, 22);
            this.toolStripLabel5.Text = "Top fraction:";
            // 
            // topFractionTextBox
            // 
            this.topFractionTextBox.Name = "topFractionTextBox";
            this.topFractionTextBox.Size = new System.Drawing.Size(40, 25);
            this.topFractionTextBox.Text = "0.20";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(104, 22);
            this.toolStripLabel3.Text = "relative start pitch:";
            // 
            // relativeStartPitchTextBox
            // 
            this.relativeStartPitchTextBox.Name = "relativeStartPitchTextBox";
            this.relativeStartPitchTextBox.Size = new System.Drawing.Size(40, 25);
            this.relativeStartPitchTextBox.Text = "1.0";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(101, 22);
            this.toolStripLabel4.Text = "relative end pitch:";
            // 
            // relativeEndPitchTextBox
            // 
            this.relativeEndPitchTextBox.Name = "relativeEndPitchTextBox";
            this.relativeEndPitchTextBox.Size = new System.Drawing.Size(40, 25);
            this.relativeEndPitchTextBox.Text = "1.0";
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(92, 22);
            this.toolStripLabel6.Text = "Adjust duration:";
            // 
            // adjustDurationComboBox
            // 
            this.adjustDurationComboBox.Items.AddRange(new object[] {
            "False",
            "True"});
            this.adjustDurationComboBox.Name = "adjustDurationComboBox";
            this.adjustDurationComboBox.Size = new System.Drawing.Size(75, 25);
            this.adjustDurationComboBox.SelectedIndexChanged += new System.EventHandler(this.adjustDurationComboBox_SelectedIndexChanged);
            // 
            // relativeDurationLabel
            // 
            this.relativeDurationLabel.Name = "relativeDurationLabel";
            this.relativeDurationLabel.Size = new System.Drawing.Size(96, 22);
            this.relativeDurationLabel.Text = "relative duration:";
            this.relativeDurationLabel.Visible = false;
            // 
            // relativeDurationTextBox
            // 
            this.relativeDurationTextBox.Name = "relativeDurationTextBox";
            this.relativeDurationTextBox.Size = new System.Drawing.Size(40, 25);
            this.relativeDurationTextBox.Text = "1.0";
            this.relativeDurationTextBox.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 547);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1051, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // speechVisualizer
            // 
            this.speechVisualizer.BackColor = System.Drawing.Color.Black;
            this.speechVisualizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speechVisualizer.Location = new System.Drawing.Point(0, 99);
            this.speechVisualizer.MarkerList = null;
            this.speechVisualizer.Name = "speechVisualizer";
            this.speechVisualizer.PitchPanelVisible = true;
            this.speechVisualizer.Size = new System.Drawing.Size(1051, 448);
            this.speechVisualizer.TabIndex = 6;
            // 
            // VoiceModificationMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 569);
            this.Controls.Add(this.speechVisualizer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "VoiceModificationMainForm";
            this.Text = "Voice modification";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox voiceSelectionComboBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox sentenceTextBox;
        private System.Windows.Forms.ToolStripButton speakButton;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton soundTypeIdentificationButton;
        private System.Windows.Forms.ToolStripButton findPitchPeriodsButton;
        private System.Windows.Forms.ToolStripButton findPitchMarksButton;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton modifySoundButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private SpeechSynthesisLibrary.Visualization.SpeechVisualizer speechVisualizer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox relativeStartPitchTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox relativeEndPitchTextBox;
        private System.Windows.Forms.ToolStripComboBox adjustDurationComboBox;
        private System.Windows.Forms.ToolStripLabel relativeDurationLabel;
        private System.Windows.Forms.ToolStripTextBox relativeDurationTextBox;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parameterSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox topFractionTextBox;
        private System.Windows.Forms.ToolStripMenuItem saveSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripButton playSoundButton;
    }
}