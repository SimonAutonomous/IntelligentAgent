using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuxiliaryLibrary
{
    public class StringEventArgs: EventArgs
    {
        private string stringValue;

        public StringEventArgs(string stringData)
        {
            this.stringValue = stringData;
        }

        public string StringValue
        {
            get { return stringValue; }
        }
    }
}
