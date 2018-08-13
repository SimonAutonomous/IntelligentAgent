using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AudioLibrary;
using AudioLibrary.SoundFeatures;
using ObjectSerializerLibrary;
using PlotLibrary;
using SpeechRecognitionLibrary;

namespace IsolatedWordRecognitionApplication
{
    public partial class IWRMainForm : Form
    {
        private IsolatedWordRecognizer recognizer = null;
        private WAVSound testSound = null;
        private IWRRecognitionResult recognitionResult; // The result of the most recent call to IsolatedWordRecognizer.RecognizeSingle()
        private WAVRecorder wavRecorder = null;
        private Thread recodingThread = null;

        public IWRMainForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void ShowParameters()
        {
            soundExtractionMovingAverageLengthTextBox.Text = recognizer.SoundExtractionMovingAverageLength.ToString();
            soundExtractionThresholdTextBox.Text = recognizer.SoundExtractionThreshold.ToString();
            preEmphasisThresholdFrequencyTextBox.Text = recognizer.PreEmphasisThresholdFrequency.ToString();
            frameDurationTextBox.Text = recognizer.FrameDuration.ToString("0.0000");
            frameShiftTextBox.Text = recognizer.FrameShift.ToString("0.0000");
            alphaTextBox.Text = recognizer.Alpha.ToString("0.000");
            autoCorrelationOrderTextBox.Text = recognizer.AutoCorrelationOrder.ToString();
            lpcOrderTextBox.Text = recognizer.LPCOrder.ToString();
            cepstralOrderTextBox.Text = recognizer.CepstralOrder.ToString();
            numberOfValuesPerFeatureTextBox.Text = recognizer.NumberOfValuesPerFeature.ToString();
            recognitionThresholdTextBox.Text = recognizer.RecognitionThreshold.ToString("0.0000");
        }

        private void SetParameters()
        {
            recognizer.SoundExtractionMovingAverageLength = int.Parse(soundExtractionMovingAverageLengthTextBox.Text);
            recognizer.SoundExtractionThreshold = double.Parse(soundExtractionThresholdTextBox.Text);
            recognizer.PreEmphasisThresholdFrequency = double.Parse(preEmphasisThresholdFrequencyTextBox.Text);
            recognizer.FrameDuration = double.Parse(frameDurationTextBox.Text);
            recognizer.FrameShift = double.Parse(frameShiftTextBox.Text);
            recognizer.Alpha = double.Parse(alphaTextBox.Text);
            recognizer.AutoCorrelationOrder = int.Parse(autoCorrelationOrderTextBox.Text);
            recognizer.LPCOrder = int.Parse(lpcOrderTextBox.Text);
            recognizer.CepstralOrder = int.Parse(cepstralOrderTextBox.Text);
            recognizer.NumberOfValuesPerFeature = int.Parse(numberOfValuesPerFeatureTextBox.Text);
        }

        private void ToggleParametersEditable(Boolean editable)
        {
            soundExtractionMovingAverageLengthTextBox.Enabled = editable;
            soundExtractionThresholdTextBox.Enabled = editable;
            preEmphasisThresholdFrequencyTextBox.Enabled = editable;
            frameDurationTextBox.Enabled = editable;
            frameShiftTextBox.Enabled = editable;
            alphaTextBox.Enabled = editable;
            autoCorrelationOrderTextBox.Enabled = editable;
            lpcOrderTextBox.Enabled = editable;
            cepstralOrderTextBox.Enabled = editable;
            numberOfValuesPerFeatureTextBox.Enabled = editable;
        }

