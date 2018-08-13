namespace IsolatedWordRecognitionApplication
{
    partial class AddWordDialog
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
            this.browseButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.soundNameTextBox = new System.Windows.Forms.TextBox();
            this.soundListListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(12, 10);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(263, 23);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Browse ...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // addButton
            // 
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(15, 77);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(260, 23);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sound name:";
            // 
            // soundNameTextBox
            // 
            this.soundNameTextBox.Location = new System.Drawing.Point(88, 42);
            this.soundNameTextBox.Name = "soundNameTextBox";
            this.soundNameTextBox.Size = new System.Drawing.Size(187, 20);
            this.soundNameTextBox.TabIndex = 5;
            this.soundNameTextBox.TextChanged += new System.EventHandler(this.soundNameTextBox_TextChanged);
            // 
            // soundListListView
            // 
            this.soundListListView.CheckBoxes = true;
            this.soundListListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.soundListListView.Location = new System.Drawing.Point(15, 115);
            this.soundListListView.Name = "soundListListView";
            this.soundListListView.Size = new System.Drawing.Size(260, 248);
            this.soundListListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.soundListListView.TabIndex = 6;
            this.soundListListView.UseCompatibleStateImageBehavior = false;
            this.soundListListView.View = System.Windows.Forms.View.List;
            this.soundListListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.soundListListView_ItemChecked);
            // 
            // AddWordDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 377);
            this.Controls.Add(this.soundListListView);
            this.Controls.Add(this.soundNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.browseButton);
            this.Name = "AddWordDialog";
            this.Text = "AddWordDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox soundNameTextBox;
        private System.Windows.Forms.ListView soundListListView;
    }
}