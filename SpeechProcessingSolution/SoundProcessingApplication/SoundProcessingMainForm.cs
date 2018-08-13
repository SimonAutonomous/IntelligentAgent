using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioLibrary;
using CustomUserControlsLibrary;
using ObjectSerializerLibrary;

namespace SoundProcessingApplication
{
    public partial class SoundProcessingMainForm : Form
    {
        private WAVRecorder recorder = new WAVRecorder();
        private Boolean recording = false;

        private const double VOLUME_RAISE_STEP = 1.1;
        private const double VOLUME_REDUCTION_STEP = 0.9;

        public SoundProcessingMainForm()
        {
            InitializeComponent();
            recorder.DeviceId = 0;
            recorder.SampleRate = 16000;
        }

        private void recordingButton_Click(object sender, EventArgs e)
        {
            if (!recording)
            {
                recording = true;
                recorder.Start();
                recordingButton.Text = "Stop recording";
            }
            else
            {
                recorder.Stop();
                byte[] recordedBytes = recorder.GetAllRecordedBytes();
                if (recordedBytes != null)
                {
                    if (recordedBytes.Length > 0)
                    {
                        WAVSound sound = new WAVSound("", recorder.SampleRate, recorder.NumberOfChannels, recorder.NumberOfBitsPerSample);
                        sound.AppendSamplesAsBytes(recordedBytes);
                        soundVisualizer.SetSound(sound);
                    }
                }
                recordingButton.Text = "Start recording";
                saveSoundToolStripMenuItem.Enabled = true;
                raiseVolumeButton.Enabled = true;
                reduceVolumeButton.Enabled = true;
                playButton.Enabled = true;
                playButton.Enabled = true;
                filterToolStrip.Enabled = true;
                recording = false;
            }
        }

        private void loadSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "WAV files (*.wav)|*.wav";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    WAVSound sound = new WAVSound();
                    sound.LoadFromFile(openFileDialog.FileName);
                    soundVisualizer.SetSound(sound);
                    saveSoundToolStripMenuItem.Enabled = true;
                    raiseVolumeButton.Enabled = true;
                    reduceVolumeButton.Enabled = true;
                    playButton.Enabled = true;
                    filterToolStrip.Enabled = true;
                }
            }
        }

        private void saveSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "WAV files (*.wav)|*.wav";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    soundVisualizer.Sound.SaveToFile(saveFileDialog.FileName);
                }
            }
        }

        private void raiseVolumeButton_Click(object sender, EventArgs e)
        {
            WAVSound sound = soundVisualizer.Sound;
            sound.SetRelativeVolume(VOLUME_RAISE_STEP);
            soundVisualizer.SetSound(sound);
        }

        private void reduceVolumeButton_Click(object sender, EventArgs e)
        {
            WAVSound sound = soundVisualizer.Sound;
            sound.SetRelativeVolume(VOLUME_REDUCTION_STEP);
            soundVisualizer.SetSound(sound);
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            SoundPlayer soundPlayer = new SoundPlayer();
            WAVSound sound = soundVisualizer.Sound.Copy();
            sound.GenerateMemoryStream();
            sound.WAVMemoryStream.Position = 0; // Manually rewind stream 
            soundPlayer.Stream = null;
            soundPlayer.Stream = sound.WAVMemoryStream;
            soundPlayer.PlaySync();
        }

        private void lowPassFilterButton_Click(object sender, EventArgs e)
        {
            double lowpassCutoffFrequency = double.Parse(lowpassFilterCutoffTextBox.Text);
            WAVSound sound = soundVisualizer.Sound.Copy();
            sound.LowPassFilter(lowpassCutoffFrequency);
            soundVisualizer.SetSound(sound);
        }

        private void highpassFilterButton_Click(object sender, EventArgs e)
        {
            double highpassCutoffFrequency = double.Parse(highpassFilterCutoffTextBox.Text);
            WAVSound sound = soundVisualizer.Sound.Copy();
            sound.HighPassFilter(highpassCutoffFrequency);
            soundVisualizer.SetSound(sound);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
