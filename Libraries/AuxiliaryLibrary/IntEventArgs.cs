using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuxiliaryLibrary
{
    public class IntEventArgs: EventArgs
    {
        private int intValue;

        public IntEventArgs(int intValue)
        {
            this.intValue = intValue;
        }

        public int IntValue
        {
            get { return intValue; }
        }
    }
}
