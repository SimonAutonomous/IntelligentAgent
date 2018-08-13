using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetDataAcquisitionLibrary
{
    public class ErrorEventArgs: EventArgs
    {
        private string errorString;

        public ErrorEventArgs(string errorString)
        {
            this.errorString = errorString;
        }

        public string ErrorString
        {
            get { return errorString; }
        }
    }
}
