using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using AudioLibrary;
using AudioLibrary.Visualization;
using MathematicsLibrary.Functions;
using ObjectSerializerLibrary;
using SpeechSynthesisLibrary.FormantSynthesis;
using SpeechSynthesisLibrary.Visualization;

namespace FormantSpeechSynthesisApplication
{
    public partial class MainForm : Form
    {
        private FormantSpeechSynthesizer speechSynthesizer = null;
        private FormantSpecification editorFormantSpecification = null;
        private List<List<FormantSpecification>> formantSpecificationMatrix = null; // Used in the IEA.
        private FormantSpecification ieaSelectedFormantSpecification = null;

        private Random randomNumberGenerator;

        private const int MINIMUM_FUNDAMENTAL_FREQUENCY = 30;
        private const int MAXIMUM_FUNDAMENTAL_FREQUENCY = 300;
        private const int MINIMUM_SAMPLING_FREQUENCY = 8000;
        private const int MAXIMUM_SAMPLING_FREQUENCY = 32000;
        private const double LONG_SILENCE_SOUND_DURATION = 0.08;
        private const double SHORT_SILENCE_SOUND_DURATION = 0.02;

        public MainForm()
        {
            InitializeComponent();
            randomNumberGenerator = new Random();
            GenerateNewSpeechSynthesizer();
            ShowSpeechSynthesizer();
            sentenceSpeechVisualizer.PitchPanelVisible = false;
        }

        private void GenerateNewSpeechSynthesizer()
        {
            speechSynthesizer = new FormantSpeechSynthesizer();
            speechSynthesizer.StorePitch = true;
            FormantSpecification longSilenceSpecification = new FormantSpecification(speechSynthesizer.FundamentalFrequency, speechSynthesizer.SamplingFrequency);
            longSilenceSpecification.Name = "=";
            FormantSettings longSilenceSettings = new FormantSettings();
            longSilenceSettings.SetSilence(LONG_SILENCE_SOUND_DURATION);
            longSilenceSpecification.FormantSettingsList.Add(longSilenceSettings);
            speechSynthesizer.SpecificationList.Add(longSilenceSpecification);
            FormantSpecification shortSilenceSpecification = new FormantSpecification(speechSynthesizer.FundamentalFrequency, speechSynthesizer.SamplingFrequency);
            shortSilenceSpecification.Name = "-";
            FormantSettings shortSilenceSettings = new FormantSettings();
            shortSilenceSettings.SetSilence(SHORT_SILENCE_SOUND_DURATION);
            shortSilenceSpecification.FormantSettingsList.Add(shortSilenceSettings);
            speechSynthesizer.SpecificationList.Add(shortSilenceSpecification);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowSpecificationList()
        {
            synthesizerSpecificationListBox.Items.Clear();
            foreach (FormantSpecification specification in speechSynthesizer.SpecificationList)
            {
                synthesizerSpecificationListBox.Items.Add(specification.Name);
            }
    //       synthesizerSpecificationListBox.SelectedIndex = 0;
        }

        private void ShowSpeechSynthesizer()
        {
            fundamentalFrequencyTextBox.Text = speechSynthesizer.FundamentalFrequency.ToString();
            samplingFrequencyTextBox.Text = speechSynthesizer.SamplingFrequency.ToString();
            ShowSpecificationList();
            wordToSoundMappingEditor.SetSpeechSynthesizer(speechSynthesizer);
        }

        private void ShowFormantSpecification(int selectedIndex)
        {
            formantSpecificationListBox.Items.Clear();
            if (editorFormantSpecification != null)
            {
                for (int ii = 0; ii < editorFormantSpecification.FormantSettingsList.Count; ii++)
                {
                    formantSpecificationListBox.Items.Add("FormantSettings" + ii.ToString());
                }
                if (editorFormantSpecification.FormantSettingsList.Count > 0)
                {
                    formantSpecificationListBox.SelectedIndex = selectedIndex;
                }  
            }
        }

        private void formantSpecificationListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedFormantSettingsIndex = formantSpecificationListBox.SelectedIndex;
            if (selectedFormantSettingsIndex < editorFormantSpecification.FormantSettingsList.Count)
            {
                FormantSettings formantSettings = editorFormantSpecification.FormantSettingsList[selectedFormantSettingsIndex];
                formantSettingsEditor.SetFormantSettings(formantSettings);
            }
        }

