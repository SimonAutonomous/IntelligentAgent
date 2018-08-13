using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomUserControlsLibrary;
using SpeechSynthesisLibrary.TDPSOLA;

namespace VoiceModificationApplication
{
    public partial class VoiceModificationParameterSettingsForm : Form
    {
        private SpeechModifier speechModifier;

        public VoiceModificationParameterSettingsForm()
        {
            InitializeComponent();
        }

        public void SetSpeechModifier(SpeechModifier speechModifier)
        {
            this.speechModifier = speechModifier;
            speechTypeEstimatorPropertyPanel.SetObject(this.speechModifier.SpeechTypeEstimator);
            pitchPeriodEstimatorPropertyPanel.SetObject(this.speechModifier.PitchPeriodEstimator);
            pitchMarkEstimatorPropertyPanel.SetObject(this.speechModifier.PitchMarkEstimator);
        }
    }
}
