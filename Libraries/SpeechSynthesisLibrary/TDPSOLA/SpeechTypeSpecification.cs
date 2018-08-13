using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeechSynthesisLibrary.TDPSOLA
{
    public class SpeechTypeSpecification
    {
        private List<Tuple<double, SpeechType>> timeSpeechTypeTupleList; // Item1 = time, Item2 = speechType

        public SpeechTypeSpecification()
        {
            timeSpeechTypeTupleList = new List<Tuple<double, SpeechType>>();
        }

        public SpeechType GetSpeechType(double time)
        {
            if ((time < 0) || (time > timeSpeechTypeTupleList.Last().Item1))
            {
                return SpeechType.Silence; // Outside the time range of the sound.
            }
            else
            {
                int indexBefore = timeSpeechTypeTupleList.FindLastIndex(t => t.Item1 <= time);
                int indexAfter = timeSpeechTypeTupleList.FindIndex(t => t.Item1 >= time);
                double timeBefore = timeSpeechTypeTupleList[indexBefore].Item1;
                double timeAfter = timeSpeechTypeTupleList[indexAfter].Item1;
                double deltaBefore = time - timeBefore;
                double deltaAfter = timeAfter - time;
                if (deltaBefore < deltaAfter)
                {
                    return timeSpeechTypeTupleList[indexBefore].Item2;
                }
                else
                {
                    return timeSpeechTypeTupleList[indexAfter].Item2;
                }
            }
        }

        public void SetSpeechType(int index, SpeechType speechType)
        {
            double time = timeSpeechTypeTupleList[index].Item1;
            Tuple<double, SpeechType> speechTypeTuple = new Tuple<double, SpeechType>(time, speechType);
            timeSpeechTypeTupleList[index] = speechTypeTuple;
        }  

        public void SetUniformSpeechType(SpeechType speechType)
        {
            for (int ii = 0; ii < timeSpeechTypeTupleList.Count; ii++)
            {
                double time = timeSpeechTypeTupleList[ii].Item1;
                Tuple<double, SpeechType> speechTypeTuple = new Tuple<double, SpeechType>(time, speechType);
                timeSpeechTypeTupleList[ii] = speechTypeTuple;
            }
        }

        public int GetIndexDuration()
        {
            int indexDuration = timeSpeechTypeTupleList.Count - 1;
            return indexDuration;
        }

        public List<Tuple<int, int, SpeechType>> GetSegmentTypes()
        {
            List<Tuple<int, int, SpeechType>> segmentTypeList = new List<Tuple<int, int, SpeechType>>();
            int segmentStartIndex = 0;
            int segmentEndIndex = - 1; // Handles the case in which the speech type is unchanged over the sound.
            SpeechType currentSpeechType = timeSpeechTypeTupleList[0].Item2;
            for (int ii = 1; ii < timeSpeechTypeTupleList.Count; ii++)
            {
                SpeechType speechType = timeSpeechTypeTupleList[ii].Item2;
                if (speechType != currentSpeechType)
                {
                    segmentEndIndex = ii - 1;
                    Tuple<int, int, SpeechType> segmentType =
                        new Tuple<int, int, SpeechType>(segmentStartIndex, segmentEndIndex, currentSpeechType);
                    segmentTypeList.Add(segmentType);
                    currentSpeechType = speechType;
                    segmentStartIndex = ii;
                    segmentEndIndex = -1; // To make sure that the final segment is included, see below
                }
            }
            if (segmentEndIndex < 0)
            {
                segmentEndIndex = timeSpeechTypeTupleList.Count - 1;
                Tuple<int, int, SpeechType> segmentType =
                        new Tuple<int, int, SpeechType>(segmentStartIndex, segmentEndIndex, currentSpeechType);
                segmentTypeList.Add(segmentType);
            }
            return segmentTypeList;
        }

        public List<Tuple<double, SpeechType>> TimeSpeechTypeTupleList
        {
            get { return timeSpeechTypeTupleList; }
            set { timeSpeechTypeTupleList = value; }
        }
    }
}
