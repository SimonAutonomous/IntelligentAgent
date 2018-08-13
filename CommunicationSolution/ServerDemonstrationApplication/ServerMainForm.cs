using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommunicationLibrary;

namespace ServerDemonstrationApplication
{
    public partial class ServerMainForm : Form
    {
        private const string ipAddressString = "127.0.0.1";
        private const int port = 7; // Just an example..

        private Server server;

        public ServerMainForm()
        {
            InitializeComponent();
        }

        private void HandleServerProgress(object sender, CommunicationProgressEventArgs e)
        {
            string information = e.DateTime.ToString("yyyyMMdd HHmmss.fff: ") + "[" + e.Action.ToString() + "] " + e.Message;
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => messageListBox.Items.Insert(0, information))); }
            else { messageListBox.Items.Insert(0, information); }
        }

        private void HandleServerError(object sender, CommunicationErrorEventArgs e)
        {
            string information = e.DateTime.ToString("yyyyMMdd HHmmss.fff: ") + "[" + e.Originator.ToString()+"]" + e.Message;
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => messageListBox.Items.Insert(0, information))); }
            else { messageListBox.Items.Insert(0, information); }
        }

        private void HandleServerReceived(object sender, DataPacketEventArgs e)
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
            server = new Server();
            server.Name = "Server";
            server.Progress += new EventHandler<CommunicationProgressEventArgs>(HandleServerProgress);
            server.Error += new EventHandler<CommunicationErrorEventArgs>(HandleServerError);
            server.Received += new EventHandler<DataPacketEventArgs>(HandleServerReceived);
            server.Connect(ipAddressString, port);
            if (server.Connected) 
            {
                helloButton.Enabled = true;
                server.AcceptClients();
                toolStrip1.Focus();
            }
        }

        private void helloButton_Click(object sender, EventArgs e)
        {
            List<string> clientIDList = server.GetClientIDList();
            foreach (string clientID in clientIDList)
            {
                server.Send(clientID, "Hello from server");
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Disconnect();
            helloButton.Enabled = false;
            exitToolStripMenuItem.Enabled = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
