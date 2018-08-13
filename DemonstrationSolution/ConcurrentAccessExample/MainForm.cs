using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ConcurrentAccessExample
{
    public partial class MainForm : Form
    {
        private const int PRINT_INTERVAL = 100000;
        private const int MAXIMUM_NUMBER_OF_ITERATIONS = 1000000;

        private AccessTestClassWithoutLock accessTestObjectWithoutLock;
        private AccessTestClassWithLock accessTestObjectWithLock;
        private Thread additionThread;
        private Boolean running = false;
        private Thread checkThread;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ThreadSafeShowError(int numberOfChecks)
        {
            string information = "Sum different from 10 in iteration " + numberOfChecks.ToString() + "! Stopping.";
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => informationListBox.Items.Add(information))); }
            else { informationListBox.Items.Add(information); }
        }

        private void ThreadSafeShowProgress(int numberOfIterations)
        {
            string information = numberOfIterations + " iterations completed";
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => informationListBox.Items.Add(information))); }
            else { informationListBox.Items.Add(information); }
        }

        private void ThreadSafeToggleButtons()
        {
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => ToggleButtons())); }
            else { ToggleButtons(); }
        }

        private void ToggleButtons()
        {
            runWithLockButton.Enabled = true;
            runWithoutLockButton.Enabled = true;
            additionThread = null;
            checkThread = null;
            accessTestObjectWithLock = null;
            accessTestObjectWithoutLock = null;
        }

        private void AddLoop(Boolean useLock)
        {
            while (running)
            {
                if (useLock) { accessTestObjectWithLock.AddElement(); }
                else { accessTestObjectWithoutLock.AddElement(); }
            }
        }

        private void CheckLoop(Boolean useLock)
        {
            int checkSum;
            int numberOfIterations = 0;
            while (running)
            {
                if (useLock) { checkSum = accessTestObjectWithLock.GetCheckSum(); }
                else { checkSum = accessTestObjectWithoutLock.GetCheckSum(); }
                if (checkSum != 10) 
                { 
                    running = false;
                    ThreadSafeShowError(numberOfIterations);
                }
                numberOfIterations++;
                if (numberOfIterations % PRINT_INTERVAL == 0) 
                { ThreadSafeShowProgress(numberOfIterations); }
                if (numberOfIterations == MAXIMUM_NUMBER_OF_ITERATIONS) { running = false; }
            }
            ThreadSafeToggleButtons();
        }

        private void runWithoutLockButton_Click(object sender, EventArgs e)
        {
            runWithoutLockButton.Enabled = false;
            runWithLockButton.Enabled = false;
            informationListBox.Items.Clear();
            accessTestObjectWithoutLock = new AccessTestClassWithoutLock();
            additionThread = new Thread(new ThreadStart(() => AddLoop(false)));
            checkThread = new Thread(new ThreadStart(() => CheckLoop(false)));
            running = true;
            additionThread.Start();
            checkThread.Start();
        }

        private void runWithLockButton_Click(object sender, EventArgs e)
        {
            runWithoutLockButton.Enabled = false;
            runWithLockButton.Enabled = false;
            informationListBox.Items.Clear();
            accessTestObjectWithLock = new AccessTestClassWithLock();
            additionThread = new Thread(new ThreadStart(() => AddLoop(true)));
            checkThread = new Thread(new ThreadStart(() => CheckLoop(true)));
            running = true;
            additionThread.Start();
            checkThread.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
