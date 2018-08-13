using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicsLibrary.Functions
{
    public class QuadraticFunction: Polynomial
    {
        public QuadraticFunction(): base(2) { }

        public static List<double> GetRandomCoefficients(double xMin, double xMax, double yMin, double yMax, Random randomNumberGenerator, double bRange, double cRange)
        {
            double a = 1;  // constant
            double b = 0;  // linear coefficient
            double c = 0;  // quadratic coefficient
            Boolean ok = false;
            while (!ok)
            {
                a = yMax*randomNumberGenerator.NextDouble();
                b = -bRange + 2*bRange * randomNumberGenerator.NextDouble();
                c = -cRange + 2*cRange * randomNumberGenerator.NextDouble();
                ok = true;
                double leftEdgeValue = a;
                double rightEdgeValue = a + b * xMax + c * xMax * xMax;
                if (Math.Abs(c) < double.Epsilon)  // linear function
                {
                    if (((a + b) < yMin) || ((a + b) > yMax)) { ok = false; } // value < 0 or > 1 at some point
                }
                else if (((-b / (2*c)) > xMin) && ((-b / (2*c)) < xMax)) // Extremum in (xMin,xMax)
                {
                    if (c < 0)  // maximum
                    {
                        if ((a - (b * b) / (4 * c)) > yMax) { ok = false; }
                        else if ((rightEdgeValue < yMin) || (leftEdgeValue < yMin)) { ok = false; }  // Minimum < 0
                    }
                    else if (c > 0) // minimum
                    {
                        if ((a - (b * b) / (4 * c)) < yMin) { ok = false; }
                        else if ((rightEdgeValue > yMax) || (leftEdgeValue > yMax)) { ok = false; } // Maximum > 0
                    }
                }
                else
                {
                    if ((rightEdgeValue < yMin) || (rightEdgeValue > yMax) || (leftEdgeValue < yMin) || (leftEdgeValue > yMax)) { ok = false; }
                }
            }
            return new List<double>() { a, b, c };
        }


    }
}
