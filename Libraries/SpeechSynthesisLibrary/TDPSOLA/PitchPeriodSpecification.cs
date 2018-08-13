using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeechSynthesisLibrary.TDPSOLA
{
    public class PitchPeriodSpecification
    {
        private List<Tuple<double, double>> timePitchPeriodTupleList; // Item1 = time, Item2 = pitch period

        public PitchPeriodSpecification()
        {
            timePitchPeriodTupleList = new List<Tuple<double, double>>();
        }

        public void GenerateFromList(List<List<double>> timePitchPeriodList)
        {
            for (int ii = 0; ii < timePitchPeriodList.Count; ii++)
            {
                timePitchPeriodTupleList.Add(new Tuple<double, double>(timePitchPeriodList[ii][0], timePitchPeriodList[ii][1]));
            }
        }

        public double GetPitchPeriod(double time)
        {
            if (time < 0)
            {
                return timePitchPeriodTupleList[0].Item2; 
            }
            else if (time > timePitchPeriodTupleList.Last().Item1)
            {
                return timePitchPeriodTupleList.Last().Item2;
            }
            else
            {
                int indexBefore = timePitchPeriodTupleList.FindLastIndex(t => t.Item1 <= time);
                int indexAfter = timePitchPeriodTupleList.FindIndex(t => t.Item1 >= time);
                double timeBefore = timePitchPeriodTupleList[indexBefore].Item1;
                double timeAfter = timePitchPeriodTupleList[indexAfter].Item1;
                double deltaBefore = time - timeBefore;
                double deltaAfter = timeAfter - time;
                double interpolatedPitch = timePitchPeriodTupleList[indexBefore].Item2 +
                    ((time - timeBefore) / (timeAfter - timeBefore)) * (timePitchPeriodTupleList[indexAfter].Item2 -
                    timePitchPeriodTupleList[indexBefore].Item2);
                return interpolatedPitch;
            }
        }

        public double GetMinimumPitchPeriod()
        {
            double minimumPitchPeriod = double.MaxValue;
            for (int ii = 0; ii< timePitchPeriodTupleList.Count; ii++)
            {
                double pitchPeriod = timePitchPeriodTupleList[ii].Item2;
                if (pitchPeriod < minimumPitchPeriod) { minimumPitchPeriod = pitchPeriod; }
            }
            return minimumPitchPeriod;
        }

        public List<Tuple<double, double>> TimePitchPeriodTupleList
        {
            get { return timePitchPeriodTupleList; }
        }
    }
}
