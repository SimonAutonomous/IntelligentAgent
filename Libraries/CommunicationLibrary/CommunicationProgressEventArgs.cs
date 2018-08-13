using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunicationLibrary
{
    public class CommunicationProgressEventArgs: EventArgs
    {
        private DateTime dateTime;
        private CommunicationAction action;
        private string message;

        public CommunicationProgressEventArgs(CommunicationAction action, string message)
        {
            this.dateTime = DateTime.Now;
            this.action = action;
            this.message = message;
        }

        public DateTime DateTime
        {
            get { return dateTime; }
        }

        public CommunicationAction Action
        {
            get {return action;}
        }

        public string Message
        {
            get { return message; }
        }
    }
}