        private void formantSpecificationListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int selectedIndex = formantSpecificationListBox.SelectedIndex;
            int selectedFormantSettingsIndex = formantSpecificationListBox.SelectedIndex;
            if (selectedFormantSettingsIndex < editorFormantSpecification.FormantSettingsList.Count)
            {
                FormantSettings formantSettings = editorFormantSpecification.FormantSettingsList[selectedFormantSettingsIndex];
                FormantSettings copiedFormantSettings = formantSettings.Copy();
                editorFormantSpecification.FormantSettingsList.Insert(selectedFormantSettingsIndex + 1, copiedFormantSettings);
                ShowFormantSpecification(selectedIndex + 1);
            }
        }

        private void formantSpecificationListBox_MouseDown(object sender, MouseEventArgs e)
        {
            int selectedFormantSettingsIndex = formantSpecificationListBox.SelectedIndex;
            if (selectedFormantSettingsIndex < editorFormantSpecification.FormantSettingsList.Count)
            {
                if (editorFormantSpecification.FormantSettingsList.Count > 1)
                {
                    if (MouseButtons == MouseButtons.Right)
                    {
                        if (MessageBox.Show("Remove formant settings?", "Formant settings removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            editorFormantSpecification.FormantSettingsList.RemoveAt(selectedFormantSettingsIndex);
                            int selectedIndex = selectedFormantSettingsIndex;
                            if (editorFormantSpecification.FormantSettingsList.Count <= selectedIndex) { selectedIndex = editorFormantSpecification.FormantSettingsList.Count - 1; }
                            ShowFormantSpecification(selectedIndex);
                        }
                    }
                }
            }
        }

        private void editorRandomizeButton_Click(object sender, EventArgs e)
        {
            if (editorFormantSpecification == null)
            {
                editorFormantSpecification = new FormantSpecification(speechSynthesizer.FundamentalFrequency, speechSynthesizer.SamplingFrequency);
            }
            if (editorFormantSpecification.FormantSettingsList.Count == 0)
            {
                FormantSettings formantSettings = new FormantSettings();
                editorFormantSpecification.FormantSettingsList.Add(formantSettings);
            }
            int selectedIndex = formantSpecificationListBox.SelectedIndex;
            if (formantSpecificationListBox.SelectedIndex < 0) { selectedIndex = 0; }
            editorFormantSpecification.FormantSettingsList[selectedIndex].Randomize(randomNumberGenerator);
            ShowFormantSpecification(selectedIndex);
            insertEndPointSilenceButton.Enabled = true;
            endPointSilenceDurationLabel.Enabled = true;
            endPointSilenceDurationTextBox.Enabled = true;
            editorPlayButton.Enabled = true;
            clearButton.Enabled = true;
            setUnvoicedButton.Enabled = true;
        }

        private void editorPlayButton_Click(object sender, EventArgs e)
        {
            saveSoundToolStripMenuItem.Enabled = true;
            editorFormantSpecification.FundamentalFrequency = speechSynthesizer.FundamentalFrequency;
            editorFormantSpecification.SamplingFrequency = speechSynthesizer.SamplingFrequency;
            editorFormantSpecification.GenerateSettingsSequence();
            speechSynthesizer.StorePitch = true;
            WAVSound sound = speechSynthesizer.GenerateSound(editorFormantSpecification);
            //  List<double> pitchList = speechSynthesizer.PitchList;
            //  augmentedSoundVisualizer.SetSound(sound, pitchList);

            int selectedIndex = formantSpecificationListBox.SelectedIndex;
            editorSpeechVisualizer.SetSound(sound);
            List<List<double>> timePitchPeriodList = speechSynthesizer.TimePitchPeriodList;
            editorSpeechVisualizer.SetTimePitchPeriodList(timePitchPeriodList);
            ShowFormantSpecification(selectedIndex);
            SoundPlayer soundPlayer = new SoundPlayer();
            sound.GenerateMemoryStream();
            sound.WAVMemoryStream.Position = 0; // Manually rewind stream 
            soundPlayer.Stream = null;
            soundPlayer.Stream = sound.WAVMemoryStream;
            soundPlayer.PlaySync();
        }

        private void fundamentalFrequencyTextBox_TextChanged(object sender, EventArgs e)
        {
            int fundamentalFrequency;
            Boolean ok = int.TryParse(fundamentalFrequencyTextBox.Text, out fundamentalFrequency);
            if (ok)
            {
                if ((fundamentalFrequency >= MINIMUM_FUNDAMENTAL_FREQUENCY) && (fundamentalFrequency <= MAXIMUM_FUNDAMENTAL_FREQUENCY))
                {
                    speechSynthesizer.FundamentalFrequency = fundamentalFrequency;
                }
            }
        }

        private void fundamentalFrequencyTextBox_Leave(object sender, EventArgs e)
        {
            int fundamentalFrequency;
            Boolean ok = int.TryParse(fundamentalFrequencyTextBox.Text, out fundamentalFrequency);
            if (ok)
            {
                if ((fundamentalFrequency < MINIMUM_FUNDAMENTAL_FREQUENCY) || (fundamentalFrequency > MAXIMUM_FUNDAMENTAL_FREQUENCY))
                {
                    fundamentalFrequencyTextBox.Text = speechSynthesizer.FundamentalFrequency.ToString();
                }
            }
            else
            {
                fundamentalFrequencyTextBox.Text = speechSynthesizer.FundamentalFrequency.ToString();
            }
        }

        private void samplingFrequencyTextBox_TextChanged(object sender, EventArgs e)
        {
            int samplingyFrequency; ;
            Boolean ok = int.TryParse(samplingFrequencyTextBox.Text, out samplingyFrequency);
            if (ok)
            {
                if ((samplingyFrequency >= MINIMUM_SAMPLING_FREQUENCY) && (samplingyFrequency <= MAXIMUM_SAMPLING_FREQUENCY))
                {
                    speechSynthesizer.SamplingFrequency = samplingyFrequency;
                }
            }
        }

        private void samplingFrequencyTextBox_Leave(object sender, EventArgs e)
        {
            int samplingyFrequency; ;
            Boolean ok = int.TryParse(samplingFrequencyTextBox.Text, out samplingyFrequency);
            if (ok)
            {
                if ((samplingyFrequency < MINIMUM_SAMPLING_FREQUENCY) || (samplingyFrequency > MAXIMUM_SAMPLING_FREQUENCY))
                {
                    samplingFrequencyTextBox.Text = speechSynthesizer.SamplingFrequency.ToString();
                }
            }
            else
            {
                samplingFrequencyTextBox.Text = speechSynthesizer.SamplingFrequency.ToString();
            }
        }

        private void insertEndPointSilenceButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = formantSpecificationListBox.SelectedIndex;
            FormantSettings initialSilenceSettings = new FormantSettings();
            double duration = double.Parse(endPointSilenceDurationTextBox.Text);
            initialSilenceSettings.SetSilence(duration);
            editorFormantSpecification.FormantSettingsList.Insert(0, initialSilenceSettings);
            FormantSettings finalSilenceSettings = new FormantSettings();
            finalSilenceSettings.SetSilence(duration);
            // Generate the transition to the final silence period:
            FormantSettings lastFormantSettings = editorFormantSpecification.FormantSettingsList.Last();
            double lastFormantSettingsDuration = lastFormantSettings.Duration;
            if (lastFormantSettingsDuration < duration) { lastFormantSettings.TransitionStart = 0; }
            else
            {
                double startFraction = 1 - (duration / lastFormantSettingsDuration);
                lastFormantSettings.TransitionStart = startFraction;
            }
            // Then add the final silence:
            editorFormantSpecification.FormantSettingsList.Add(finalSilenceSettings);
            ShowFormantSpecification(selectedIndex+1);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            editorFormantSpecification = null;
            ShowFormantSpecification(-1);
            formantSettingsEditor.Clear();
            editorSpeechVisualizer.SetSound(null);
            editorPlayButton.Enabled = false;
            endPointSilenceDurationLabel.Enabled = false;
            insertEndPointSilenceButton.Enabled = false;
            endPointSilenceDurationTextBox.Enabled = false;
        }

