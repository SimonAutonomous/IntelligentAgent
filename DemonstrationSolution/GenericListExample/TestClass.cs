using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericListExample
{
    public class TestClass
    {
        private int integerField;
        private double doubleField;

        public TestClass Copy()  // Note: can use Clone, but be careful when copying non-simple objects!
        {
            TestClass copiedObject = new TestClass();
            copiedObject.IntegerProperty = integerField;
            copiedObject.DoubleProperty = doubleField;
            return copiedObject;
        }

        public string AsString()
        {
            string objectAsString = integerField.ToString() + " " + doubleField.ToString();
            return objectAsString;
        }

        public int IntegerProperty
        {
            get { return integerField; }
            set { integerField = value; }
        }

        public double DoubleProperty
        {
            get { return doubleField; }
            set { doubleField = value; }
        }
    }
}
