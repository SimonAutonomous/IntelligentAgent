using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioLibrary
{
    public class WAVSoundEventArgs: EventArgs
    {
        #region Fields
        private WAVSound sound;
        #endregion

        #region Constructor
        public WAVSoundEventArgs(WAVSound sound)
        {
            this.sound = sound.Copy();
        }
        #endregion

        #region Properties
        public WAVSound Sound
        {
            get { return sound; }
        }
        #endregion
    }
}
