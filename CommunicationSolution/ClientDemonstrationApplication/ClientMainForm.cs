using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommunicationLibrary;

namespace ClientDemonstrationApplication
{
    public partial class ClientMainForm : Form
    {
        private const string ipAddressString = "127.0.0.1";
        private const int port = 7; // Just an example..

        private Client client;

        public ClientMainForm()
        {
            InitializeComponent();
        }

        private void HandleClientProgress(object sender, CommunicationProgressEventArgs e)
        {
            string information = e.DateTime.ToString("yyyyMMdd HHmmss.fff: ") + "[" + e.Action.ToString() + "] " + e.Message;
            if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => messageListBox.Items.Insert(0, information))); }
            else { messageListBox.Items.Insert(0, information); }
        }

        private void HandleClientError(object sender, CommunicationErrorEventArgs e)
        {
            string information = e.DateTime.ToString("yyyyMMdd HHmmss.fff: ") + "[" + e.Originator.ToString() + "] " + e.Message;
            if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => messageListBox.Items.Insert(0, information))); }
            else { messageListBox.Items.Insert(0, information); }
        }

        private void HandleClientReceived(object sender, DataPacketEventArgs e)
        {
            string information = e.DataPacket.TimeStamp.ToString("yyyyMMdd HHmmss.fff: ") + 
                                 e.DataPacket.Message + " from " + e.SenderID.ToString();
            if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => messageListBox.Items.Insert(0, information))); }
            else { messageListBox.Items.Insert(0, information); }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exitToolStripMenuItem.Enabled = false;
            messageListBox.Items.Clear();
            client = new Client();
            client.Name = "Client";
            client.Progress += new EventHandler<CommunicationProgressEventArgs>(HandleClientProgress);
            client.Error += new EventHandler<CommunicationErrorEventArgs>(HandleClientError);
            client.Received += new EventHandler<DataPacketEventArgs>(HandleClientReceived);
            client.Connect(ipAddressString, port);
            helloButton.Enabled = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            helloButton.Enabled = false;
            exitToolStripMenuItem.Enabled = true;
        }

        private void helloButton_Click(object sender, EventArgs e)
        {
            client.Send("Hello");
        }
    }
}
