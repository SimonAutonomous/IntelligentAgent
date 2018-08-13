using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicsLibrary.SignalProcessing
{
    // Implements a filter of the kind
    //
    // s(k) + a1*s(k-1) + a2*s(k-2) + ... a3*s(k-p) =
    //        b0*x(k) + b1*x(k-1) + bq*s(k-q),
    // where s(k) is the output at step k and x(k) is the input
    //
    public class LinearDigitalFilter
    {
        protected List<double> aList;  // Note a[0] is always = 1 (not used)
        protected List<double> bList;
        protected List<double> outputList; // s(k)
        protected List<double> inputList; // x(k)

        public LinearDigitalFilter()
        {
            aList = new List<double>();
            aList.Add(1);
            bList = new List<double>();
            outputList = new List<double>();
            inputList = new List<double>();
        }

        public void Reset()
        {
            outputList = new List<double>();
            inputList = new List<double>();
        }


        // MW ToDo:
        // NOTE: This code needs to be checked (20170804)  (correct indices?)
        public void Step(double input)
        {
            inputList.Add(input);
            int topIndex = Math.Min(aList.Count-1, outputList.Count);
            int lastOutputIndex = outputList.Count - 1;
            double weightedPreviousOutput = 0;
            for (int ii = 0; ii < topIndex; ii++)
            {
                weightedPreviousOutput += aList[ii+1] * outputList[lastOutputIndex - ii];
            }
            topIndex = Math.Min(bList.Count, inputList.Count);
            int lastInputIndex = inputList.Count - 1;
            double weightedInput = 0;
            for (int ii = 0; ii < topIndex; ii++)
            {
                weightedInput += bList[ii] * inputList[lastInputIndex - ii];
            }
            double output = weightedInput - weightedPreviousOutput;
            outputList.Add(output);
        }

        public List<double> OutputList
        {
            get { return outputList; }
        }
    }
}
