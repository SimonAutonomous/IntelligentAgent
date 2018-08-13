using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioLibrary
{
    public class WAVRecorderEventArgs: EventArgs
    {
        #region Fields
        private byte[] recordedBytes;
        private int numberOfBytes;
        #endregion

        #region Constructors
        public WAVRecorderEventArgs(byte[] recordedBytes, int numberOfBytes)
        {
            this.recordedBytes = recordedBytes;
            this.numberOfBytes = numberOfBytes;
        }
        #endregion

        #region Properties
        public byte[] RecordedBytes
        {
            get {return recordedBytes;}
        }

        public int NumberOfBytes
        {
            get { return numberOfBytes; }
        }
        #endregion
    }
}
