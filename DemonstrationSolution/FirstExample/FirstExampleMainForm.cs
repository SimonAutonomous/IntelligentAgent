using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FirstExample
{
    public partial class FirstExampleMainForm : Form
    {
        public FirstExampleMainForm()
        {
            InitializeComponent();
        }

        private string GenerateResponse()
        {
            string hello = "Hello user. Today is a ";
            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            hello += dayOfWeek + ".";
            return hello;
        }

        private void helloButton_Click(object sender, EventArgs e)
        {
            string response = GenerateResponse();
            responseTextBox.Text = response;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
