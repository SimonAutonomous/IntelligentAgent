using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicsLibrary.SignalProcessing
{
    public class AveragingFilter
    {
        public List<double> Run(List<double> inputList)
        {
            List<double> outputList = new List<double>();
            outputList.Add(inputList[0]);
            for (int ii = 1; ii < inputList.Count-1; ii++)
            {
                double output = (inputList[ii-1]+ inputList[ii] + inputList[ii+1])/ 3;
                outputList.Add(output);
            }
            outputList.Add(inputList.Last());
            return outputList;
        }
    }
}
