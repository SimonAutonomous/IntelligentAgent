namespace SpeechRecognitionApplication
{
    partial class SpeechRecognitionMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeechRecognitionMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.recordingButton = new System.Windows.Forms.ToolStripButton();
            this.recognizeButton = new System.Windows.Forms.ToolStripButton();
            this.recognitionResultTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.speechRecognitionTabPage = new System.Windows.Forms.TabPage();
            this.grammarTabPage = new System.Windows.Forms.TabPage();
            this.soundVisualizer = new AudioLibrary.Visualization.SoundVisualizer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.grammarPhraseTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.addGrammarPhraseButton = new System.Windows.Forms.ToolStripButton();
            this.grammarPhraseListBox = new System.Windows.Forms.ListBox();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.speechRecognitionTabPage.SuspendLayout();
            this.grammarTabPage.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(900, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordingButton,
            this.recognizeButton,
            this.recognitionResultTextBox});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(886, 25);
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
            this.recordingButton.Click += new System.EventHandler(this.startRecordingButton_Click);
            // 
            // recognizeButton
            // 
            this.recognizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.recognizeButton.Enabled = false;
            this.recognizeButton.Image = ((System.Drawing.Image)(resources.GetObject("recognizeButton.Image")));
            this.recognizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recognizeButton.Name = "recognizeButton";
            this.recognizeButton.Size = new System.Drawing.Size(65, 22);
            this.recognizeButton.Text = "Recognize";
            this.recognizeButton.Click += new System.EventHandler(this.recognizeButton_Click);
            // 
            // recognitionResultTextBox
            // 
            this.recognitionResultTextBox.Name = "recognitionResultTextBox";
            this.recognitionResultTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.speechRecognitionTabPage);
            this.mainTabControl.Controls.Add(this.grammarTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 24);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(900, 480);
            this.mainTabControl.TabIndex = 3;
            // 
            // speechRecognitionTabPage
            // 
            this.speechRecognitionTabPage.Controls.Add(this.soundVisualizer);
            this.speechRecognitionTabPage.Controls.Add(this.toolStrip1);
            this.speechRecognitionTabPage.Location = new System.Drawing.Point(4, 22);
            this.speechRecognitionTabPage.Name = "speechRecognitionTabPage";
            this.speechRecognitionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.speechRecognitionTabPage.Size = new System.Drawing.Size(892, 454);
            this.speechRecognitionTabPage.TabIndex = 0;
            this.speechRecognitionTabPage.Text = "Speech recognition";
            this.speechRecognitionTabPage.UseVisualStyleBackColor = true;
            // 
            // grammarTabPage
            // 
            this.grammarTabPage.Controls.Add(this.grammarPhraseListBox);
            this.grammarTabPage.Controls.Add(this.toolStrip2);
            this.grammarTabPage.Location = new System.Drawing.Point(4, 22);
            this.grammarTabPage.Name = "grammarTabPage";
            this.grammarTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.grammarTabPage.Size = new System.Drawing.Size(892, 454);
            this.grammarTabPage.TabIndex = 1;
            this.grammarTabPage.Text = "Grammar";
            this.grammarTabPage.UseVisualStyleBackColor = true;
            // 
            // soundVisualizer
            // 
            this.soundVisualizer.BackColor = System.Drawing.Color.Black;
            this.soundVisualizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soundVisualizer.Location = new System.Drawing.Point(3, 28);
            this.soundVisualizer.MarkerList = null;
            this.soundVisualizer.Name = "soundVisualizer";
            this.soundVisualizer.Size = new System.Drawing.Size(886, 423);
            this.soundVisualizer.TabIndex = 3;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grammarPhraseTextBox,
            this.addGrammarPhraseButton});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(886, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // grammarPhraseTextBox
            // 
            this.grammarPhraseTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grammarPhraseTextBox.Name = "grammarPhraseTextBox";
            this.grammarPhraseTextBox.Size = new System.Drawing.Size(400, 25);
            // 
            // addGrammarPhraseButton
            // 
            this.addGrammarPhraseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addGrammarPhraseButton.Image = ((System.Drawing.Image)(resources.GetObject("addGrammarPhraseButton.Image")));
            this.addGrammarPhraseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addGrammarPhraseButton.Name = "addGrammarPhraseButton";
            this.addGrammarPhraseButton.Size = new System.Drawing.Size(33, 22);
            this.addGrammarPhraseButton.Text = "Add";
            this.addGrammarPhraseButton.Click += new System.EventHandler(this.addGrammarPhraseButton_Click);
            // 
            // grammarPhraseListBox
            // 
            this.grammarPhraseListBox.BackColor = System.Drawing.Color.Black;
            this.grammarPhraseListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grammarPhraseListBox.ForeColor = System.Drawing.Color.Lime;
            this.grammarPhraseListBox.FormattingEnabled = true;
            this.grammarPhraseListBox.IntegralHeight = false;
            this.grammarPhraseListBox.Items.AddRange(new object[] {
            "yes",
            "no"});
            this.grammarPhraseListBox.Location = new System.Drawing.Point(3, 28);
            this.grammarPhraseListBox.Name = "grammarPhraseListBox";
            this.grammarPhraseListBox.Size = new System.Drawing.Size(886, 423);
            this.grammarPhraseListBox.TabIndex = 1;
            this.grammarPhraseListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grammarPhraseListBox_MouseDoubleClick);
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
            // SpeechRecognitionMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 504);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SpeechRecognitionMainForm";
            this.Text = "Microsoft speech recognizer demonstration, v1.0";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.speechRecognitionTabPage.ResumeLayout(false);
            this.speechRecognitionTabPage.PerformLayout();
            this.grammarTabPage.ResumeLayout(false);
            this.grammarTabPage.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton recordingButton;
        private System.Windows.Forms.ToolStripButton recognizeButton;
        private System.Windows.Forms.ToolStripTextBox recognitionResultTextBox;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage speechRecognitionTabPage;
        private AudioLibrary.Visualization.SoundVisualizer soundVisualizer;
        private System.Windows.Forms.TabPage grammarTabPage;
        private System.Windows.Forms.ListBox grammarPhraseListBox;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripTextBox grammarPhraseTextBox;
        private System.Windows.Forms.ToolStripButton addGrammarPhraseButton;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}