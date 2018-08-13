using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicsLibrary.SignalProcessing
{
    public class FirstOrderLowPassFilter: LinearDigitalFilter
    {
        public FirstOrderLowPassFilter():base()
        {
            // Default values (identity filter)
            aList.Add(1);    // Note that aList has two elements, to get the indices right (a[0] = 1, see LinearDigitalFilter()).
            bList.Add(0);
        }

        public void SetAlpha(double alpha)
        {
            aList[1] = (1 - alpha);
            bList[0] = alpha;
        }
    }
}
