using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicsLibrary.Functions
{
    public class Polynomial: MathematicalFunction
    {
        List<double> coefficientList;

        public Polynomial(int degree)
        {
            coefficientList = new List<double>();
            for (int ii = 0; ii <= degree; ii++)
            {
                coefficientList.Add(0);
            }
        }

        public override double GetValue(double x)
        {
            double currentPower = 1;
            double functionValue = 0;
            for (int ii = 0; ii < coefficientList.Count; ii++)
            {
                functionValue += coefficientList[ii] * currentPower;
                currentPower *= x;
            }
            return functionValue;
        }

        public List<double> CoefficientList
        {
            get { return coefficientList; }
            set { coefficientList = value; }
        }
    }
}
