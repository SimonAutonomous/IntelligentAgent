using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TheadingExample
{
    public partial class MainForm : Form
    {
        private const int UPPER_LIMIT = 100000;
        private const int PRINT_INTERVAL = 10000;

        private Thread computationThread;
        private Boolean running = false;

        public MainForm()
        {
            InitializeComponent();
        }

        // =================================================================
        //
        // Run the computation on the GUI thread. This freezes the GUI (not elegant!)
        //
        // =================================================================
        private void runInSingleThreadButton_Click(object sender, EventArgs e)
        {
            runInSingleThreadButton.Enabled = false;
            runMultiThreadedButton.Enabled = false;
            progressListBox.Items.Clear();
            progressListBox.Items.Add("Starting");
            for (int k = 1; k <= UPPER_LIMIT; k++)
            {
                double sum = 0;
                for (int j = 1; j <= k; j++) { sum += j*j; }
                if (k % PRINT_INTERVAL == 0) { ShowProgress("k = " + k.ToString()); }
            }
            progressListBox.Items.Add("Done");
            runInSingleThreadButton.Enabled = true;
            runMultiThreadedButton.Enabled = true;
        }

        private void ShowProgress(string progressInformation)
        {
            progressListBox.Items.Add(progressInformation);
        }

        // =================================================================
        //
        // Run the computation in a separate thread.
        //
        // =================================================================
        private void runMultiThreadedButton_Click(object sender, EventArgs e)
        {
            runInSingleThreadButton.Enabled = false;
            runMultiThreadedButton.Enabled = false;
            progressListBox.Items.Clear();
            progressListBox.Items.Add("Starting");
            computationThread = new Thread(new ThreadStart(() => ComputationLoop()));
            computationThread.Start();
        }

        private void ComputationLoop()
        {
            for (int k = 1; k <= UPPER_LIMIT; k++)
            {
                double sum = 0;
                for (int j = 1; j <= k; j++) { sum += j * j; }
                if (k % PRINT_INTERVAL == 0) { ThreadSafeShowProgress("k = " + k.ToString()); }
            }
            ThreadSafeHandleDone();
        }

        private void ThreadSafeShowProgress(string progressInformation)
        {
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => ShowProgress(progressInformation)));}
            else { ShowProgress(progressInformation);}
        }

        private void ThreadSafeHandleDone()
        {
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => HandleDone())); }
            else { HandleDone(); }
        }

        private void HandleDone()
        {
            runMultiThreadedButton.Enabled = true;
            runInSingleThreadButton.Enabled = true;
            progressListBox.Items.Add("Done!");
        }
    }
}
