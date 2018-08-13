using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CommunicationLibrary
{
    public class ClientState
    {
        private string clientName; // The client name communicated by the client.
        private string clientID;   // The (unique) client ID assigned by the server
        private Boolean connected;
        private byte[] receiveBuffer;
        private byte[] sendBuffer;
        private Socket clientSocket;

        public ClientState(int bufferSize, Socket clientSocket)
        {
            connected = false;
            receiveBuffer = new byte[bufferSize];
            this.clientSocket = clientSocket;
        }

        public Boolean Connected
        {
            get { return connected; }
            set { connected = value; }
        }  

        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }

        public string ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        public byte[] ReceiveBuffer
        {
            get { return receiveBuffer; }
        }

        public byte[] SendBuffer
        {
            get { return sendBuffer; }
            set { sendBuffer = value; }
        }

        public Socket ClientSocket
        {
            get { return clientSocket; }
        }
    }
}
