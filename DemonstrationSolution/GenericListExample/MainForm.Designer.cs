namespace GenericListExample
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
            this.runExample1Button = new System.Windows.Forms.ToolStripButton();
            this.displayTextBox = new System.Windows.Forms.TextBox();
            this.runExample2Button = new System.Windows.Forms.ToolStripButton();
            this.runExample3Button = new System.Windows.Forms.ToolStripButton();
            this.runExample4Button = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(406, 24);
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runExample1Button,
            this.runExample2Button,
            this.runExample3Button,
            this.runExample4Button});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(406, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // runExample1Button
            // 
            this.runExample1Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runExample1Button.Image = ((System.Drawing.Image)(resources.GetObject("runExample1Button.Image")));
            this.runExample1Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runExample1Button.Name = "runExample1Button";
            this.runExample1Button.Size = new System.Drawing.Size(88, 22);
            this.runExample1Button.Text = "Run example 1";
            this.runExample1Button.Click += new System.EventHandler(this.runExample1Button_Click);
            // 
            // displayTextBox
            // 
            this.displayTextBox.BackColor = System.Drawing.Color.Black;
            this.displayTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayTextBox.ForeColor = System.Drawing.Color.Lime;
            this.displayTextBox.Location = new System.Drawing.Point(0, 49);
            this.displayTextBox.Multiline = true;
            this.displayTextBox.Name = "displayTextBox";
            this.displayTextBox.ReadOnly = true;
            this.displayTextBox.Size = new System.Drawing.Size(406, 313);
            this.displayTextBox.TabIndex = 2;
            // 
            // runExample2Button
            // 
            this.runExample2Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runExample2Button.Image = ((System.Drawing.Image)(resources.GetObject("runExample2Button.Image")));
            this.runExample2Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runExample2Button.Name = "runExample2Button";
            this.runExample2Button.Size = new System.Drawing.Size(88, 22);
            this.runExample2Button.Text = "Run example 2";
            this.runExample2Button.Click += new System.EventHandler(this.runExample2Button_Click);
            // 
            // runExample3Button
            // 
            this.runExample3Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runExample3Button.Image = ((System.Drawing.Image)(resources.GetObject("runExample3Button.Image")));
            this.runExample3Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runExample3Button.Name = "runExample3Button";
            this.runExample3Button.Size = new System.Drawing.Size(88, 22);
            this.runExample3Button.Text = "Run example 3";
            this.runExample3Button.Click += new System.EventHandler(this.runExample3Button_Click);
            // 
            // runExample4Button
            // 
            this.runExample4Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runExample4Button.Image = ((System.Drawing.Image)(resources.GetObject("runExample4Button.Image")));
            this.runExample4Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runExample4Button.Name = "runExample4Button";
            this.runExample4Button.Size = new System.Drawing.Size(88, 22);
            this.runExample4Button.Text = "Run example 4";
            this.runExample4Button.Click += new System.EventHandler(this.runExample4Button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 362);
            this.Controls.Add(this.displayTextBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Generic list example";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton runExample1Button;
        private System.Windows.Forms.TextBox displayTextBox;
        private System.Windows.Forms.ToolStripButton runExample2Button;
        private System.Windows.Forms.ToolStripButton runExample3Button;
        private System.Windows.Forms.ToolStripButton runExample4Button;
    }
}

