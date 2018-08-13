using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CommunicationLibrary
{
    public class DataPacket
    {
        private const int ITEMS_PER_PACKET = 4;

        private DateTime timeStamp;
        private string senderName;
        private string message;
        private int checkSum; // Simply the total byte sum of the message, appended at the end.

        private const char SEPARATION_CHARACTER = '_';

        public DataPacket Copy()
        {
            DataPacket copiedDataPacket = new DataPacket();
            copiedDataPacket.timeStamp = timeStamp;
            copiedDataPacket.senderName = senderName;
            copiedDataPacket.message = message;
            copiedDataPacket.checkSum = checkSum;
            return copiedDataPacket;
        }

        public int GetCheckSum(byte[] dataAsBytes)
        {   
            int checkSum = 0;
            foreach (Byte dataByte in dataAsBytes) { checkSum += (int)dataByte; }
            return checkSum;
        }

        public byte[] AsBytes()
        {
            string tmpString = timeStamp.ToString("yyMMddHHmmssfff") + SEPARATION_CHARACTER + senderName + SEPARATION_CHARACTER + message + SEPARATION_CHARACTER;
            byte[] dataAsBytes = Encoding.ASCII.GetBytes(tmpString);
            int checkSum = GetCheckSum(dataAsBytes);
            string dataPacketAsString = tmpString + checkSum.ToString();
            byte[] dataPacketAsBytes = Encoding.ASCII.GetBytes(dataPacketAsString);
            return dataPacketAsBytes;
        }

        public Boolean Generate(byte[] rawDataPacket)
        {
            string rawDataString = Encoding.ASCII.GetString(rawDataPacket);
            string[] rawDataStringSplit = rawDataString.Split(new char[] { SEPARATION_CHARACTER }, StringSplitOptions.RemoveEmptyEntries);
            if (rawDataStringSplit.Length != ITEMS_PER_PACKET) { return false; }
            else
            {
                Boolean checkSumAvailable = int.TryParse(rawDataStringSplit[3], out checkSum);
                if (checkSumAvailable)
                {
                    string tmpString = rawDataStringSplit[0] + SEPARATION_CHARACTER;
                    for (int ii = 1; ii < rawDataStringSplit.Length - 1; ii++)
                    {
                        tmpString += rawDataStringSplit[ii] + SEPARATION_CHARACTER;
                    }
                    byte[] dataAsBytes = Encoding.ASCII.GetBytes(tmpString);
                    int inferredCheckSum = GetCheckSum(dataAsBytes);
                    if (inferredCheckSum == checkSum) // Message OK
                    {
                        this.timeStamp = DateTime.Now; // ToDo: Change here: Get actual time stamp!
                        this.senderName = rawDataStringSplit[1];
                        this.message = rawDataStringSplit[2];
                        return true;
                    }
                    else { return false; }
                }
                else { return false; }
            }
        }

        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        public string SenderName
        {
            get { return senderName; }
            set { senderName = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
