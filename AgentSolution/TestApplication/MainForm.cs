using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgentLibrary;

namespace TestApplication
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void checkMatchButton_Click(object sender, EventArgs e)
        {
            Pattern pattern = new Pattern();
            pattern.Definition = patternDefinitionTextBox.Text;
            string inputString = inputStringTextBox.Text;           
            Boolean isMatching = pattern.IsMatching(inputString);
            if (isMatching) { this.BackColor = Color.Lime; }
            else { this.BackColor = Color.Red; }
        }

        private void inputStringTextBox_TextChanged(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;
        }

        private void patternDefinitionTextBox_TextChanged(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;
        }
    }
}