        private void ieaFormantSettingsListListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ieaSelectedFormantSpecification == null) { return; }
            int selectedIndex = ieaFormantSettingsListListBox.SelectedIndex;
            FormantSettings ieaSelectedFormantSettings = ieaSelectedFormantSpecification.FormantSettingsList[selectedIndex];
            ieaFormantSettingsEditor.SetFormantSettings(ieaSelectedFormantSettings);
        }

        private void EvaluateAll()
        {
            FormantSpeechSynthesizer speechSynthesizer = new FormantSpeechSynthesizer();
            for (int iX = 0; iX < formantSpecificationMatrix.Count; iX++)
            {
                for (int iY = 0; iY < formantSpecificationMatrix[0].Count; iY++)
                {
                    FormantSpecification formantSpecification = formantSpecificationMatrix[iX][iY].Copy();
                    formantSpecification.GenerateSettingsSequence();
                    WAVSound sound = speechSynthesizer.GenerateSound(formantSpecification);
                    soundVisualizer3x3.SetSound(sound, iX, iY);
                }
            }
        }

        private void ShowIEASelectedFormantSpecification()
        {
            ieaFormantSettingsListListBox.Items.Clear();
            for (int ii = 0; ii < ieaSelectedFormantSpecification.FormantSettingsList.Count; ii++)
            {
                ieaFormantSettingsListListBox.Items.Add("FormantSettings" + ii.ToString());
            }
            if (editorFormantSpecification.FormantSettingsList.Count > 0)
            {
                ieaFormantSettingsListListBox.SelectedIndex = 0;
            }
        }

        private void GenerateNextIteration(int xIndex, int yIndex)
        {
            double relativeModificationRange = double.Parse(relativeModificationRangeTextBox.Text);
            List<int> modifiableFormantSettingsIndexList = new List<int>();
            for (int ii = 0; ii < modificationScopeDropDownButton.DropDownItems.Count; ii++)
            {
                ToolStripMenuItem item = (ToolStripMenuItem)modificationScopeDropDownButton.DropDownItems[ii];
                if (item.Checked) { modifiableFormantSettingsIndexList.Add(ii); }
            }
            Boolean modifySinusoids = modifySinusoidsToolStripMenuItem.Checked;
            Boolean modifyVoicedFraction = modifyVoicedFractionToolStripMenuItem.Checked;
            Boolean modifyDuration = modifyDurationToolStripMenuItem.Checked;
            Boolean modifyTransitionStart = modifyTransitionStartToolStripMenuItem.Checked;
            Boolean modifyAmplitudeVariation = modifyAmplitudeVariationToolStripMenuItem.Checked;
            Boolean modifyPitchVariation = modifyPitchVariationToolStripMenuItem.Checked;
            FormantSpecification centerSpecification = formantSpecificationMatrix[xIndex][yIndex].Copy();
            formantSpecificationMatrix[1][1] = centerSpecification.Copy();
            for (int iX = 0; iX < formantSpecificationMatrix.Count; iX++)
            {
                for (int iY = 0; iY < formantSpecificationMatrix[0].Count; iY++)
                {
                    if (!((iX==1) && (iY==1)))
                  //  if ((iX != 1) || (iY != 1))
                    {
                        FormantSpecification modifiedSpecification = centerSpecification.Copy();
                        modifiedSpecification.Modify(randomNumberGenerator, modifiableFormantSettingsIndexList, relativeModificationRange,
                            modifySinusoids, modifyVoicedFraction, modifyDuration, modifyTransitionStart, modifyAmplitudeVariation, modifyPitchVariation);
                        formantSpecificationMatrix[iX][iY] = modifiedSpecification.Copy();
                    }
                }
            }
        }

        private void HandleSoundVisualizer3x3ItemDoubleClicked(object sender, VisualizerIndexEventArgs e)
        {
            ieaSelectedFormantSpecification = formantSpecificationMatrix[e.XIndex][e.YIndex].Copy();
            ShowIEASelectedFormantSpecification();
            GenerateNextIteration(e.XIndex, e.YIndex);
            EvaluateAll();
        }

        private void HandleSoundVisualizer3x3ItemClicked(object sender, VisualizerIndexEventArgs e)
        {
            ieaSelectedFormantSpecification = formantSpecificationMatrix[e.XIndex][e.YIndex].Copy();
            ShowIEASelectedFormantSpecification();  
        }

        private void startButton_Click(object sender, EventArgs e)
        {
         //   soundVisualizer3x3.BackColor = Color.Transparent;
            randomNumberGenerator = new Random();
            modificationScopeDropDownButton.DropDownItems.Clear();
            for (int ii = 0; ii < editorFormantSpecification.FormantSettingsList.Count; ii++)
            {
                ToolStripMenuItem button = new ToolStripMenuItem();
                button.Width = 120;
                button.DisplayStyle = ToolStripItemDisplayStyle.Text;
                button.Text = "Formant settings " + ii.ToString();
                button.CheckOnClick = true;
                button.CheckState = CheckState.Checked;
                modificationScopeDropDownButton.DropDownItems.Add(button);
            }
            soundVisualizer3x3.Initialize();
            soundVisualizer3x3.ItemClicked -= new EventHandler<VisualizerIndexEventArgs>(HandleSoundVisualizer3x3ItemClicked);  // For repeated runs
            soundVisualizer3x3.ItemClicked += new EventHandler<VisualizerIndexEventArgs>(HandleSoundVisualizer3x3ItemClicked);
            soundVisualizer3x3.ItemDoubleClicked -= new EventHandler<VisualizerIndexEventArgs>(HandleSoundVisualizer3x3ItemDoubleClicked);
            soundVisualizer3x3.ItemDoubleClicked += new EventHandler<VisualizerIndexEventArgs>(HandleSoundVisualizer3x3ItemDoubleClicked);  
            formantSpecificationMatrix = new List<List<FormantSpecification>>();
            formantSpecificationMatrix.Add(new List<FormantSpecification>() { editorFormantSpecification.Copy(), editorFormantSpecification.Copy(), editorFormantSpecification.Copy() });
            formantSpecificationMatrix.Add(new List<FormantSpecification>() { editorFormantSpecification.Copy(), editorFormantSpecification.Copy(), editorFormantSpecification.Copy() });
            formantSpecificationMatrix.Add(new List<FormantSpecification>() { editorFormantSpecification.Copy(), editorFormantSpecification.Copy(), editorFormantSpecification.Copy() }); 
            EvaluateAll();
            assignCurrentSoundButton.Enabled = true;
        }

        private void assignCurrentSoundButton_Click(object sender, EventArgs e)
        {
            editorFormantSpecification = formantSpecificationMatrix[1][1].Copy();
            ShowFormantSpecification(0);
            editorFormantSpecification.GenerateSettingsSequence();
            FormantSpeechSynthesizer speechSynthesizer = new FormantSpeechSynthesizer();
            WAVSound sound = speechSynthesizer.GenerateSound(editorFormantSpecification);
            editorSpeechVisualizer.SetSound(sound);
        //    soundNameTextBox.Text = "";
        //   soundEditorVisualizer.ClearHistory();
       //     soundEditorVisualizer.SetSound(sound);
        }
        
        private void saveSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editorSpeechVisualizer.Sound == null)
            {
                MessageBox.Show("No sound available in the sound editor!");
            }
            else
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "WAV files (*.wav)|*.wav";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        editorSpeechVisualizer.Sound.SaveToFile(saveFileDialog.FileName);
                    }
                }
            }
        }

        private void setUnvoicedButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = formantSpecificationListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                editorFormantSpecification.FormantSettingsList[selectedIndex].VoicedFraction = 0;
            }
        }

        private void addToSynthesizerButton_Click(object sender, EventArgs e)
        {
            FormantSpecification addedFormantSpecification = editorFormantSpecification.Copy();
            addedFormantSpecification.Name = soundNameTextBox.Text;

            // 20161025: This is needed to avoid incorrect transitions when the sound is later concatenated
            // with other sounds:
            addedFormantSpecification.FormantSettingsList.Last().TransitionStart = 1.0;

            speechSynthesizer.SpecificationList.Add(addedFormantSpecification);
            speechSynthesizer.SpecificationList.Sort((a, b) => a.Name.CompareTo(b.Name));
            ShowSpeechSynthesizer();
        }

        private void synthesizerSpecificationListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedSpecificationIndex = synthesizerSpecificationListBox.SelectedIndex;
            if (selectedSpecificationIndex >= 0)
            {
                editorFormantSpecification = speechSynthesizer.SpecificationList[selectedSpecificationIndex].Copy();
                ShowFormantSpecification(0);
                editorFormantSpecification.GenerateSettingsSequence();
                WAVSound sound = speechSynthesizer.GenerateSound(editorFormantSpecification);
                editorSpeechVisualizer.SetSound(sound);
                editorPlayButton.Enabled = true;
                SoundPlayer soundPlayer = new SoundPlayer();
                sound.GenerateMemoryStream();
                sound.WAVMemoryStream.Position = 0; // Manually rewind stream 
                soundPlayer.Stream = null;
                soundPlayer.Stream = sound.WAVMemoryStream;
                soundPlayer.PlaySync();
            }
        }

        private void synthesizerSpecificationListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int selectedIndex = synthesizerSpecificationListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                int selectedWordIndex = wordToSoundMappingEditor.SelectedRowIndex;
                if ((selectedWordIndex >= 0) && (speechSynthesizer.WordToSoundMappingList.Count > 0))
                {
                    string soundName = synthesizerSpecificationListBox.SelectedItem.ToString();
                    speechSynthesizer.WordToSoundMappingList[selectedWordIndex].SoundNameList.Add(soundName);
                    wordToSoundMappingEditor.SetSpeechSynthesizer(speechSynthesizer);
                }
            }
        }

        private void synthesizerSpecificationListBox_MouseDown(object sender, MouseEventArgs e)
        {
            int selectedIndex = synthesizerSpecificationListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (MessageBox.Show("Are you sure that you want to remove the sound?", "Remove sound", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        speechSynthesizer.SpecificationList.RemoveAt(selectedIndex);
                        ShowSpecificationList();
                    }
                }
            }
        }

        private void speakSentenceButton_Click(object sender, EventArgs e)
        {
            speechSynthesizer.Volume = double.Parse(volumeTextBox.Text);
            string wordString = sentenceToSpeakTextBox.Text;
            if (wordString != "")
            {
                List<string> wordList = wordString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                List<double> silenceList = new List<double>() { };
                for (int ii = 0; ii < (wordList.Count - 1); ii++)
                {
                    silenceList.Add(0.05);
                }
                WAVSound sentenceSound = speechSynthesizer.GenerateWordSequence(wordList, silenceList);
                SoundPlayer soundPlayer = new SoundPlayer();
                sentenceSound.GenerateMemoryStream();
                sentenceSound.WAVMemoryStream.Position = 0; // Manually rewind stream 
                soundPlayer.Stream = null;
                soundPlayer.Stream = sentenceSound.WAVMemoryStream;
                soundPlayer.PlaySync();
                sentenceSpeechVisualizer.SetSound(sentenceSound);
            }
        }

        private void newSpeechSynthesizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateNewSpeechSynthesizer();
            ShowSpeechSynthesizer();
            saveSpeechSynthesizerToolStripMenuItem.Enabled = true;
            speakSentenceButton.Enabled = true;
        }

        private void loadSpeechSynthesizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML files (*.xml)|*xml";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    speechSynthesizer = (FormantSpeechSynthesizer)ObjectXmlSerializer.ObtainSerializedObject(openFileDialog.FileName, typeof(FormantSpeechSynthesizer));

                    foreach (FormantSpecification fs in speechSynthesizer.SpecificationList)
                    {
                        fs.FormantSettingsList.Last().TransitionStart = 1;
                    }

                    ShowSpeechSynthesizer();
                    saveSpeechSynthesizerToolStripMenuItem.Enabled = true;
                    speakSentenceButton.Enabled = true;
                }
            }
        }

        private void saveSpeechSynthesizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "XML files (*.xml)|*xml";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ObjectXmlSerializer.SerializeObject(saveFileDialog.FileName, speechSynthesizer);
                }
            }
        }

        private void wordToSoundMappingEditor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (speechSynthesizer != null)
            {
                int selectedIndex = e.RowIndex;
                if ((selectedIndex >= 0) && (selectedIndex < speechSynthesizer.WordToSoundMappingList.Count))
                {
                    WordToSoundMapping wordToSoundMapping = speechSynthesizer.WordToSoundMappingList[selectedIndex];
                    if (editorFormantSpecification == null)
                    {
                        editorFormantSpecification = new FormantSpecification(speechSynthesizer.FundamentalFrequency, speechSynthesizer.SamplingFrequency);
                        editorPlayButton.Enabled = true;
                    }
                    editorFormantSpecification.FormantSettingsList = new List<FormantSettings>();
                    foreach (string soundName in wordToSoundMapping.SoundNameList)
                    {
                        FormantSpecification specification = speechSynthesizer.SpecificationList.Find(s => s.Name == soundName).Copy();
                        foreach (FormantSettings settings in specification.FormantSettingsList)
                        {
                            editorFormantSpecification.FormantSettingsList.Add(settings.Copy());
                        }
                    }
                }
            }
        }
    }
}
