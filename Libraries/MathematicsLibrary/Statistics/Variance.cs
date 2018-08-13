using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicsLibrary.Statistics
{
    public class Variance
    {
        public static double Compute(List<double> valueList)
        {
            double variance = 0;
            if (valueList.Count > 0)
            {
                double average = valueList.Average();
                for (int ii = 0; ii < valueList.Count; ii++)
                {
                    variance += (valueList[ii] - average) * (valueList[ii] - average);
                }
                variance /= (double)(valueList.Count);
            }
            return variance;
        }

        public static double ComputeUnbiased(List<double> valueList)
        {
            double variance = Variance.Compute(valueList);
            double unbiasedVariance = variance * valueList.Count / (double)(valueList.Count - 1);
            return unbiasedVariance;
        }
    }
}
