using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using AudioLibrary;

namespace SpeechSynthesisLibrary
{
    [DataContract]
    public abstract class SpeechSynthesizer
    {
        public abstract WAVSound GenerateWord(string word);

        public abstract WAVSound GenerateWordSequence(List<string> wordList, List<double> silenceList);
    }
}
