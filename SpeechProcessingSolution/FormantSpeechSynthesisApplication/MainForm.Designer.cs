namespace FormantSpeechSynthesisApplication
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
            this.saveSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.newSpeechSynthesizerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSpeechSynthesizerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSpeechSynthesizerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.editorTabPage = new System.Windows.Forms.TabPage();
            this.editorMainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.editorSecondarySplitContainer = new System.Windows.Forms.SplitContainer();
            this.formantSpecificationListBox = new System.Windows.Forms.ListBox();
            this.formantSettingsEditor = new SpeechSynthesisLibrary.FormantSynthesis.FormantSettingsEditor();
            this.editorSpeechVisualizer = new SpeechSynthesisLibrary.Visualization.SpeechVisualizer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.editorRandomizeButton = new System.Windows.Forms.ToolStripButton();
            this.editorPlayButton = new System.Windows.Forms.ToolStripButton();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            this.setUnvoicedButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.fundamentalFrequencyTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.samplingFrequencyTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.insertEndPointSilenceButton = new System.Windows.Forms.ToolStripButton();
            this.endPointSilenceDurationLabel = new System.Windows.Forms.ToolStripLabel();
            this.endPointSilenceDurationTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.optimizerTabPage = new System.Windows.Forms.TabPage();
            this.optimizerTabControl = new System.Windows.Forms.TabControl();
            this.allSoundsOptimizationTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.ieaFormantSettingsEditor = new SpeechSynthesisLibrary.FormantSynthesis.FormantSettingsEditor();
            this.ieaFormantSettingsListListBox = new System.Windows.Forms.ListBox();
            this.soundVisualizer3x3 = new AudioLibrary.Visualization.SoundVisualizer3x3();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.assignCurrentSoundButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.relativeModificationRangeTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.modificationScopeDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.modificationSettingsDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.modifySinusoidsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyVoicedFractionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyDurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyTransitionStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyAmplitudeVariationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyPitchVariationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.synthesizerTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.synthesizerSpecificationListBox = new System.Windows.Forms.ListBox();
            this.wordToSoundMappingEditor = new SpeechSynthesisLibrary.FormantSynthesis.WordToSoundMappingEditor();
            this.sentenceSpeechVisualizer = new SpeechSynthesisLibrary.Visualization.SpeechVisualizer();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel9 = new System.Windows.Forms.ToolStripLabel();
            this.soundNameTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.addToSynthesizerButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.volumeLabel = new System.Windows.Forms.ToolStripLabel();
            this.volumeTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.sentenceToSpeakTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.speakSentenceButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.editorTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorMainSplitContainer)).BeginInit();
            this.editorMainSplitContainer.Panel1.SuspendLayout();
            this.editorMainSplitContainer.Panel2.SuspendLayout();
            this.editorMainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorSecondarySplitContainer)).BeginInit();
            this.editorSecondarySplitContainer.Panel1.SuspendLayout();
            this.editorSecondarySplitContainer.Panel2.SuspendLayout();
            this.editorSecondarySplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.formantSettingsEditor)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.optimizerTabPage.SuspendLayout();
            this.optimizerTabControl.SuspendLayout();
            this.allSoundsOptimizationTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ieaFormantSettingsEditor)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.synthesizerTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wordToSoundMappingEditor)).BeginInit();
            this.toolStrip5.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1202, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSoundToolStripMenuItem,
            this.toolStripSeparator8,
            this.newSpeechSynthesizerToolStripMenuItem,
            this.loadSpeechSynthesizerToolStripMenuItem,
            this.saveSpeechSynthesizerToolStripMenuItem,
            this.toolStripSeparator7,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveSoundToolStripMenuItem
            // 
            this.saveSoundToolStripMenuItem.Enabled = false;
            this.saveSoundToolStripMenuItem.Name = "saveSoundToolStripMenuItem";
            this.saveSoundToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.saveSoundToolStripMenuItem.Text = "Save sound";
            this.saveSoundToolStripMenuItem.Click += new System.EventHandler(this.saveSoundToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(198, 6);
            // 
            // newSpeechSynthesizerToolStripMenuItem
            // 
            this.newSpeechSynthesizerToolStripMenuItem.Name = "newSpeechSynthesizerToolStripMenuItem";
            this.newSpeechSynthesizerToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.newSpeechSynthesizerToolStripMenuItem.Text = "New speech synthesizer";
            this.newSpeechSynthesizerToolStripMenuItem.Click += new System.EventHandler(this.newSpeechSynthesizerToolStripMenuItem_Click);
            // 
            // loadSpeechSynthesizerToolStripMenuItem
            // 
            this.loadSpeechSynthesizerToolStripMenuItem.Name = "loadSpeechSynthesizerToolStripMenuItem";
            this.loadSpeechSynthesizerToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.loadSpeechSynthesizerToolStripMenuItem.Text = "Load speech synthesizer";
            this.loadSpeechSynthesizerToolStripMenuItem.Click += new System.EventHandler(this.loadSpeechSynthesizerToolStripMenuItem_Click);
            // 
            // saveSpeechSynthesizerToolStripMenuItem
            // 
            this.saveSpeechSynthesizerToolStripMenuItem.Enabled = false;
            this.saveSpeechSynthesizerToolStripMenuItem.Name = "saveSpeechSynthesizerToolStripMenuItem";
            this.saveSpeechSynthesizerToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.saveSpeechSynthesizerToolStripMenuItem.Text = "Save speech synthesizer";
            this.saveSpeechSynthesizerToolStripMenuItem.Click += new System.EventHandler(this.saveSpeechSynthesizerToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(198, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.editorTabPage);
            this.mainTabControl.Controls.Add(this.optimizerTabPage);
            this.mainTabControl.Controls.Add(this.synthesizerTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 24);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1202, 591);
            this.mainTabControl.TabIndex = 1;
            // 
            // editorTabPage
            // 
            this.editorTabPage.Controls.Add(this.editorMainSplitContainer);
            this.editorTabPage.Controls.Add(this.toolStrip1);
            this.editorTabPage.Controls.Add(this.statusStrip1);
            this.editorTabPage.Location = new System.Drawing.Point(4, 22);
            this.editorTabPage.Name = "editorTabPage";
            this.editorTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.editorTabPage.Size = new System.Drawing.Size(1194, 565);
            this.editorTabPage.TabIndex = 0;
            this.editorTabPage.Text = "Editor";
            this.editorTabPage.UseVisualStyleBackColor = true;
            // 
            // editorMainSplitContainer
            // 
            this.editorMainSplitContainer.BackColor = System.Drawing.Color.Black;
            this.editorMainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorMainSplitContainer.Location = new System.Drawing.Point(3, 28);
            this.editorMainSplitContainer.Name = "editorMainSplitContainer";
            // 
            // editorMainSplitContainer.Panel1
            // 
            this.editorMainSplitContainer.Panel1.Controls.Add(this.editorSecondarySplitContainer);
            // 
            // editorMainSplitContainer.Panel2
            // 
            this.editorMainSplitContainer.Panel2.Controls.Add(this.editorSpeechVisualizer);
            this.editorMainSplitContainer.Size = new System.Drawing.Size(1188, 512);
            this.editorMainSplitContainer.SplitterDistance = 347;
            this.editorMainSplitContainer.TabIndex = 3;
            this.editorMainSplitContainer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.formantSpecificationListBox_MouseDoubleClick);
            this.editorMainSplitContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formantSpecificationListBox_MouseDown);
            // 
            // editorSecondarySplitContainer
            // 
            this.editorSecondarySplitContainer.BackColor = System.Drawing.Color.Black;
            this.editorSecondarySplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorSecondarySplitContainer.Location = new System.Drawing.Point(0, 0);
            this.editorSecondarySplitContainer.Name = "editorSecondarySplitContainer";
            this.editorSecondarySplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // editorSecondarySplitContainer.Panel1
            // 
            this.editorSecondarySplitContainer.Panel1.BackColor = System.Drawing.Color.DimGray;
            this.editorSecondarySplitContainer.Panel1.Controls.Add(this.formantSpecificationListBox);
            // 
            // editorSecondarySplitContainer.Panel2
            // 
            this.editorSecondarySplitContainer.Panel2.Controls.Add(this.formantSettingsEditor);
            this.editorSecondarySplitContainer.Size = new System.Drawing.Size(347, 512);
            this.editorSecondarySplitContainer.SplitterDistance = 66;
            this.editorSecondarySplitContainer.TabIndex = 0;
            this.editorSecondarySplitContainer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.formantSpecificationListBox_MouseDoubleClick);
            this.editorSecondarySplitContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formantSpecificationListBox_MouseDown);
            // 
            // formantSpecificationListBox
            // 
            this.formantSpecificationListBox.BackColor = System.Drawing.Color.DimGray;
            this.formantSpecificationListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.formantSpecificationListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formantSpecificationListBox.ForeColor = System.Drawing.Color.White;
            this.formantSpecificationListBox.FormattingEnabled = true;
            this.formantSpecificationListBox.Location = new System.Drawing.Point(0, 0);
            this.formantSpecificationListBox.Name = "formantSpecificationListBox";
            this.formantSpecificationListBox.Size = new System.Drawing.Size(347, 66);
            this.formantSpecificationListBox.TabIndex = 0;
            this.formantSpecificationListBox.SelectedIndexChanged += new System.EventHandler(this.formantSpecificationListBox_SelectedIndexChanged);
            this.formantSpecificationListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.formantSpecificationListBox_MouseDoubleClick);
            this.formantSpecificationListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formantSpecificationListBox_MouseDown);
            // 
            // formantSettingsEditor
            // 
            this.formantSettingsEditor.BackgroundColor = System.Drawing.Color.DimGray;
            this.formantSettingsEditor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.formantSettingsEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formantSettingsEditor.Location = new System.Drawing.Point(0, 0);
            this.formantSettingsEditor.Name = "formantSettingsEditor";
            this.formantSettingsEditor.Size = new System.Drawing.Size(347, 442);
            this.formantSettingsEditor.TabIndex = 0;
            // 
            // editorSpeechVisualizer
            // 
            this.editorSpeechVisualizer.BackColor = System.Drawing.Color.Black;
            this.editorSpeechVisualizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorSpeechVisualizer.Location = new System.Drawing.Point(0, 0);
            this.editorSpeechVisualizer.MarkerList = null;
            this.editorSpeechVisualizer.Name = "editorSpeechVisualizer";
            this.editorSpeechVisualizer.PitchPanelVisible = true;
            this.editorSpeechVisualizer.Size = new System.Drawing.Size(837, 512);
            this.editorSpeechVisualizer.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editorRandomizeButton,
            this.editorPlayButton,
            this.clearButton,
            this.setUnvoicedButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.fundamentalFrequencyTextBox,
            this.toolStripLabel2,
            this.samplingFrequencyTextBox,
            this.insertEndPointSilenceButton,
            this.endPointSilenceDurationLabel,
            this.endPointSilenceDurationTextBox});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1188, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // editorRandomizeButton
            // 
            this.editorRandomizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editorRandomizeButton.Image = ((System.Drawing.Image)(resources.GetObject("editorRandomizeButton.Image")));
            this.editorRandomizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editorRandomizeButton.Name = "editorRandomizeButton";
            this.editorRandomizeButton.Size = new System.Drawing.Size(70, 22);
            this.editorRandomizeButton.Text = "Randomize";
            this.editorRandomizeButton.Click += new System.EventHandler(this.editorRandomizeButton_Click);
            // 
            // editorPlayButton
            // 
            this.editorPlayButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editorPlayButton.Enabled = false;
            this.editorPlayButton.Image = ((System.Drawing.Image)(resources.GetObject("editorPlayButton.Image")));
            this.editorPlayButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editorPlayButton.Name = "editorPlayButton";
            this.editorPlayButton.Size = new System.Drawing.Size(33, 22);
            this.editorPlayButton.Text = "Play";
            this.editorPlayButton.Click += new System.EventHandler(this.editorPlayButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearButton.Enabled = false;
            this.clearButton.Image = ((System.Drawing.Image)(resources.GetObject("clearButton.Image")));
            this.clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(38, 22);
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // setUnvoicedButton
            // 
            this.setUnvoicedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.setUnvoicedButton.Enabled = false;
            this.setUnvoicedButton.Image = ((System.Drawing.Image)(resources.GetObject("setUnvoicedButton.Image")));
            this.setUnvoicedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.setUnvoicedButton.Name = "setUnvoicedButton";
            this.setUnvoicedButton.Size = new System.Drawing.Size(79, 22);
            this.setUnvoicedButton.Text = "Set unvoiced";
            this.setUnvoicedButton.Click += new System.EventHandler(this.setUnvoicedButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(136, 22);
            this.toolStripLabel1.Text = "Fundamental frequency:";
            // 
            // fundamentalFrequencyTextBox
            // 
            this.fundamentalFrequencyTextBox.Name = "fundamentalFrequencyTextBox";
            this.fundamentalFrequencyTextBox.Size = new System.Drawing.Size(60, 25);
            this.fundamentalFrequencyTextBox.Leave += new System.EventHandler(this.fundamentalFrequencyTextBox_Leave);
            this.fundamentalFrequencyTextBox.TextChanged += new System.EventHandler(this.fundamentalFrequencyTextBox_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(116, 22);
            this.toolStripLabel2.Text = "Sampling frequency:";
            // 
            // samplingFrequencyTextBox
            // 
            this.samplingFrequencyTextBox.Name = "samplingFrequencyTextBox";
            this.samplingFrequencyTextBox.Size = new System.Drawing.Size(60, 25);
            this.samplingFrequencyTextBox.Leave += new System.EventHandler(this.samplingFrequencyTextBox_Leave);
            this.samplingFrequencyTextBox.TextChanged += new System.EventHandler(this.samplingFrequencyTextBox_TextChanged);
            // 
            // insertEndPointSilenceButton
            // 
            this.insertEndPointSilenceButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.insertEndPointSilenceButton.Enabled = false;
            this.insertEndPointSilenceButton.Image = ((System.Drawing.Image)(resources.GetObject("insertEndPointSilenceButton.Image")));
            this.insertEndPointSilenceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.insertEndPointSilenceButton.Name = "insertEndPointSilenceButton";
            this.insertEndPointSilenceButton.Size = new System.Drawing.Size(133, 22);
            this.insertEndPointSilenceButton.Text = "Insert end point silence";
            this.insertEndPointSilenceButton.Click += new System.EventHandler(this.insertEndPointSilenceButton_Click);
            // 
            // endPointSilenceDurationLabel
            // 
            this.endPointSilenceDurationLabel.Enabled = false;
            this.endPointSilenceDurationLabel.Name = "endPointSilenceDurationLabel";
            this.endPointSilenceDurationLabel.Size = new System.Drawing.Size(148, 22);
            this.endPointSilenceDurationLabel.Text = "End point silence duration:";
            // 
            // endPointSilenceDurationTextBox
            // 
            this.endPointSilenceDurationTextBox.Enabled = false;
            this.endPointSilenceDurationTextBox.Name = "endPointSilenceDurationTextBox";
            this.endPointSilenceDurationTextBox.Size = new System.Drawing.Size(50, 25);
            this.endPointSilenceDurationTextBox.Text = "0.05";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(3, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1188, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // optimizerTabPage
            // 
            this.optimizerTabPage.Controls.Add(this.optimizerTabControl);
            this.optimizerTabPage.Location = new System.Drawing.Point(4, 22);
            this.optimizerTabPage.Name = "optimizerTabPage";
            this.optimizerTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.optimizerTabPage.Size = new System.Drawing.Size(1194, 565);
            this.optimizerTabPage.TabIndex = 4;
            this.optimizerTabPage.Text = "Optimizer";
            this.optimizerTabPage.UseVisualStyleBackColor = true;
            // 
            // optimizerTabControl
            // 
            this.optimizerTabControl.Controls.Add(this.allSoundsOptimizationTabPage);
            this.optimizerTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optimizerTabControl.Location = new System.Drawing.Point(3, 3);
            this.optimizerTabControl.Name = "optimizerTabControl";
            this.optimizerTabControl.SelectedIndex = 0;
            this.optimizerTabControl.Size = new System.Drawing.Size(1188, 559);
            this.optimizerTabControl.TabIndex = 0;
            // 
            // allSoundsOptimizationTabPage
            // 
            this.allSoundsOptimizationTabPage.Controls.Add(this.splitContainer6);
            this.allSoundsOptimizationTabPage.Controls.Add(this.toolStrip3);
            this.allSoundsOptimizationTabPage.Location = new System.Drawing.Point(4, 22);
            this.allSoundsOptimizationTabPage.Name = "allSoundsOptimizationTabPage";
            this.allSoundsOptimizationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.allSoundsOptimizationTabPage.Size = new System.Drawing.Size(1180, 533);
            this.allSoundsOptimizationTabPage.TabIndex = 4;
            this.allSoundsOptimizationTabPage.Text = "All sounds";
            this.allSoundsOptimizationTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(3, 28);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.splitContainer7);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.soundVisualizer3x3);
            this.splitContainer6.Size = new System.Drawing.Size(1174, 502);
            this.splitContainer6.SplitterDistance = 357;
            this.splitContainer6.TabIndex = 2;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.BackColor = System.Drawing.Color.LightSlateGray;
            this.splitContainer7.Panel1.Controls.Add(this.ieaFormantSettingsEditor);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.ieaFormantSettingsListListBox);
            this.splitContainer7.Size = new System.Drawing.Size(357, 502);
            this.splitContainer7.SplitterDistance = 199;
            this.splitContainer7.TabIndex = 0;
            // 
            // ieaFormantSettingsEditor
            // 
            this.ieaFormantSettingsEditor.BackgroundColor = System.Drawing.Color.DimGray;
            this.ieaFormantSettingsEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ieaFormantSettingsEditor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ieaFormantSettingsEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ieaFormantSettingsEditor.Location = new System.Drawing.Point(0, 0);
            this.ieaFormantSettingsEditor.Name = "ieaFormantSettingsEditor";
            this.ieaFormantSettingsEditor.Size = new System.Drawing.Size(199, 502);
            this.ieaFormantSettingsEditor.TabIndex = 0;
            // 
            // ieaFormantSettingsListListBox
            // 
            this.ieaFormantSettingsListListBox.BackColor = System.Drawing.Color.DimGray;
            this.ieaFormantSettingsListListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ieaFormantSettingsListListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ieaFormantSettingsListListBox.ForeColor = System.Drawing.Color.White;
            this.ieaFormantSettingsListListBox.FormattingEnabled = true;
            this.ieaFormantSettingsListListBox.IntegralHeight = false;
            this.ieaFormantSettingsListListBox.Location = new System.Drawing.Point(0, 0);
            this.ieaFormantSettingsListListBox.Name = "ieaFormantSettingsListListBox";
            this.ieaFormantSettingsListListBox.Size = new System.Drawing.Size(154, 502);
            this.ieaFormantSettingsListListBox.TabIndex = 1;
            this.ieaFormantSettingsListListBox.SelectedIndexChanged += new System.EventHandler(this.ieaFormantSettingsListListBox_SelectedIndexChanged);
            // 
            // soundVisualizer3x3
            // 
            this.soundVisualizer3x3.BackColor = System.Drawing.Color.DimGray;
            this.soundVisualizer3x3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soundVisualizer3x3.Location = new System.Drawing.Point(0, 0);
            this.soundVisualizer3x3.Name = "soundVisualizer3x3";
            this.soundVisualizer3x3.Size = new System.Drawing.Size(813, 502);
            this.soundVisualizer3x3.TabIndex = 0;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startButton,
            this.assignCurrentSoundButton,
            this.toolStripSeparator3,
            this.toolStripLabel3,
            this.relativeModificationRangeTextBox,
            this.modificationScopeDropDownButton,
            this.modificationSettingsDropDownButton});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1174, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
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
            // assignCurrentSoundButton
            // 
            this.assignCurrentSoundButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.assignCurrentSoundButton.Enabled = false;
            this.assignCurrentSoundButton.Image = ((System.Drawing.Image)(resources.GetObject("assignCurrentSoundButton.Image")));
            this.assignCurrentSoundButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.assignCurrentSoundButton.Name = "assignCurrentSoundButton";
            this.assignCurrentSoundButton.Size = new System.Drawing.Size(171, 22);
            this.assignCurrentSoundButton.Text = "Assign current sound to editor";
            this.assignCurrentSoundButton.Click += new System.EventHandler(this.assignCurrentSoundButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(111, 22);
            this.toolStripLabel3.Text = "Modification range:";
            // 
            // relativeModificationRangeTextBox
            // 
            this.relativeModificationRangeTextBox.Name = "relativeModificationRangeTextBox";
            this.relativeModificationRangeTextBox.Size = new System.Drawing.Size(50, 25);
            this.relativeModificationRangeTextBox.Text = "0.10";
            // 
            // modificationScopeDropDownButton
            // 
            this.modificationScopeDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.modificationScopeDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("modificationScopeDropDownButton.Image")));
            this.modificationScopeDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modificationScopeDropDownButton.Name = "modificationScopeDropDownButton";
            this.modificationScopeDropDownButton.Size = new System.Drawing.Size(122, 22);
            this.modificationScopeDropDownButton.Text = "Modification scope";
            // 
            // modificationSettingsDropDownButton
            // 
            this.modificationSettingsDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.modificationSettingsDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifySinusoidsToolStripMenuItem,
            this.modifyVoicedFractionToolStripMenuItem,
            this.modifyDurationToolStripMenuItem,
            this.modifyTransitionStartToolStripMenuItem,
            this.modifyAmplitudeVariationToolStripMenuItem,
            this.modifyPitchVariationToolStripMenuItem});
            this.modificationSettingsDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("modificationSettingsDropDownButton.Image")));
            this.modificationSettingsDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modificationSettingsDropDownButton.Name = "modificationSettingsDropDownButton";
            this.modificationSettingsDropDownButton.Size = new System.Drawing.Size(132, 22);
            this.modificationSettingsDropDownButton.Text = "Modification settings";
            // 
            // modifySinusoidsToolStripMenuItem
            // 
            this.modifySinusoidsToolStripMenuItem.Checked = true;
            this.modifySinusoidsToolStripMenuItem.CheckOnClick = true;
            this.modifySinusoidsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.modifySinusoidsToolStripMenuItem.Name = "modifySinusoidsToolStripMenuItem";
            this.modifySinusoidsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.modifySinusoidsToolStripMenuItem.Text = "Modify sinusoids";
            // 
            // modifyVoicedFractionToolStripMenuItem
            // 
            this.modifyVoicedFractionToolStripMenuItem.Checked = true;
            this.modifyVoicedFractionToolStripMenuItem.CheckOnClick = true;
            this.modifyVoicedFractionToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.modifyVoicedFractionToolStripMenuItem.Name = "modifyVoicedFractionToolStripMenuItem";
            this.modifyVoicedFractionToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.modifyVoicedFractionToolStripMenuItem.Text = "Modify voiced fraction";
            // 
            // modifyDurationToolStripMenuItem
            // 
            this.modifyDurationToolStripMenuItem.Checked = true;
            this.modifyDurationToolStripMenuItem.CheckOnClick = true;
            this.modifyDurationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.modifyDurationToolStripMenuItem.Name = "modifyDurationToolStripMenuItem";
            this.modifyDurationToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.modifyDurationToolStripMenuItem.Text = "Modify duration";
            // 
            // modifyTransitionStartToolStripMenuItem
            // 
            this.modifyTransitionStartToolStripMenuItem.Checked = true;
            this.modifyTransitionStartToolStripMenuItem.CheckOnClick = true;
            this.modifyTransitionStartToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.modifyTransitionStartToolStripMenuItem.Name = "modifyTransitionStartToolStripMenuItem";
            this.modifyTransitionStartToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.modifyTransitionStartToolStripMenuItem.Text = "Modify transition start";
            // 
            // modifyAmplitudeVariationToolStripMenuItem
            // 
            this.modifyAmplitudeVariationToolStripMenuItem.Checked = true;
            this.modifyAmplitudeVariationToolStripMenuItem.CheckOnClick = true;
            this.modifyAmplitudeVariationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.modifyAmplitudeVariationToolStripMenuItem.Name = "modifyAmplitudeVariationToolStripMenuItem";
            this.modifyAmplitudeVariationToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.modifyAmplitudeVariationToolStripMenuItem.Text = "Modify amplitude variation";
            // 
            // modifyPitchVariationToolStripMenuItem
            // 
            this.modifyPitchVariationToolStripMenuItem.Checked = true;
            this.modifyPitchVariationToolStripMenuItem.CheckOnClick = true;
            this.modifyPitchVariationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.modifyPitchVariationToolStripMenuItem.Name = "modifyPitchVariationToolStripMenuItem";
            this.modifyPitchVariationToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.modifyPitchVariationToolStripMenuItem.Text = "Modify pitch variation";
            // 
            // synthesizerTabPage
            // 
            this.synthesizerTabPage.Controls.Add(this.splitContainer4);
            this.synthesizerTabPage.Controls.Add(this.toolStrip5);
            this.synthesizerTabPage.Location = new System.Drawing.Point(4, 22);
            this.synthesizerTabPage.Name = "synthesizerTabPage";
            this.synthesizerTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.synthesizerTabPage.Size = new System.Drawing.Size(1194, 565);
            this.synthesizerTabPage.TabIndex = 2;
            this.synthesizerTabPage.Text = "Synthesizer";
            this.synthesizerTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 28);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer8);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.sentenceSpeechVisualizer);
            this.splitContainer4.Size = new System.Drawing.Size(1188, 534);
            this.splitContainer4.SplitterDistance = 335;
            this.splitContainer4.TabIndex = 1;
            // 
            // splitContainer8
            // 
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            this.splitContainer8.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.synthesizerSpecificationListBox);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.wordToSoundMappingEditor);
            this.splitContainer8.Size = new System.Drawing.Size(335, 534);
            this.splitContainer8.SplitterDistance = 249;
            this.splitContainer8.TabIndex = 0;
            // 
            // synthesizerSpecificationListBox
            // 
            this.synthesizerSpecificationListBox.BackColor = System.Drawing.Color.DimGray;
            this.synthesizerSpecificationListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.synthesizerSpecificationListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.synthesizerSpecificationListBox.ForeColor = System.Drawing.Color.White;
            this.synthesizerSpecificationListBox.FormattingEnabled = true;
            this.synthesizerSpecificationListBox.IntegralHeight = false;
            this.synthesizerSpecificationListBox.Location = new System.Drawing.Point(0, 0);
            this.synthesizerSpecificationListBox.Name = "synthesizerSpecificationListBox";
            this.synthesizerSpecificationListBox.Size = new System.Drawing.Size(335, 249);
            this.synthesizerSpecificationListBox.TabIndex = 0;
            this.synthesizerSpecificationListBox.SelectedIndexChanged += new System.EventHandler(this.synthesizerSpecificationListBox_SelectedIndexChanged);
            this.synthesizerSpecificationListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.synthesizerSpecificationListBox_MouseDoubleClick);
            this.synthesizerSpecificationListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.synthesizerSpecificationListBox_MouseDown);
            // 
            // wordToSoundMappingEditor
            // 
            this.wordToSoundMappingEditor.BackgroundColor = System.Drawing.Color.DimGray;
            this.wordToSoundMappingEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.wordToSoundMappingEditor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wordToSoundMappingEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wordToSoundMappingEditor.Location = new System.Drawing.Point(0, 0);
            this.wordToSoundMappingEditor.Name = "wordToSoundMappingEditor";
            this.wordToSoundMappingEditor.Size = new System.Drawing.Size(335, 281);
            this.wordToSoundMappingEditor.TabIndex = 1;
            this.wordToSoundMappingEditor.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.wordToSoundMappingEditor_CellDoubleClick);
            // 
            // sentenceSpeechVisualizer
            // 
            this.sentenceSpeechVisualizer.BackColor = System.Drawing.Color.Black;
            this.sentenceSpeechVisualizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sentenceSpeechVisualizer.Location = new System.Drawing.Point(0, 0);
            this.sentenceSpeechVisualizer.MarkerList = null;
            this.sentenceSpeechVisualizer.Name = "sentenceSpeechVisualizer";
            this.sentenceSpeechVisualizer.PitchPanelVisible = true;
            this.sentenceSpeechVisualizer.Size = new System.Drawing.Size(849, 534);
            this.sentenceSpeechVisualizer.TabIndex = 0;
            // 
            // toolStrip5
            // 
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel9,
            this.soundNameTextBox,
            this.addToSynthesizerButton,
            this.toolStripSeparator9,
            this.volumeLabel,
            this.volumeTextBox,
            this.toolStripSeparator6,
            this.sentenceToSpeakTextBox,
            this.speakSentenceButton});
            this.toolStrip5.Location = new System.Drawing.Point(3, 3);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(1188, 25);
            this.toolStrip5.TabIndex = 0;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // toolStripLabel9
            // 
            this.toolStripLabel9.Name = "toolStripLabel9";
            this.toolStripLabel9.Size = new System.Drawing.Size(77, 22);
            this.toolStripLabel9.Text = "Sound name:";
            // 
            // soundNameTextBox
            // 
            this.soundNameTextBox.Name = "soundNameTextBox";
            this.soundNameTextBox.Size = new System.Drawing.Size(50, 25);
            // 
            // addToSynthesizerButton
            // 
            this.addToSynthesizerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addToSynthesizerButton.Image = ((System.Drawing.Image)(resources.GetObject("addToSynthesizerButton.Image")));
            this.addToSynthesizerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addToSynthesizerButton.Name = "addToSynthesizerButton";
            this.addToSynthesizerButton.Size = new System.Drawing.Size(108, 22);
            this.addToSynthesizerButton.Text = "Add to synthesizer";
            this.addToSynthesizerButton.Click += new System.EventHandler(this.addToSynthesizerButton_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // volumeLabel
            // 
            this.volumeLabel.Name = "volumeLabel";
            this.volumeLabel.Size = new System.Drawing.Size(51, 22);
            this.volumeLabel.Text = "Volume:";
            // 
            // volumeTextBox
            // 
            this.volumeTextBox.Name = "volumeTextBox";
            this.volumeTextBox.Size = new System.Drawing.Size(60, 25);
            this.volumeTextBox.Text = "1.00";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // sentenceToSpeakTextBox
            // 
            this.sentenceToSpeakTextBox.Name = "sentenceToSpeakTextBox";
            this.sentenceToSpeakTextBox.Size = new System.Drawing.Size(500, 25);
            // 
            // speakSentenceButton
            // 
            this.speakSentenceButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.speakSentenceButton.Enabled = false;
            this.speakSentenceButton.Image = ((System.Drawing.Image)(resources.GetObject("speakSentenceButton.Image")));
            this.speakSentenceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.speakSentenceButton.Name = "speakSentenceButton";
            this.speakSentenceButton.Size = new System.Drawing.Size(42, 22);
            this.speakSentenceButton.Text = "Speak";
            this.speakSentenceButton.Click += new System.EventHandler(this.speakSentenceButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 615);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Formant speech synthesis application (c) Mattias Wahde, 2018";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.editorTabPage.ResumeLayout(false);
            this.editorTabPage.PerformLayout();
            this.editorMainSplitContainer.Panel1.ResumeLayout(false);
            this.editorMainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editorMainSplitContainer)).EndInit();
            this.editorMainSplitContainer.ResumeLayout(false);
            this.editorSecondarySplitContainer.Panel1.ResumeLayout(false);
            this.editorSecondarySplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editorSecondarySplitContainer)).EndInit();
            this.editorSecondarySplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.formantSettingsEditor)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.optimizerTabPage.ResumeLayout(false);
            this.optimizerTabControl.ResumeLayout(false);
            this.allSoundsOptimizationTabPage.ResumeLayout(false);
            this.allSoundsOptimizationTabPage.PerformLayout();
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ieaFormantSettingsEditor)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.synthesizerTabPage.ResumeLayout(false);
            this.synthesizerTabPage.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
            this.splitContainer8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wordToSoundMappingEditor)).EndInit();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage editorTabPage;
        private System.Windows.Forms.TabPage synthesizerTabPage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer editorMainSplitContainer;
        private System.Windows.Forms.SplitContainer editorSecondarySplitContainer;
        private System.Windows.Forms.ListBox formantSpecificationListBox;
        private SpeechSynthesisLibrary.FormantSynthesis.FormantSettingsEditor formantSettingsEditor;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton editorPlayButton;
        private System.Windows.Forms.ToolStripButton editorRandomizeButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox fundamentalFrequencyTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox samplingFrequencyTextBox;
        private SpeechSynthesisLibrary.Visualization.SpeechVisualizer editorSpeechVisualizer;
        private System.Windows.Forms.ToolStripButton insertEndPointSilenceButton;
        private System.Windows.Forms.ToolStripLabel endPointSilenceDurationLabel;
        private System.Windows.Forms.ToolStripTextBox endPointSilenceDurationTextBox;
        private System.Windows.Forms.ToolStripButton clearButton;
        private System.Windows.Forms.TabPage optimizerTabPage;
        private System.Windows.Forms.TabControl optimizerTabControl;
        private System.Windows.Forms.TabPage allSoundsOptimizationTabPage;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private SpeechSynthesisLibrary.FormantSynthesis.FormantSettingsEditor ieaFormantSettingsEditor;
        private System.Windows.Forms.ListBox ieaFormantSettingsListListBox;
        private AudioLibrary.Visualization.SoundVisualizer3x3 soundVisualizer3x3;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripButton assignCurrentSoundButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox relativeModificationRangeTextBox;
        private System.Windows.Forms.ToolStripDropDownButton modificationScopeDropDownButton;
        private System.Windows.Forms.ToolStripDropDownButton modificationSettingsDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem modifyAmplitudeVariationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifySinusoidsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyVoicedFractionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyDurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyTransitionStartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyPitchVariationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton setUnvoicedButton;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.SplitContainer splitContainer8;
        private System.Windows.Forms.ListBox synthesizerSpecificationListBox;
        private SpeechSynthesisLibrary.FormantSynthesis.WordToSoundMappingEditor wordToSoundMappingEditor;
        private SpeechSynthesisLibrary.Visualization.SpeechVisualizer sentenceSpeechVisualizer;
        private System.Windows.Forms.ToolStripLabel toolStripLabel9;
        private System.Windows.Forms.ToolStripTextBox soundNameTextBox;
        private System.Windows.Forms.ToolStripButton addToSynthesizerButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripTextBox sentenceToSpeakTextBox;
        private System.Windows.Forms.ToolStripButton speakSentenceButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem newSpeechSynthesizerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSpeechSynthesizerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSpeechSynthesizerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripLabel volumeLabel;
        private System.Windows.Forms.ToolStripTextBox volumeTextBox;
    }
}

