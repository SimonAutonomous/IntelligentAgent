using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeechSynthesisLibrary.TDPSOLA
{
    public class PitchMarkSpecification
    {
        private List<double> pitchTimeList;

        public List<double> PitchTimeList
        {
            get { return pitchTimeList; }
            set { pitchTimeList = value; }
        }
    }
}
