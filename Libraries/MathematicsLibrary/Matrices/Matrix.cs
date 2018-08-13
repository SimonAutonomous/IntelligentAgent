using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicsLibrary.Matrices
{
    public class Matrix
    {
        List<List<double>> elements;

        public Matrix()
        {
            elements = new List<List<double>>();
        }

        public Matrix(int nRows, int nColumns)
        {
            elements = new List<List<double>>();
            for (int ii = 0; ii < nRows; ii++)
            {
                List<double> row = new List<double>();
                for (int jj = 0; jj < nColumns; jj++)
                {
                    row.Add(0);
                }
                elements.Add(row);
            }
        }

        public List<List<double>> Elements
        {
            get { return elements; }
            set { elements = value; }
        }
    }
}
