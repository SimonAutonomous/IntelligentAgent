using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EventHandlerExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            EventTestClass eventTestObject = new EventTestClass();
            eventTestObject.Started += new EventHandler(HandleStarted);
            eventTestObject.Progress += new EventHandler<ProgressEventArgs>(HandleProgress);
            eventTestObject.Run();
        }

        private void HandleStarted(object sender, EventArgs e)
        {
            string startInformationString = "Started";
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => progressListBox.Items.Add(startInformationString))); }
            else { progressListBox.Items.Add(startInformationString); }
        }

        private void HandleProgress(object sender, ProgressEventArgs e)
        {
            string progressInformationString = "Sums completed: " + e.SumsCompleted.ToString();
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => progressListBox.Items.Add(progressInformationString))); }
            else { progressListBox.Items.Add(progressInformationString); }
        }
    }
}
