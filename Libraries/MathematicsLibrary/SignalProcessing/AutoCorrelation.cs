using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicsLibrary.SignalProcessing
{
    public class AutoCorrelation
    {
        public static double Compute(List<Int16> sampleList, int lag)
        {
            double autoCorrelation = 0;
            for (int ii = 0; ii < sampleList.Count - lag; ii++)
            {
                autoCorrelation += sampleList[ii] * sampleList[ii + lag];
            }
            return autoCorrelation;
        }

        public static double Compute(List<double> sampleList, int lag)
        {
            double autoCorrelation = 0;
            for (int ii = 0; ii < sampleList.Count - lag; ii++)
            {
                autoCorrelation += sampleList[ii] * sampleList[ii + lag];
            }
            return autoCorrelation;
        }

        public static double ComputeNormalized(List<Int16> sampleList, int lag)
        {
            double autoCorrelation = 0;
            double mean = 0;
            for (int ii = 0; ii < sampleList.Count; ii++)
            {
                mean += (double)sampleList[ii];
            }
            mean /= sampleList.Count;
            double variance = 0;
            for (int ii = 0; ii < sampleList.Count; ii++)
            {
                variance += (sampleList[ii] - mean) * (sampleList[ii] - mean);
            }
            for (int ii = 0; ii < sampleList.Count - lag; ii++)  // removed -1 in "lag-1" 20160824
            {
                autoCorrelation += ((double)sampleList[ii] - mean) * ((double)sampleList[ii + lag] - mean) / variance;
            }
            return autoCorrelation;
        }
    }
}
