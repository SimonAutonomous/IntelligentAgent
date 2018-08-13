using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OOPExample
{
    public partial class OOPMainForm : Form
    {
        public OOPMainForm()
        {
            InitializeComponent();
        }

        private void runExampleButton_Click(object sender, EventArgs e)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.SideLengthX = 3;
            rectangle.SideLengthY = 2;
            double area = rectangle.Area;
            classExampleTextBox.Text = "Side lengths: " + rectangle.SideLengthX.ToString() + ", " +
                rectangle.SideLengthY.ToString() + ", Area: " + area.ToString() + "\r\n";
            rectangle.SideLengthX = 1;
            area = rectangle.ComputeArea();
            classExampleTextBox.Text += "Side lengths: " + rectangle.SideLengthX.ToString() + ", " +
            rectangle.SideLengthY.ToString() + ", Area: " + area.ToString() + "\r\n";
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
