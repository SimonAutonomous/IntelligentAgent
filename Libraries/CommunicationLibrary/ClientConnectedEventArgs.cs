using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunicationLibrary
{
    public class ClientConnectedEventArgs: EventArgs
    {
        private string clientName;
        private string clientID;

        public ClientConnectedEventArgs(string clientName, string clientID)
        {
            this.clientName = clientName;
            this.clientID = clientID;
        }

        public string ClientName
        {
            get { return clientName; }
        }

        public string ClientID
        {
            get { return clientID; }
        }
    }
}
