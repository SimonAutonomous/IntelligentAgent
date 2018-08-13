using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SpeechSynthesisLibrary.FormantSynthesis
{
    [DataContract]
    public class WordToSoundMapping
    {
        #region Fields
        private string word;
        private List<string> soundNameList;
        #endregion

        #region Constructor
        public WordToSoundMapping()
        {
            word = "";
            soundNameList = new List<string>();
        }
        #endregion

        #region Properties
        [DataMember]
        public string Word
        {
            get { return word; }
            set { word = value; }
        }

        [DataMember]
        public List<string> SoundNameList
        {
            get { return soundNameList; }
            set { soundNameList = value; }
        }
        #endregion
    }
}
