using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicsLibrary.Vectors
{
    public class Vector
    {
        private List<double> elements;

        public Vector()
        {
            elements = new List<double>();
        }

        public Vector Copy()
        {
            Vector v = new Vector();
            foreach (double d in this.Elements)
            {
                v.Elements.Add(d);
            }
            return v;
        }

        public Vector(int size)
        {
            elements = new List<double>();
            for (int ii = 0; ii < size; ii++)
            {
                elements.Add(0);
            }
        }

        public void Clear()
        {
            for (int ii = 0; ii < this.Elements.Count; ii++)
            {
                this.Elements[ii] = 0;
            }
        }

        public List<double> Elements
        {
            get { return elements; }
            set { elements = value; }
        }
    }
}
