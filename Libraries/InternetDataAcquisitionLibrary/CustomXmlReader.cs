using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;

namespace InternetDataAcquisitionLibrary
{
    public class CustomXmlReader: XmlTextReader
    {
        private Boolean readingDate = false;
        private string customDateTimeFormat;

        public CustomXmlReader(Stream s) : base(s) { }

        public CustomXmlReader(string url) : base(url) { }

        public override void ReadStartElement()
        {
            if (string.Equals(base.NamespaceURI, string.Empty, StringComparison.InvariantCultureIgnoreCase) &&
                (string.Equals(base.LocalName, "lastBuildDate", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(base.LocalName, "pubDate", StringComparison.InvariantCultureIgnoreCase)))
            {
                readingDate = true;
            }
            base.ReadStartElement();
        }

        public override void ReadEndElement()
        {
            if (readingDate)
            {
                readingDate = false;
            }
            base.ReadEndElement();
        }

        public override string ReadString()
        {
            if (readingDate)
            {
                string dateString = base.ReadString();
                DateTime dateTime;
                Boolean ok = DateTime.TryParse(dateString, out dateTime);
                if (!ok)
                {
                    dateTime = DateTime.ParseExact(dateString, customDateTimeFormat, CultureInfo.InvariantCulture);
                }
                return dateTime.ToUniversalTime().ToString("R", CultureInfo.InvariantCulture);
            }
            else
            {
                return base.ReadString();
            }
        }

        public void SetCustomDateTimeFormat(string customDateTimeFormat)
        {
            this.customDateTimeFormat = customDateTimeFormat;
        }
    }
}
