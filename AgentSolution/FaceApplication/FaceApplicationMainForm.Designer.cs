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
            this.faceAndMovieContainer = new System.Windows.Forms.SplitContainer();
            this.viewer3D = new ThreeDimensionalVisualizationLibrary.Viewer3D();
            this.recommendationGrpBox = new System.Windows.Forms.GroupBox();
            this.movieTxt = new System.Windows.Forms.TextBox();
            this.mostSimUserTxt = new System.Windows.Forms.TextBox();
            this.mostSimUserLabel = new System.Windows.Forms.Label();
            this.movieLabel = new System.Windows.Forms.Label();
            this.movieInfoGrpBox = new System.Windows.Forms.GroupBox();
            this.imdbRatingTxt = new System.Windows.Forms.TextBox();
            this.genreTxt = new System.Windows.Forms.TextBox();
            this.imdbRatingLabel = new System.Windows.Forms.Label();
            this.yearTxt = new System.Windows.Forms.TextBox();
            this.titleTxt = new System.Windows.Forms.TextBox();
            this.genreLabel = new System.Windows.Forms.Label();
            this.yearLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.currentUserTxt = new System.Windows.Forms.TextBox();
            this.labelCurrentUser = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.communicationLogTabPage = new System.Windows.Forms.TabPage();
            this.communicationLogListBox = new CustomUserControlsLibrary.ColorListBox();
            this.menuStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.faceTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.faceAndMovieContainer)).BeginInit();
            this.faceAndMovieContainer.Panel1.SuspendLayout();
            this.faceAndMovieContainer.Panel2.SuspendLayout();
            this.faceAndMovieContainer.SuspendLayout();
            this.recommendationGrpBox.SuspendLayout();
            this.movieInfoGrpBox.SuspendLayout();
            this.communicationLogTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(384, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
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
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 19);
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
            this.mainTabControl.Location = new System.Drawing.Point(0, 25);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(384, 836);
            this.mainTabControl.TabIndex = 2;
            // 
            // faceTabPage
            // 
            this.faceTabPage.Controls.Add(this.faceAndMovieContainer);
            this.faceTabPage.Controls.Add(this.splitter1);
            this.faceTabPage.Location = new System.Drawing.Point(4, 22);
            this.faceTabPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.faceTabPage.Name = "faceTabPage";
            this.faceTabPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.faceTabPage.Size = new System.Drawing.Size(376, 810);
            this.faceTabPage.TabIndex = 1;
            this.faceTabPage.Text = "Face";
            this.faceTabPage.UseVisualStyleBackColor = true;
            // 
            // faceAndMovieContainer
            // 
            this.faceAndMovieContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.faceAndMovieContainer.Location = new System.Drawing.Point(5, 5);
            this.faceAndMovieContainer.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.faceAndMovieContainer.Name = "faceAndMovieContainer";
            this.faceAndMovieContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // faceAndMovieContainer.Panel1
            // 
            this.faceAndMovieContainer.Panel1.Controls.Add(this.viewer3D);
            // 
            // faceAndMovieContainer.Panel2
            // 
            this.faceAndMovieContainer.Panel2.Controls.Add(this.recommendationGrpBox);
            this.faceAndMovieContainer.Panel2.Controls.Add(this.movieInfoGrpBox);
            this.faceAndMovieContainer.Panel2.Controls.Add(this.currentUserTxt);
            this.faceAndMovieContainer.Panel2.Controls.Add(this.labelCurrentUser);
            this.faceAndMovieContainer.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.faceMovieSplitContainer);
            this.faceAndMovieContainer.Size = new System.Drawing.Size(368, 800);
            this.faceAndMovieContainer.SplitterDistance = 368;
            this.faceAndMovieContainer.SplitterWidth = 5;
            this.faceAndMovieContainer.TabIndex = 3;
            // 
            // viewer3D
            // 
            this.viewer3D.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewer3D.BackColor = System.Drawing.Color.Black;
            this.viewer3D.CameraDistance = 4D;
            this.viewer3D.CameraLatitude = 0.39269908169872414D;
            this.viewer3D.CameraLongitude = 0.78539816339744828D;
            this.viewer3D.CameraTarget = ((OpenTK.Vector3)(resources.GetObject("viewer3D.CameraTarget")));
            this.viewer3D.Location = new System.Drawing.Point(0, 0);
            this.viewer3D.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.viewer3D.Name = "viewer3D";
            this.viewer3D.Scene = null;
            this.viewer3D.ShowSurfaces = true;
            this.viewer3D.ShowVertices = false;
            this.viewer3D.ShowWireframe = false;
            this.viewer3D.ShowWorldAxes = false;
            this.viewer3D.Size = new System.Drawing.Size(371, 373);
            this.viewer3D.TabIndex = 0;
            this.viewer3D.UseSmoothShading = false;
            this.viewer3D.VSync = false;
            // 
            // recommendationGrpBox
            // 
            this.recommendationGrpBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recommendationGrpBox.Controls.Add(this.movieTxt);
            this.recommendationGrpBox.Controls.Add(this.mostSimUserTxt);
            this.recommendationGrpBox.Controls.Add(this.mostSimUserLabel);
            this.recommendationGrpBox.Controls.Add(this.movieLabel);
            this.recommendationGrpBox.Location = new System.Drawing.Point(79, 288);
            this.recommendationGrpBox.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.recommendationGrpBox.Name = "recommendationGrpBox";
            this.recommendationGrpBox.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.recommendationGrpBox.Size = new System.Drawing.Size(210, 116);
            this.recommendationGrpBox.TabIndex = 3;
            this.recommendationGrpBox.TabStop = false;
            this.recommendationGrpBox.Text = "Recommendation";
            // 
            // movieTxt
            // 
            this.movieTxt.Location = new System.Drawing.Point(101, 29);
            this.movieTxt.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.movieTxt.Name = "movieTxt";
            this.movieTxt.Size = new System.Drawing.Size(105, 20);
            this.movieTxt.TabIndex = 1;
            this.movieTxt.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // mostSimUserTxt
            // 
            this.mostSimUserTxt.Location = new System.Drawing.Point(101, 64);
            this.mostSimUserTxt.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.mostSimUserTxt.Name = "mostSimUserTxt";
            this.mostSimUserTxt.Size = new System.Drawing.Size(105, 20);
            this.mostSimUserTxt.TabIndex = 1;
            this.mostSimUserTxt.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // mostSimUserLabel
            // 
            this.mostSimUserLabel.AutoSize = true;
            this.mostSimUserLabel.Location = new System.Drawing.Point(4, 68);
            this.mostSimUserLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mostSimUserLabel.Name = "mostSimUserLabel";
            this.mostSimUserLabel.Size = new System.Drawing.Size(84, 13);
            this.mostSimUserLabel.TabIndex = 0;
            this.mostSimUserLabel.Text = "Most similar user";
            // 
            // movieLabel
            // 
            this.movieLabel.AutoSize = true;
            this.movieLabel.Location = new System.Drawing.Point(4, 32);
            this.movieLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.movieLabel.Name = "movieLabel";
            this.movieLabel.Size = new System.Drawing.Size(36, 13);
            this.movieLabel.TabIndex = 0;
            this.movieLabel.Text = "Movie";
            // 
            // movieInfoGrpBox
            // 
            this.movieInfoGrpBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.movieInfoGrpBox.Controls.Add(this.imdbRatingTxt);
            this.movieInfoGrpBox.Controls.Add(this.genreTxt);
            this.movieInfoGrpBox.Controls.Add(this.imdbRatingLabel);
            this.movieInfoGrpBox.Controls.Add(this.yearTxt);
            this.movieInfoGrpBox.Controls.Add(this.titleTxt);
            this.movieInfoGrpBox.Controls.Add(this.genreLabel);
            this.movieInfoGrpBox.Controls.Add(this.yearLabel);
            this.movieInfoGrpBox.Controls.Add(this.titleLabel);
            this.movieInfoGrpBox.Location = new System.Drawing.Point(79, 82);
            this.movieInfoGrpBox.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.movieInfoGrpBox.Name = "movieInfoGrpBox";
            this.movieInfoGrpBox.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.movieInfoGrpBox.Size = new System.Drawing.Size(210, 199);
            this.movieInfoGrpBox.TabIndex = 2;
            this.movieInfoGrpBox.TabStop = false;
            this.movieInfoGrpBox.Text = "Movie information";
            // 
            // imdbRatingTxt
            // 
            this.imdbRatingTxt.Location = new System.Drawing.Point(101, 107);
            this.imdbRatingTxt.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.imdbRatingTxt.Name = "imdbRatingTxt";
            this.imdbRatingTxt.Size = new System.Drawing.Size(105, 20);
            this.imdbRatingTxt.TabIndex = 1;
            this.imdbRatingTxt.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // genreTxt
            // 
            this.genreTxt.Location = new System.Drawing.Point(101, 143);
            this.genreTxt.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.genreTxt.Name = "genreTxt";
            this.genreTxt.Size = new System.Drawing.Size(105, 20);
            this.genreTxt.TabIndex = 1;
            // 
            // imdbRatingLabel
            // 
            this.imdbRatingLabel.AutoSize = true;
            this.imdbRatingLabel.Location = new System.Drawing.Point(4, 110);
            this.imdbRatingLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.imdbRatingLabel.Name = "imdbRatingLabel";
            this.imdbRatingLabel.Size = new System.Drawing.Size(62, 13);
            this.imdbRatingLabel.TabIndex = 0;
            this.imdbRatingLabel.Text = "IMDb rating";
            // 
            // yearTxt
            // 
            this.yearTxt.Location = new System.Drawing.Point(101, 70);
            this.yearTxt.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.yearTxt.Name = "yearTxt";
            this.yearTxt.Size = new System.Drawing.Size(105, 20);
            this.yearTxt.TabIndex = 1;
            // 
            // titleTxt
            // 
            this.titleTxt.Location = new System.Drawing.Point(101, 34);
            this.titleTxt.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.titleTxt.Name = "titleTxt";
            this.titleTxt.Size = new System.Drawing.Size(105, 20);
            this.titleTxt.TabIndex = 1;
            // 
            // genreLabel
            // 
            this.genreLabel.AutoSize = true;
            this.genreLabel.Location = new System.Drawing.Point(4, 147);
            this.genreLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.genreLabel.Name = "genreLabel";
            this.genreLabel.Size = new System.Drawing.Size(36, 13);
            this.genreLabel.TabIndex = 0;
            this.genreLabel.Text = "Genre";
            // 
            // yearLabel
            // 
            this.yearLabel.AutoSize = true;
            this.yearLabel.Location = new System.Drawing.Point(4, 74);
            this.yearLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(29, 13);
            this.yearLabel.TabIndex = 0;
            this.yearLabel.Text = "Year";
            this.yearLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(4, 38);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(27, 13);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Title";
            // 
            // currentUserTxt
            // 
            this.currentUserTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.currentUserTxt.Location = new System.Drawing.Point(180, 28);
            this.currentUserTxt.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.currentUserTxt.Name = "currentUserTxt";
            this.currentUserTxt.Size = new System.Drawing.Size(105, 20);
            this.currentUserTxt.TabIndex = 1;
            this.currentUserTxt.TextChanged += new System.EventHandler(this.currentUserTxt_TextChanged);
            // 
            // labelCurrentUser
            // 
            this.labelCurrentUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCurrentUser.AutoSize = true;
            this.labelCurrentUser.Location = new System.Drawing.Point(84, 28);
            this.labelCurrentUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCurrentUser.Name = "labelCurrentUser";
            this.labelCurrentUser.Size = new System.Drawing.Size(64, 13);
            this.labelCurrentUser.TabIndex = 0;
            this.labelCurrentUser.Text = "Current user";
            this.labelCurrentUser.Click += new System.EventHandler(this.label1_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(3, 5);
            this.splitter1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 800);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // communicationLogTabPage
            // 
            this.communicationLogTabPage.Controls.Add(this.communicationLogListBox);
            this.communicationLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.communicationLogTabPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.communicationLogTabPage.Name = "communicationLogTabPage";
            this.communicationLogTabPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.communicationLogTabPage.Size = new System.Drawing.Size(376, 810);
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
            this.communicationLogListBox.Location = new System.Drawing.Point(3, 5);
            this.communicationLogListBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.communicationLogListBox.Name = "communicationLogListBox";
            this.communicationLogListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.communicationLogListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.communicationLogListBox.Size = new System.Drawing.Size(370, 800);
            this.communicationLogListBox.TabIndex = 0;
            // 
            // FaceApplicationMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 861);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimumSize = new System.Drawing.Size(400, 900);
            this.Name = "FaceApplicationMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Face application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaceApplicationMainForm_FormClosing);
            this.Load += new System.EventHandler(this.FaceApplicationMainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.faceTabPage.ResumeLayout(false);
            this.faceAndMovieContainer.Panel1.ResumeLayout(false);
            this.faceAndMovieContainer.Panel2.ResumeLayout(false);
            this.faceAndMovieContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.faceAndMovieContainer)).EndInit();
            this.faceAndMovieContainer.ResumeLayout(false);
            this.recommendationGrpBox.ResumeLayout(false);
            this.recommendationGrpBox.PerformLayout();
            this.movieInfoGrpBox.ResumeLayout(false);
            this.movieInfoGrpBox.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem blinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shakeHeadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer faceAndMovieContainer;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label labelCurrentUser;
        private System.Windows.Forms.GroupBox recommendationGrpBox;
        private System.Windows.Forms.Label mostSimUserLabel;
        private System.Windows.Forms.Label movieLabel;
        private System.Windows.Forms.GroupBox movieInfoGrpBox;
        private System.Windows.Forms.Label imdbRatingLabel;
        private System.Windows.Forms.Label genreLabel;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox currentUserTxt;
        private System.Windows.Forms.TextBox imdbRatingTxt;
        private System.Windows.Forms.TextBox genreTxt;
        private System.Windows.Forms.TextBox yearTxt;
        private System.Windows.Forms.TextBox titleTxt;
        private System.Windows.Forms.TextBox movieTxt;
        private System.Windows.Forms.TextBox mostSimUserTxt;
        private ThreeDimensionalVisualizationLibrary.Viewer3D viewer3D;
    }
}

