using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunicationLibrary
{
    public class CommunicationErrorEventArgs: EventArgs
    {
        private DateTime dateTime;
        private string originator;  // The entity _reporting_ the error
        private string message;

        public CommunicationErrorEventArgs(string originator, string message)
        {
            this.dateTime = DateTime.Now;
            this.originator = originator;
            this.message = message;
        }

        public DateTime DateTime
        {
            get { return dateTime; }
        }

        public string Originator
        {
            get { return originator; }
        }

        public string Message
        {
            get { return message; }
        }
    }
}
