using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SerializationExample
{
    [DataContract]
    public class SerializationTestClass
    {
        private int intParameter;
        private double doubleParameter;
        private double doubleParameter2;
        private List<int> integerList;

        [DataMember]
        public int IntParameter
        {
            get { return intParameter; }
            set { intParameter = value; }
        }

        [DataMember]
        public double DoubleParameter
        {
            get { return doubleParameter; }
            set { doubleParameter = value; }
        }

        public double DoubleParameter2
        {
            get { return doubleParameter2; }
            set { doubleParameter2 = value; }
        }

        [DataMember]
        public List<int> IntegerList
        {
            get { return integerList; }
            set { integerList = value; }
        }
    }
}
