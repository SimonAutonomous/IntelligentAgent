using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SpeechSynthesisLibrary.FormantSynthesis
{
    public partial class FormantSettingsEditor : DataGridView
    {
        private const int SCROLLBAR_WIDTH = 21;

        private FormantSettings formantSettings;

        public FormantSettingsEditor()
        {
            InitializeComponent();
        }

        private void GenerateColumns()
        {
            this.Rows.Clear();
            this.Columns.Clear();
            this.Columns.Add("parameterColumn", "Parameter");
            this.Columns[0].ReadOnly = true;
            this.Columns.Add("valueColumn", "Value");
            this.ColumnHeadersVisible = true;
            this.RowHeadersVisible = false;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToResizeColumns = true;
            this.AllowUserToResizeRows = false;
            SetColumnWidths();
        }

        private void SetColumnWidths()
        {
            int scrollBarWidth = SCROLLBAR_WIDTH;
            if (this.Columns.Count == 2)
            {
                this.Columns[0].Width = (this.Width - scrollBarWidth) / 2;
                this.Columns[1].Width = this.Width - this.Columns[0].Width - scrollBarWidth;
            }
        }

        public void Clear()
        {
            this.formantSettings = null;
            GenerateColumns();
        }

        public void SetFormantSettings(FormantSettings formantSettings)
        {
            this.formantSettings = formantSettings;
            GenerateColumns();
            for (int iSinusoid = 0; iSinusoid < formantSettings.FrequencyList.Count; iSinusoid++)
            {
                this.Rows.Add(new string[] { "Frequency" + (iSinusoid + 1).ToString(), formantSettings.FrequencyList[iSinusoid].ToString("0") });
                this.Rows.Add(new string[] { "Amplitude" + (iSinusoid + 1).ToString(), formantSettings.AmplitudeList[iSinusoid].ToString("0.000") });
                this.Rows.Add(new string[] { "Bandwidth" + (iSinusoid + 1).ToString(), formantSettings.BandwidthList[iSinusoid].ToString("0") });
            }
            this.Rows.Add(new string[] { "Duration", formantSettings.Duration.ToString("0.000") });
            this.Rows.Add(new string[] { "Voiced fraction", formantSettings.VoicedFraction.ToString("0.000") });
            this.Rows.Add(new string[] { "Transition start", formantSettings.TransitionStart.ToString("0.000") });
            this.Rows.Add(new string[] { "Relative amplitude variation (extremum time)", formantSettings.RelativeAmplitudeVariationParameterList[0].ToString("0.000") });
            this.Rows.Add(new string[] { "Relative amplitude variation (start amplitude)", formantSettings.RelativeAmplitudeVariationParameterList[1].ToString("0.000") });
            this.Rows.Add(new string[] { "Relative amplitude variation (end amplitude)", formantSettings.RelativeAmplitudeVariationParameterList[2].ToString("0.000") });
            this.Rows.Add(new string[] { "Relative pitch variation (constant)", formantSettings.RelativePitchVariationParameterList[0].ToString("0.000") });
            this.Rows.Add(new string[] { "Relative pitch variation (linear)", formantSettings.RelativePitchVariationParameterList[1].ToString("0.000") });
            this.Rows.Add(new string[] { "Relative pitch variation (quadratic)", formantSettings.RelativePitchVariationParameterList[2].ToString("0.000") });
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetColumnWidths();
        }

        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            base.OnCellValueChanged(e);
            double parameterValue;
            Boolean ok = double.TryParse(this.Rows[e.RowIndex].Cells[1].Value.ToString(), out parameterValue);
            if (ok)
            {
                if (e.RowIndex < formantSettings.FrequencyList.Count * 3)
                {
                    int sinusoidIndex = e.RowIndex / 3; // Integer division
                    int parameterIndex = e.RowIndex - sinusoidIndex*3;
                    if (parameterIndex == 0) { formantSettings.FrequencyList[sinusoidIndex] = parameterValue; }
                    else if (parameterIndex == 1) { formantSettings.AmplitudeList[sinusoidIndex] = parameterValue; }
                    else { formantSettings.BandwidthList[sinusoidIndex] = parameterValue; }
                }
                else
                {
                    int relativeIndex = e.RowIndex - formantSettings.FrequencyList.Count * 3;
                    if (relativeIndex == 0) { formantSettings.Duration = parameterValue; }
                    else if (relativeIndex == 1) { formantSettings.VoicedFraction = parameterValue; }
                    else if (relativeIndex == 2) { formantSettings.TransitionStart = parameterValue; }
                    else if (relativeIndex == 3) { formantSettings.RelativeAmplitudeVariationParameterList[0] = parameterValue; }
                    else if (relativeIndex == 4) { formantSettings.RelativeAmplitudeVariationParameterList[1] = parameterValue; }
                    else if (relativeIndex == 5) { formantSettings.RelativeAmplitudeVariationParameterList[2] = parameterValue; }
                    else if (relativeIndex == 6) { formantSettings.RelativePitchVariationParameterList[0] = parameterValue; }
                    else if (relativeIndex == 7) { formantSettings.RelativePitchVariationParameterList[1] = parameterValue; }
                    else if (relativeIndex == 8) { formantSettings.RelativePitchVariationParameterList[2] = parameterValue; }
                }
            }
        }

        public FormantSettings FormantSettings
        {
            get { return formantSettings; }
        }
    }
}
