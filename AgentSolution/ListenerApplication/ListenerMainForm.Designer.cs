namespace ListenerApplication
{
    partial class ListenerMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListenerMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.communicationLogTabPage = new System.Windows.Forms.TabPage();
            this.communicationLogColorListBox = new CustomUserControlsLibrary.ColorListBox();
            this.inputTabPage = new System.Windows.Forms.TabPage();
            this.inputSplitContainer = new System.Windows.Forms.SplitContainer();
            this.inputMessageColorListBox = new CustomUserControlsLibrary.ColorListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.recordingButton = new System.Windows.Forms.ToolStripButton();
            this.recognizeButton = new System.Windows.Forms.ToolStripButton();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.continuousInputTabPage = new System.Windows.Forms.TabPage();
            this.continuousInputSplitContainer = new System.Windows.Forms.SplitContainer();
            this.continuousInputColorListBox = new CustomUserControlsLibrary.ColorListBox();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.detectionThresholdTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.recordingIntervalLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.continuousInputButton = new System.Windows.Forms.ToolStripButton();
            this.continuousInputStopButton = new System.Windows.Forms.ToolStripButton();
            this.continuousDisplaySettingsItem = new System.Windows.Forms.ToolStripDropDownButton();
            this.showSoundStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractDetectedSoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.recordingDeviceComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.grammarTabPage = new System.Windows.Forms.TabPage();
            this.grammarPhraseListBox = new System.Windows.Forms.ListBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.grammarPhraseTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.addToGrammarButton = new System.Windows.Forms.ToolStripButton();
            this.soundVisualizer = new AudioLibrary.Visualization.SoundVisualizer();
            this.continuousSpeechRecognitionControl = new SpeechRecognitionLibrary.Visualization.ContinuousSpeechRecognitionControl();
            this.detectedSoundVisualizer = new AudioLibrary.Visualization.SoundVisualizer();
            this.menuStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.communicationLogTabPage.SuspendLayout();
            this.inputTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputSplitContainer)).BeginInit();
            this.inputSplitContainer.Panel1.SuspendLayout();
            this.inputSplitContainer.Panel2.SuspendLayout();
            this.inputSplitContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.continuousInputTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.continuousInputSplitContainer)).BeginInit();
            this.continuousInputSplitContainer.Panel1.SuspendLayout();
            this.continuousInputSplitContainer.Panel2.SuspendLayout();
            this.continuousInputSplitContainer.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.grammarTabPage.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(652, 24);
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
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.communicationLogTabPage);
            this.mainTabControl.Controls.Add(this.inputTabPage);
            this.mainTabControl.Controls.Add(this.continuousInputTabPage);
            this.mainTabControl.Controls.Add(this.grammarTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 24);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(652, 431);
            this.mainTabControl.TabIndex = 1;
            // 
            // communicationLogTabPage
            // 
            this.communicationLogTabPage.Controls.Add(this.communicationLogColorListBox);
            this.communicationLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.communicationLogTabPage.Name = "communicationLogTabPage";
            this.communicationLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.communicationLogTabPage.Size = new System.Drawing.Size(644, 405);
            this.communicationLogTabPage.TabIndex = 0;
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
            this.communicationLogColorListBox.Size = new System.Drawing.Size(638, 399);
            this.communicationLogColorListBox.TabIndex = 0;
            // 
            // inputTabPage
            // 
            this.inputTabPage.Controls.Add(this.inputSplitContainer);
            this.inputTabPage.Controls.Add(this.toolStrip1);
            this.inputTabPage.Controls.Add(this.inputTextBox);
            this.inputTabPage.Location = new System.Drawing.Point(4, 22);
            this.inputTabPage.Name = "inputTabPage";
            this.inputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inputTabPage.Size = new System.Drawing.Size(644, 405);
            this.inputTabPage.TabIndex = 1;
            this.inputTabPage.Text = "Input";
            this.inputTabPage.UseVisualStyleBackColor = true;
            // 
            // inputSplitContainer
            // 
            this.inputSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputSplitContainer.Location = new System.Drawing.Point(3, 54);
            this.inputSplitContainer.Name = "inputSplitContainer";
            this.inputSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // inputSplitContainer.Panel1
            // 
            this.inputSplitContainer.Panel1.Controls.Add(this.inputMessageColorListBox);
            // 
            // inputSplitContainer.Panel2
            // 
            this.inputSplitContainer.Panel2.Controls.Add(this.soundVisualizer);
            this.inputSplitContainer.Size = new System.Drawing.Size(638, 348);
            this.inputSplitContainer.SplitterDistance = 209;
            this.inputSplitContainer.TabIndex = 5;
            // 
            // inputMessageColorListBox
            // 
            this.inputMessageColorListBox.BackColor = System.Drawing.Color.Black;
            this.inputMessageColorListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputMessageColorListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputMessageColorListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.inputMessageColorListBox.ForeColor = System.Drawing.Color.Lime;
            this.inputMessageColorListBox.FormattingEnabled = true;
            this.inputMessageColorListBox.Location = new System.Drawing.Point(0, 0);
            this.inputMessageColorListBox.Name = "inputMessageColorListBox";
            this.inputMessageColorListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.inputMessageColorListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.inputMessageColorListBox.Size = new System.Drawing.Size(638, 209);
            this.inputMessageColorListBox.TabIndex = 6;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordingButton,
            this.recognizeButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 29);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(638, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // recordingButton
            // 
            this.recordingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.recordingButton.Image = ((System.Drawing.Image)(resources.GetObject("recordingButton.Image")));
            this.recordingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recordingButton.Name = "recordingButton";
            this.recordingButton.Size = new System.Drawing.Size(35, 22);
            this.recordingButton.Text = "Start";
            this.recordingButton.Click += new System.EventHandler(this.recordingButton_Click);
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
            // inputTextBox
            // 
            this.inputTextBox.BackColor = System.Drawing.Color.Silver;
            this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.inputTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputTextBox.Location = new System.Drawing.Point(3, 3);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(638, 26);
            this.inputTextBox.TabIndex = 1;
            this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTextBox_KeyDown);
            // 
            // continuousInputTabPage
            // 
            this.continuousInputTabPage.Controls.Add(this.continuousInputSplitContainer);
            this.continuousInputTabPage.Controls.Add(this.statusStrip1);
            this.continuousInputTabPage.Controls.Add(this.toolStrip2);
            this.continuousInputTabPage.Location = new System.Drawing.Point(4, 22);
            this.continuousInputTabPage.Name = "continuousInputTabPage";
            this.continuousInputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.continuousInputTabPage.Size = new System.Drawing.Size(644, 405);
            this.continuousInputTabPage.TabIndex = 3;
            this.continuousInputTabPage.Text = "Continuous input";
            this.continuousInputTabPage.UseVisualStyleBackColor = true;
            // 
            // continuousInputSplitContainer
            // 
            this.continuousInputSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.continuousInputSplitContainer.Location = new System.Drawing.Point(3, 28);
            this.continuousInputSplitContainer.Name = "continuousInputSplitContainer";
            this.continuousInputSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // continuousInputSplitContainer.Panel1
            // 
            this.continuousInputSplitContainer.Panel1.Controls.Add(this.continuousInputColorListBox);
            this.continuousInputSplitContainer.Panel1.Controls.Add(this.toolStrip4);
            // 
            // continuousInputSplitContainer.Panel2
            // 
            this.continuousInputSplitContainer.Panel2.Controls.Add(this.splitContainer1);
            this.continuousInputSplitContainer.Size = new System.Drawing.Size(638, 352);
            this.continuousInputSplitContainer.SplitterDistance = 157;
            this.continuousInputSplitContainer.TabIndex = 2;
            // 
            // continuousInputColorListBox
            // 
            this.continuousInputColorListBox.BackColor = System.Drawing.Color.Black;
            this.continuousInputColorListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.continuousInputColorListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.continuousInputColorListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.continuousInputColorListBox.ForeColor = System.Drawing.Color.Lime;
            this.continuousInputColorListBox.FormattingEnabled = true;
            this.continuousInputColorListBox.Location = new System.Drawing.Point(0, 25);
            this.continuousInputColorListBox.Name = "continuousInputColorListBox";
            this.continuousInputColorListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.continuousInputColorListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.continuousInputColorListBox.Size = new System.Drawing.Size(638, 132);
            this.continuousInputColorListBox.TabIndex = 8;
            // 
            // toolStrip4
            // 
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.detectionThresholdTextBox});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(638, 25);
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(117, 22);
            this.toolStripLabel2.Text = "Detection threshold: ";
            // 
            // detectionThresholdTextBox
            // 
            this.detectionThresholdTextBox.Name = "detectionThresholdTextBox";
            this.detectionThresholdTextBox.Size = new System.Drawing.Size(60, 25);
            this.detectionThresholdTextBox.Text = "300";
            this.detectionThresholdTextBox.TextChanged += new System.EventHandler(this.detectionThresholdTextBox_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.continuousSpeechRecognitionControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.detectedSoundVisualizer);
            this.splitContainer1.Size = new System.Drawing.Size(638, 191);
            this.splitContainer1.SplitterDistance = 308;
            this.splitContainer1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.recordingIntervalLabel});
            this.statusStrip1.Location = new System.Drawing.Point(3, 380);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(638, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel1.Text = "Recording interval: ";
            // 
            // recordingIntervalLabel
            // 
            this.recordingIntervalLabel.Name = "recordingIntervalLabel";
            this.recordingIntervalLabel.Size = new System.Drawing.Size(29, 17);
            this.recordingIntervalLabel.Text = "N/A";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.continuousInputButton,
            this.continuousInputStopButton,
            this.continuousDisplaySettingsItem,
            this.toolStripLabel1,
            this.recordingDeviceComboBox});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(638, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // continuousInputButton
            // 
            this.continuousInputButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.continuousInputButton.Image = ((System.Drawing.Image)(resources.GetObject("continuousInputButton.Image")));
            this.continuousInputButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.continuousInputButton.Name = "continuousInputButton";
            this.continuousInputButton.Size = new System.Drawing.Size(35, 22);
            this.continuousInputButton.Text = "Start";
            this.continuousInputButton.Click += new System.EventHandler(this.continuousInputButton_Click);
            // 
            // continuousInputStopButton
            // 
            this.continuousInputStopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.continuousInputStopButton.Enabled = false;
            this.continuousInputStopButton.Image = ((System.Drawing.Image)(resources.GetObject("continuousInputStopButton.Image")));
            this.continuousInputStopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.continuousInputStopButton.Name = "continuousInputStopButton";
            this.continuousInputStopButton.Size = new System.Drawing.Size(35, 22);
            this.continuousInputStopButton.Text = "Stop";
            this.continuousInputStopButton.Click += new System.EventHandler(this.continuousInputStopButton_Click);
            // 
            // continuousDisplaySettingsItem
            // 
            this.continuousDisplaySettingsItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.continuousDisplaySettingsItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showSoundStreamToolStripMenuItem,
            this.extractDetectedSoundsToolStripMenuItem});
            this.continuousDisplaySettingsItem.Image = ((System.Drawing.Image)(resources.GetObject("continuousDisplaySettingsItem.Image")));
            this.continuousDisplaySettingsItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.continuousDisplaySettingsItem.Name = "continuousDisplaySettingsItem";
            this.continuousDisplaySettingsItem.Size = new System.Drawing.Size(62, 22);
            this.continuousDisplaySettingsItem.Text = "Settings";
            // 
            // showSoundStreamToolStripMenuItem
            // 
            this.showSoundStreamToolStripMenuItem.CheckOnClick = true;
            this.showSoundStreamToolStripMenuItem.Name = "showSoundStreamToolStripMenuItem";
            this.showSoundStreamToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.showSoundStreamToolStripMenuItem.Text = "Show sound stream";
            this.showSoundStreamToolStripMenuItem.Click += new System.EventHandler(this.showSoundStreamToolStripMenuItem_Click);
            // 
            // extractDetectedSoundsToolStripMenuItem
            // 
            this.extractDetectedSoundsToolStripMenuItem.CheckOnClick = true;
            this.extractDetectedSoundsToolStripMenuItem.Name = "extractDetectedSoundsToolStripMenuItem";
            this.extractDetectedSoundsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.extractDetectedSoundsToolStripMenuItem.Text = "Show detected sounds";
            this.extractDetectedSoundsToolStripMenuItem.Click += new System.EventHandler(this.extractDetectedSoundsToolStripMenuItem_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(101, 22);
            this.toolStripLabel1.Text = "Recording device:";
            // 
            // recordingDeviceComboBox
            // 
            this.recordingDeviceComboBox.Name = "recordingDeviceComboBox";
            this.recordingDeviceComboBox.Size = new System.Drawing.Size(141, 25);
            // 
            // grammarTabPage
            // 
            this.grammarTabPage.Controls.Add(this.grammarPhraseListBox);
            this.grammarTabPage.Controls.Add(this.toolStrip3);
            this.grammarTabPage.Location = new System.Drawing.Point(4, 22);
            this.grammarTabPage.Name = "grammarTabPage";
            this.grammarTabPage.Size = new System.Drawing.Size(644, 405);
            this.grammarTabPage.TabIndex = 2;
            this.grammarTabPage.Text = "Grammar";
            this.grammarTabPage.UseVisualStyleBackColor = true;
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
            "no",
            "back",
            "next",
            "end",
            "what time is it",
            "what is your name",
            "Hazel",
            "my name is simon",
            "i am simon",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.grammarPhraseListBox.Location = new System.Drawing.Point(0, 25);
            this.grammarPhraseListBox.Name = "grammarPhraseListBox";
            this.grammarPhraseListBox.Size = new System.Drawing.Size(644, 380);
            this.grammarPhraseListBox.TabIndex = 4;
            this.grammarPhraseListBox.SelectedIndexChanged += new System.EventHandler(this.grammarPhraseListBox_SelectedIndexChanged);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grammarPhraseTextBox,
            this.addToGrammarButton});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(644, 25);
            this.toolStrip3.TabIndex = 3;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // grammarPhraseTextBox
            // 
            this.grammarPhraseTextBox.Name = "grammarPhraseTextBox";
            this.grammarPhraseTextBox.Size = new System.Drawing.Size(200, 25);
            // 
            // addToGrammarButton
            // 
            this.addToGrammarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addToGrammarButton.Image = ((System.Drawing.Image)(resources.GetObject("addToGrammarButton.Image")));
            this.addToGrammarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addToGrammarButton.Name = "addToGrammarButton";
            this.addToGrammarButton.Size = new System.Drawing.Size(33, 22);
            this.addToGrammarButton.Text = "Add";
            this.addToGrammarButton.Click += new System.EventHandler(this.addToGrammarButton_Click);
            // 
            // soundVisualizer
            // 
            this.soundVisualizer.BackColor = System.Drawing.Color.Black;
            this.soundVisualizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soundVisualizer.Location = new System.Drawing.Point(0, 0);
            this.soundVisualizer.MarkerList = null;
            this.soundVisualizer.Name = "soundVisualizer";
            this.soundVisualizer.Size = new System.Drawing.Size(638, 135);
            this.soundVisualizer.TabIndex = 0;
            // 
            // continuousSpeechRecognitionControl
            // 
            this.continuousSpeechRecognitionControl.BackColor = System.Drawing.Color.Black;
            this.continuousSpeechRecognitionControl.DetectionThreshold = 300;
            this.continuousSpeechRecognitionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.continuousSpeechRecognitionControl.ExtractDetectedSounds = true;
            this.continuousSpeechRecognitionControl.Location = new System.Drawing.Point(0, 0);
            this.continuousSpeechRecognitionControl.MarkerList = null;
            this.continuousSpeechRecognitionControl.Name = "continuousSpeechRecognitionControl";
            this.continuousSpeechRecognitionControl.RecordingDeviceID = 0;
            this.continuousSpeechRecognitionControl.ShowSoundStream = true;
            this.continuousSpeechRecognitionControl.Size = new System.Drawing.Size(308, 191);
            this.continuousSpeechRecognitionControl.TabIndex = 1;
            // 
            // detectedSoundVisualizer
            // 
            this.detectedSoundVisualizer.BackColor = System.Drawing.Color.Black;
            this.detectedSoundVisualizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detectedSoundVisualizer.Location = new System.Drawing.Point(0, 0);
            this.detectedSoundVisualizer.MarkerList = null;
            this.detectedSoundVisualizer.Name = "detectedSoundVisualizer";
            this.detectedSoundVisualizer.Size = new System.Drawing.Size(326, 191);
            this.detectedSoundVisualizer.TabIndex = 0;
            // 
            // ListenerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 455);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ListenerMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listener";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ListenerMainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.communicationLogTabPage.ResumeLayout(false);
            this.inputTabPage.ResumeLayout(false);
            this.inputTabPage.PerformLayout();
            this.inputSplitContainer.Panel1.ResumeLayout(false);
            this.inputSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputSplitContainer)).EndInit();
            this.inputSplitContainer.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.continuousInputTabPage.ResumeLayout(false);
            this.continuousInputTabPage.PerformLayout();
            this.continuousInputSplitContainer.Panel1.ResumeLayout(false);
            this.continuousInputSplitContainer.Panel1.PerformLayout();
            this.continuousInputSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.continuousInputSplitContainer)).EndInit();
            this.continuousInputSplitContainer.ResumeLayout(false);
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.grammarTabPage.ResumeLayout(false);
            this.grammarTabPage.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage communicationLogTabPage;
        private CustomUserControlsLibrary.ColorListBox communicationLogColorListBox;
        private System.Windows.Forms.TabPage inputTabPage;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.TabPage grammarTabPage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton recordingButton;
        private System.Windows.Forms.ToolStripButton recognizeButton;
        private System.Windows.Forms.SplitContainer inputSplitContainer;
        private CustomUserControlsLibrary.ColorListBox inputMessageColorListBox;
        private AudioLibrary.Visualization.SoundVisualizer soundVisualizer;
        private System.Windows.Forms.TabPage continuousInputTabPage;
        private System.Windows.Forms.SplitContainer continuousInputSplitContainer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton continuousInputButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel recordingIntervalLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private SpeechRecognitionLibrary.Visualization.ContinuousSpeechRecognitionControl continuousSpeechRecognitionControl;
        private AudioLibrary.Visualization.SoundVisualizer detectedSoundVisualizer;
        private System.Windows.Forms.ToolStripDropDownButton continuousDisplaySettingsItem;
        private System.Windows.Forms.ToolStripMenuItem showSoundStreamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractDetectedSoundsToolStripMenuItem;
        private System.Windows.Forms.ListBox grammarPhraseListBox;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripTextBox grammarPhraseTextBox;
        private System.Windows.Forms.ToolStripButton addToGrammarButton;
        private System.Windows.Forms.ToolStripButton continuousInputStopButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox recordingDeviceComboBox;
        private CustomUserControlsLibrary.ColorListBox continuousInputColorListBox;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox detectionThresholdTextBox;
    }
}

