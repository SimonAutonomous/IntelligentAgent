using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunicationLibrary
{
    public class DataPacketEventArgs: EventArgs
    {
        private DataPacket dataPacket;
        private string senderID;

        public DataPacketEventArgs(DataPacket dataPacket, string senderID)
        {
            this.dataPacket = dataPacket.Copy();
            this.senderID = senderID;
        }

        public DataPacket DataPacket
        {
            get { return dataPacket; }
        }

        public string SenderID
        {
            get { return senderID; }
        }
    }
}