        private void Initialize()
        {
            recognizer = new IsolatedWordRecognizer();
            ShowParameters();
            recognizer = null;
            featurePlotPanel.SetFrameSize(50, 20, 20, 20);
            featurePlotPanel.VerticalAxisVisible = true;
            featurePlotPanel.AxisColor = Color.Black;
            featurePlotPanel.HorizontalAxisVisible = true;
            featurePlotPanel.MajorHorizontalTickMarksVisible = true;
            featurePlotPanel.MajorHorizontalTickMarkSpacing = 0.1;
            featurePlotPanel.HorizontalAxisMarkingsVisible = true;
            featurePlotPanel.HorizontalAxisMarkingsFormatString = "0.00";
            featurePlotPanel.MajorVerticalTickMarksVisible = true;
            featurePlotPanel.MajorVerticalTickMarkSpacing = 0.5;
            featurePlotPanel.VerticalAxisMarkingsFormatString = "0.00";
            featurePlotPanel.GridVisible = true;
            featurePlotPanel.GridColor = Color.Silver;
            featurePlotPanel.HorizontalAxisLabel = "time";
            featurePlotPanel.SetHorizontalPlotRange(0.0, 1.0);
            featurePlotPanel.SetVerticalPlotRange(-2.001, 2);
            featurePlotPanel.SetHorizontalAxisPosition(0);
            featurePlotPanel.SetVerticalAxisPosition(0);

            featureComparisonPlotPanel.SetFrameSize(50, 20, 20, 20);
            featureComparisonPlotPanel.VerticalAxisVisible = true;
            featureComparisonPlotPanel.AxisColor = Color.Black;
            featureComparisonPlotPanel.HorizontalAxisVisible = true;
            featureComparisonPlotPanel.MajorHorizontalTickMarksVisible = true;
            featureComparisonPlotPanel.MajorHorizontalTickMarkSpacing = 0.1;
            featureComparisonPlotPanel.HorizontalAxisMarkingsVisible = true;
            featureComparisonPlotPanel.HorizontalAxisMarkingsFormatString = "0.00";
            featureComparisonPlotPanel.MajorVerticalTickMarksVisible = true;
            featureComparisonPlotPanel.MajorVerticalTickMarkSpacing = 0.5;
            featureComparisonPlotPanel.VerticalAxisMarkingsFormatString = "0.00";
            featureComparisonPlotPanel.GridVisible = true;
            featureComparisonPlotPanel.GridColor = Color.Silver;
            featureComparisonPlotPanel.HorizontalAxisLabel = "time";
            featureComparisonPlotPanel.SetHorizontalPlotRange(0.0, 1.0);
            featureComparisonPlotPanel.SetVerticalPlotRange(-2.001, 2);
            featureComparisonPlotPanel.SetHorizontalAxisPosition(0);
            featureComparisonPlotPanel.SetVerticalAxisPosition(0);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            recognizer = new IsolatedWordRecognizer();
            recognizer.AvailableSoundsChanged += new EventHandler(HandleAvailableSoundsChanged);
            recognizer.Name = "IWR1";
            SetParameters(); 
            ToggleParametersEditable(false);
            featurePlotPanel.Clear();
            HandleAvailableSoundsChanged(this, EventArgs.Empty); // A bit ugly, but OK.
            speechRecognizerEditingToolStrip.Visible = true;
            saveToolStripMenuItem.Enabled = true;
        }

        private void parametersVisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speechRecognizerToolStrip1.Visible = parametersVisibleToolStripMenuItem.Checked;
            speechRecognizerToolStrip2.Visible = parametersVisibleToolStripMenuItem.Checked;
            speechRecognizerToolStrip3.Visible = parametersVisibleToolStripMenuItem.Checked;
        }

