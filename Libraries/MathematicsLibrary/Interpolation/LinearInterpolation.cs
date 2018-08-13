using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicsLibrary.Interpolation
{
    public class LinearInterpolation
    {
        // Interpolates the input series (ti
        // Note: numberOfPoints must be at least 2!
        public static List<List<double>> Interpolate(List<List<double>> xyList, int numberOfPoints)
        {
            List<double> xList = xyList[0];
            List<double> yList = xyList[1];

            List<double> interpolatedXList = new List<double>();
            List<double> interpolatedYList = new List<double>();

            double minimumX = xList[0];
            double maximumX = xList.Last();

            double deltaTime = (maximumX - minimumX) / (numberOfPoints - 1);
            for (int ii = 0; ii < numberOfPoints; ii++)
            {
                double interpolatedX = minimumX + ii*deltaTime;
                interpolatedXList.Add(interpolatedX);
            }
            // Add the first point (at the first x-Value)
            interpolatedYList.Add(yList[0]);
            int index = 0;
            int interpolationIndex = 1;
            while (interpolationIndex < (interpolatedXList.Count-1))
            {
                double x = interpolatedXList[interpolationIndex];
                while (xList[index] < x)
                {
                    index++;
                    if (index == xList.Count)  // Should not happen...
                    {
                        index--;  
                        break;
                    }
                }
                int indexBefore = index-1;
                int indexAfter = index;
                double xBefore = xList[indexBefore];
                double xAfter = xList[indexAfter];
                // Debug code for catching errors (x should always be in the range [xBefore, xAfter])
           /*     if ((xBefore > x) || (xAfter < x))
                {
                }  */
                double yBefore = yList[indexBefore];
                double yAfter = yList[indexAfter];
                double interpolatedY = yBefore + (x - xBefore) * (yAfter - yBefore) / (xAfter - xBefore);
                interpolatedYList.Add(interpolatedY);
                interpolationIndex++;
                index = indexBefore;
            }
            // Add the last point (at the last x-Value)
            interpolatedYList.Add(yList.Last());
            return new List<List<double>>() { interpolatedXList, interpolatedYList };
        }
    }
}
