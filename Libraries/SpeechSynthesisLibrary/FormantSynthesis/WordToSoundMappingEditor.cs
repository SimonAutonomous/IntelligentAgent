using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using AudioLibrary;
using AudioLibrary.Visualization;

namespace SpeechSynthesisLibrary.FormantSynthesis
{
    public partial class WordToSoundMappingEditor : DataGridView
    {
        private const int SCROLLBAR_WIDTH = 20;
        private FormantSpeechSynthesizer speechSynthesizer;
        private int selectedRowIndex;

        public event EventHandler<WAVSoundEventArgs> SoundGenerated = null;

        public WordToSoundMappingEditor()
        {
            InitializeComponent();
        }

        private void GenerateColumns()
        {
            this.Rows.Clear();
            this.MultiSelect = false;
            this.ClearSelection();
            this.Columns.Clear();
            this.RowHeadersVisible = false;
            this.ColumnHeadersVisible = true;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToResizeRows = false;
            this.Columns.Add("wordColumn", "Word");
            this.Columns[0].ReadOnly = false;
            this.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Columns.Add("mappingColumn", "Mapping");
            this.Columns[1].ReadOnly = true;
            this.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            SetColumnWidths();
        }

        private void SetColumnWidths()
        {
            this.Columns[1].Width = this.Width - SCROLLBAR_WIDTH - this.Columns[0].Width;
        }

        private void OnSoundGenerated(WAVSound sound)
        {
            if (SoundGenerated != null)
            {
                EventHandler<WAVSoundEventArgs> handler = SoundGenerated;
                WAVSoundEventArgs e = new WAVSoundEventArgs(sound);
                handler(this, e);
            }
        }

        private void ShowWordToSoundMappingList()
        {
            GenerateColumns();
            foreach (WordToSoundMapping wordToSoundMapping in speechSynthesizer.WordToSoundMappingList)
            {
                string mappingString = "";
                foreach (string soundName in wordToSoundMapping.SoundNameList) { mappingString += soundName + " "; }
                mappingString = mappingString.TrimEnd(new char[] { ' ' });
                this.Rows.Add(new string[] { wordToSoundMapping.Word, mappingString });
            }
        }

        public void SetSpeechSynthesizer(FormantSpeechSynthesizer speechSynthesizer)
        {
            this.speechSynthesizer = speechSynthesizer;
            ShowWordToSoundMappingList();
        }


        protected override void OnRowEnter(DataGridViewCellEventArgs e)
        {
            base.OnRowEnter(e);
            if (e.RowIndex >= speechSynthesizer.WordToSoundMappingList.Count)
            {
                speechSynthesizer.WordToSoundMappingList.Add(new WordToSoundMapping());
            }
        }

        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            base.OnCellEndEdit(e);
            if (e.ColumnIndex == 0)
            {
                WordToSoundMapping wordToSoundMapping = speechSynthesizer.WordToSoundMappingList[e.RowIndex];
                if (this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    wordToSoundMapping.Word = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
        }

        protected override void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseClick(e);
            selectedRowIndex = e.RowIndex;
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex == 0)
                {
                    speechSynthesizer.WordToSoundMappingList.RemoveAt(selectedRowIndex);
                    ShowWordToSoundMappingList();
                }
                if (e.ColumnIndex == 1)
                {
                    WordToSoundMapping wordToSoundMapping = speechSynthesizer.WordToSoundMappingList[selectedRowIndex];
                    if (wordToSoundMapping.SoundNameList.Count > 0)
                    {
                        wordToSoundMapping.SoundNameList.RemoveAt(wordToSoundMapping.SoundNameList.Count - 1);
                        string mappingString = "";
                        foreach (string soundName in wordToSoundMapping.SoundNameList) { mappingString += soundName + " "; }
                        mappingString = mappingString.TrimEnd(new char[] { ' ' });
                        Rows[selectedRowIndex].Cells[e.ColumnIndex].Value = mappingString;
                    }
                }
            }
        }

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            base.OnCellDoubleClick(e);
            selectedRowIndex = e.RowIndex;
            if (e.ColumnIndex == 0)
            {

                if (selectedRowIndex < speechSynthesizer.WordToSoundMappingList.Count)
                {
                    string word = speechSynthesizer.WordToSoundMappingList[e.RowIndex].Word;
                    WAVSound wordSound = speechSynthesizer.GenerateWord(word);
                    if (wordSound != null)
                    {
                        OnSoundGenerated(wordSound);
                    }
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.Columns.Count > 0)
            {
                SetColumnWidths();
            }
        }

        public int SelectedRowIndex
        {
            get { return selectedRowIndex; }
        }
    }
}