        private void HandleAvailableSoundsChanged(object sender, EventArgs e)
        {
            availableWordsListBox.Items.Clear();
            List<string> availableSoundsList = recognizer.GetAvailableSounds();
            foreach (string availableSound in availableSoundsList) { availableWordsListBox.Items.Add(availableSound); }
            if (availableSoundsList.Count > 0)
            {
                featureLabel.Visible = true;
                featureComboBox.Visible = true;
                weightLabel.Visible = true;
                weightTextBox.Visible = true;
                int previouslySelectedIndex = -1;
                if (featureComboBox.Items.Count > 0)
                {
                    previouslySelectedIndex = featureComboBox.SelectedIndex;
                    featureComboBox.SelectedIndex = previouslySelectedIndex; // To update, so that the features of the currently
                    // selected sound are shown
                }
                else
                {
                    featureComboBox.Items.Clear(); // Not really needed...
                    for (int ii = 0; ii < recognizer.AverageSoundFeatureSetList[0].FeatureList.Count; ii++)
                    {
                        featureComboBox.Items.Add(recognizer.AverageSoundFeatureSetList[0].FeatureList[ii].Name);
                    }
                    if (featureComboBox.Items.Count > 0)
                    {
                        featureComboBox.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                featureLabel.Visible = false;
                featureComboBox.Visible = false;
                weightLabel.Visible = false;
                weightTextBox.Visible = false;
            }
        }

        private void AppendFeatureSeries(int selectedSoundIndex, int selectedFeatureIndex)
        {
            SoundFeature averageSoundFeature = recognizer.AverageSoundFeatureSetList[selectedSoundIndex].FeatureList[selectedFeatureIndex];
            string featureName = averageSoundFeature.Name;
            DataSeries dataSeries = new DataSeries();
            dataSeries.Generate(featureName, averageSoundFeature.TimeList, averageSoundFeature.ValueList);
            dataSeries.SetPointVisibilityState(true);
            dataSeries.SetPointConnectionState(true);
            dataSeries.SetLineColor(Color.Black);
            dataSeries.SetPointColor(Color.Red);
            List<double> verticalErrorBarList = new List<double>();
            for (int ii = 0; ii < averageSoundFeature.VarianceList.Count; ii++)
            {
                double standardDeviation = Math.Sqrt(averageSoundFeature.VarianceList[ii]);
                verticalErrorBarList.Add(standardDeviation);
            }
            dataSeries.AddSymmetricVerticalErrorBars(verticalErrorBarList);
            dataSeries.SetVerticalErrorBarSerifVisibilityState(true);
            dataSeries.SetErrorBarRelativeSerifLength(0.002);
            featurePlotPanel.AddDataSeries(dataSeries);
        }

        private void PlotFeatures()
        {
            featurePlotPanel.Clear();
            if (availableWordsListBox.SelectedIndices.Count > 0) { autorangeToolStripMenuItem.Enabled = true; }
            else { autorangeToolStripMenuItem.Enabled = false; }
            foreach (int selectedSoundIndex in availableWordsListBox.SelectedIndices)
            {
                if (selectedSoundIndex >= 0)
                {
                    int selectedFeatureIndex = featureComboBox.SelectedIndex;
                    featurePlotPanel.VerticalAxisLabel = recognizer.AverageSoundFeatureSetList[0].FeatureList[selectedFeatureIndex].Name;
                    if (selectedFeatureIndex >= 0)
                    {
                        AppendFeatureSeries(selectedSoundIndex, selectedFeatureIndex);
                    }
                }
            }
            featurePlotPanel.SetVerticalErrorBarVisibilityState(errorBarsVisibleToolStripMenuItem.Checked);
            featurePlotPanel.Refresh();
            yMinTextBox.Text = featurePlotPanel.YMin.ToString("0.000");
            yMaxTextBox.Text = featurePlotPanel.YMax.ToString("0.000");
            if (featureComboBox.SelectedIndex >= 0)
            {
                weightTextBox.Text = recognizer.WeightList[featureComboBox.SelectedIndex].ToString("0.0000");
            }
        }

        private void availableWordsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlotFeatures();
        }

        private void featuresComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlotFeatures();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ObjectXmlSerializer.SerializeObject(saveFileDialog.FileName, recognizer);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                recognizer = (IsolatedWordRecognizer)ObjectXmlSerializer.ObtainSerializedObject(openFileDialog.FileName, typeof(IsolatedWordRecognizer));
                recognizer.AvailableSoundsChanged += new EventHandler(HandleAvailableSoundsChanged);
                ShowParameters();
                ToggleParametersEditable(false);
                speechRecognizerEditingToolStrip.Visible = true;
                saveToolStripMenuItem.Enabled = true;
                featurePlotPanel.Clear();
                HandleAvailableSoundsChanged(this, EventArgs.Empty); // A bit ugly, but OK...
            }
        }

        private void addWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddWordDialog addWordDialog = new AddWordDialog();
            addWordDialog.SetRecognizer(recognizer);
            addWordDialog.Show();
        }

        private void autorangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (autorangeToolStripMenuItem.Checked)
            {
                featurePlotPanel.VerticalAutoRange = true;
                featurePlotPanel.Refresh(); // Needed to update yMin and yMax
                yMinTextBox.Text = featurePlotPanel.YMin.ToString("0.000");
                yMaxTextBox.Text = featurePlotPanel.YMax.ToString("0.000");
                yMinTextBox.Enabled = false;
                yMaxTextBox.Enabled = false;
            }
            else
            {
                featurePlotPanel.VerticalAutoRange = false;
                featurePlotPanel.SetHorizontalAxisPosition(0);
                yMinTextBox.Enabled = true;
                yMaxTextBox.Enabled = true;
            }
        }

        private void yMinTextBox_TextChanged(object sender, EventArgs e)
        {
            if (yMaxTextBox.Text == "") { return; } // Happens before initialization...
            double yMax = double.Parse(yMaxTextBox.Text);
            double yMin;
            Boolean ok = double.TryParse(yMinTextBox.Text, out yMin);
            if (ok)
            {
                if (yMin < yMax) 
                { 
                    featurePlotPanel.SetVerticalPlotRange(yMin, yMax);
                    featurePlotPanel.SetHorizontalAxisPosition(0);
                    featurePlotPanel.Refresh();
                }
            }
        }

        private void yMaxTextBox_TextChanged(object sender, EventArgs e)
        {
            if (yMinTextBox.Text == "") { return; } // Happens before initialization...
            double yMin = double.Parse(yMinTextBox.Text);
            double yMax;
            Boolean ok = double.TryParse(yMaxTextBox.Text, out yMax);
            if (ok)
            {
                if (yMax > yMin)
                {
                    featurePlotPanel.SetVerticalPlotRange(yMin, yMax);
                    featurePlotPanel.SetHorizontalAxisPosition(0);
                    featurePlotPanel.Refresh();
                }
            }
        }

        private void errorBarsVisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            featurePlotPanel.SetVerticalErrorBarVisibilityState(errorBarsVisibleToolStripMenuItem.Checked);
            featurePlotPanel.Refresh(); // Needed to redraw the plots in order to show (or hide) the error bars.
        }

        private void weightTextBox_TextChanged(object sender, EventArgs e)
        {
            double newWeight;
            Boolean ok = double.TryParse(weightTextBox.Text, out newWeight);
            if (ok)
            {
                int selectedFeatureIndex = featureComboBox.SelectedIndex;
                if (selectedFeatureIndex >= 0)
                {
                    recognizer.WeightList[selectedFeatureIndex] = newWeight;
                }
            }
        }

        private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "WAV files (*.wav)|*.wav";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                testSound = new WAVSound();
                testSound.LoadFromFile(openFileDialog.FileName);
                soundVisualizer.SetSound(testSound);
                playSoundButton.Enabled = true;
                saveSoundToolStripMenuItem.Enabled = true;
            }
        }

        private void recognizeButton_Click(object sender, EventArgs e)
        {
            WAVSound copiedTestSound = soundVisualizer.Sound.Copy(); 
            if (setMaximumNonclippingVolumeToolStripMenuItem.Checked)
            {
                copiedTestSound.SetMaximumNonClippingVolume();
                soundVisualizer.SetSound(copiedTestSound);
            }
            recognitionResult = recognizer.RecognizeSingle(copiedTestSound);
            // 20170114
            if (recognitionResult == null)
            {
                recognitionResultTextBox.Text = "No sound data!";
                return;
            }
            // end 20170114
            deviationListBox.Items.Clear();
            for (int ii = 0; ii < recognitionResult.DeviationList.Count; ii++)
            {
                deviationListBox.Items.Add(recognitionResult.DeviationList[ii].Item1.PadRight(16) + " " +
                    recognitionResult.DeviationList[ii].Item2.ToString("0.0000"));
            }
            if (recognitionResult.DeviationList.Count > 0)
            {
                if (recognitionResult.DeviationList[0].Item2 < recognizer.RecognitionThreshold)
                {
                    recognitionResultTextBox.Text = recognitionResult.DeviationList[0].Item1;
                }
                else
                {
                    recognitionResultTextBox.Text = "UNKNOWN";
                }
                featureComparisonComboBox.Items.Clear();
                if (recognizer.AverageSoundFeatureSetList.Count > 0)
                {
                    for (int ii = 0; ii < recognizer.AverageSoundFeatureSetList[0].FeatureList.Count; ii++)
                    {
                        featureComparisonComboBox.Items.Add(recognizer.AverageSoundFeatureSetList[0].FeatureList[ii].Name);
                    }
                    featureComparisonComboBox.SelectedIndex = 0;
                }
            }
        }

        private void recognitionThresholdTextBox_TextChanged(object sender, EventArgs e)
        {
            double newRecognitionThreshold;
            Boolean ok = double.TryParse(recognitionThresholdTextBox.Text, out newRecognitionThreshold);
            if (ok)
            {
                if (newRecognitionThreshold > 0)
                {
                    recognizer.RecognitionThreshold = newRecognitionThreshold;
                }
            }
        }

        private void ShowTestSoundFeature(SoundFeature soundFeature)
        {
            featureComparisonPlotPanel.Clear();
            featureComparisonPlotPanel.VerticalAutoRange = true;
            DataSeries dataSeries = new DataSeries();
            dataSeries.Generate(soundFeature.Name, soundFeature.TimeList, soundFeature.ValueList);
            dataSeries.SetPointVisibilityState(true);
            dataSeries.SetPointConnectionState(true);
            dataSeries.SetLineColor(Color.Cyan);
            dataSeries.SetPointColor(Color.Blue);
            featureComparisonPlotPanel.AddDataSeries(dataSeries);
        }

        private void ShowComparisonSoundFeature(SoundFeature comparisonSoundFeature)
        {
            DataSeries dataSeries = new DataSeries();
            dataSeries.Generate(comparisonSoundFeature.Name, comparisonSoundFeature.TimeList, comparisonSoundFeature.ValueList);
            dataSeries.SetPointVisibilityState(true);
            dataSeries.SetPointConnectionState(true);
            dataSeries.SetLineColor(Color.Black);
            dataSeries.SetPointColor(Color.Red);
            List<double> verticalErrorBarList = new List<double>();
            for (int ii = 0; ii < comparisonSoundFeature.VarianceList.Count; ii++)
            {
                double standardDeviation = Math.Sqrt(comparisonSoundFeature.VarianceList[ii]);
                verticalErrorBarList.Add(standardDeviation);
            }
            dataSeries.AddSymmetricVerticalErrorBars(verticalErrorBarList);
            dataSeries.SetVerticalErrorBarSerifVisibilityState(true);
            dataSeries.SetErrorBarRelativeSerifLength(0.002);
            dataSeries.SetVerticalErrorBarsVisibilityState(true);
            featureComparisonPlotPanel.AddDataSeries(dataSeries);
        }

        private void ShowTestSoundAndComparisonFeatures()
        {
            featureComparisonPlotPanel.Clear();
            int selectedFeatureIndex = featureComparisonComboBox.SelectedIndex;
            // Plot the feature values from the test sound:
            SoundFeature soundFeature = recognitionResult.SoundFeatureSet.FeatureList[selectedFeatureIndex];
            ShowTestSoundFeature(soundFeature);

            // A bit ugly, but OK...
            if (deviationListBox.SelectedIndex >= 0)
            {
                string selectedComparisonSound =
                    deviationListBox.Items[deviationListBox.SelectedIndex].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];

                SoundFeatureSet comparisonSoundFeatureSet = recognizer.AverageSoundFeatureSetList.Find(s => s.Information == selectedComparisonSound);
                if (comparisonSoundFeatureSet != null)  // Should always be the case...
                {
                    SoundFeature comparisonSoundFeature = comparisonSoundFeatureSet.FeatureList[selectedFeatureIndex];
                    ShowComparisonSoundFeature(comparisonSoundFeature);
                }
            }
        }

        private void featureComparisonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTestSoundAndComparisonFeatures();
        }

        private void deviationListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTestSoundAndComparisonFeatures();
        }

        private void playSoundButton_Click(object sender, EventArgs e)
        {
            playSoundButton.Enabled = false;
            WAVSound sound = soundVisualizer.Sound.Copy(); // recorderVisualizer.Sound.Copy();
            SoundPlayer soundPlayer = new SoundPlayer();
            sound.GenerateMemoryStream();
            sound.WAVMemoryStream.Position = 0; // Manually rewind stream 
            soundPlayer.Stream = null;
            soundPlayer.Stream = sound.WAVMemoryStream;
            soundPlayer.PlaySync();
            playSoundButton.Enabled = true;
        }

        private void recordToolStripButton_Click(object sender, EventArgs e)
        {
            if (recordToolStripButton.Text.Contains("Start"))  // A bit ugly, but OK
            {
                recordToolStripButton.Text = "Stop recording";
                wavRecorder = new WAVRecorder();
                wavRecorder.DeviceId = 0;
                wavRecorder.Start();
            }
            else
            {
                wavRecorder.Stop();
                byte[] recordedBytes = wavRecorder.GetAllRecordedBytes();
                if (recordedBytes != null)
                {
                    if (recordedBytes.Length > 0)
                    {
                        WAVSound sound = new WAVSound("", wavRecorder.SampleRate, wavRecorder.NumberOfChannels, wavRecorder.NumberOfBitsPerSample);
                        sound.AppendSamplesAsBytes(recordedBytes);
                        soundVisualizer.SetSound(sound);
                    }
                }
                recordToolStripButton.Text = "Start recording";
                playSoundButton.Enabled = true;
                saveSoundToolStripMenuItem.Enabled = true;
            }
        }

        private void saveSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "WAV files (*.wav)|*.wav";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                WAVSound savedSound = soundVisualizer.Sound.Copy(); //  recorderVisualizer.Sound.Copy();
                savedSound.SaveToFile(saveFileDialog.FileName);
            }
        }
    }
}
