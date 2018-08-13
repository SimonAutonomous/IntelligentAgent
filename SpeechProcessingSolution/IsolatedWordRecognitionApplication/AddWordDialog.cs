using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AudioLibrary;
using SpeechRecognitionLibrary;

namespace IsolatedWordRecognitionApplication
{
    public partial class AddWordDialog : Form
    {
        private IsolatedWordRecognizer recognizer = null;
        private string[] files;

        public AddWordDialog()
        {
            InitializeComponent();
        }

        public void SetRecognizer(IsolatedWordRecognizer recognizer)
        {
            this.recognizer = recognizer;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = Path.GetDirectoryName(Application.ExecutablePath);
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                soundListListView.Items.Clear();
                files = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                foreach (string file in files)
                {
                    if (file.ToLower().EndsWith(".wav"))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        soundListListView.Items.Add(fileName);
                    }
                }
            }
        }


        private void soundListListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            addButton.Enabled = false;
            if ((soundListListView.CheckedIndices.Count > 0) && (soundNameTextBox.Text != ""))
            {
                addButton.Enabled = true;
            }
        }

        private void soundNameTextBox_TextChanged(object sender, EventArgs e)
        {
            addButton.Enabled = false;
            if ((soundListListView.CheckedIndices.Count > 0) && (soundNameTextBox.Text != ""))
            {
                addButton.Enabled = true;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string soundName = soundNameTextBox.Text;
            List<WAVSound> soundList = new List<WAVSound>();
            List<string> selectedFiles = new List<string>();
            foreach (int checkedIndex in soundListListView.CheckedIndices)
            {
                selectedFiles.Add(files[checkedIndex]);
            }
            foreach (string file in selectedFiles)
            {
                WAVSound sound = new WAVSound();
                sound.LoadFromFile(file);
                soundList.Add(sound);
            }
            if (recognizer.ContainsSound(soundName))
            {
                if (MessageBox.Show("Overwrite existing instance?", "Sound available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    recognizer.RemoveSound(soundName);
                    recognizer.AppendSound(soundName, soundList);
                }
            }
            else 
            { 
                recognizer.AppendSound(soundName, soundList);
            }
        }
    }
}
