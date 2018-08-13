using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventHandlerExample
{
    public class ProgressEventArgs: EventArgs
    {
        private int sumsCompleted;

        public ProgressEventArgs(int sumsCompleted)
        {
            this.sumsCompleted = sumsCompleted;
        }

        public int SumsCompleted
        {
            get { return sumsCompleted; }
        }
    }
}
