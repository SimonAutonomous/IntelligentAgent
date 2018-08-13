using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging; // Needed for the ImageFormat
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImageProcessingLibrary;

namespace ImageProcessingApplication
{
    public partial class ImageProcessingMainForm : Form
    {
        private List<Tuple<string, Bitmap>> imageSequenceList = null;

        public ImageProcessingMainForm()
        {
            InitializeComponent();
        }

        private void ShowImageSequenceList()
        {
            imageSequenceListBox.Items.Clear();
            for (int ii = 0; ii < imageSequenceList.Count; ii++)
            {
                imageSequenceListBox.Items.Add(imageSequenceList[ii].Item1);
            }
            imageSequenceListBox.SelectedIndex = imageSequenceListBox.Items.Count - 1;
        }

        private void RemoveTrailingItems(int sequenceIndex)
        {
            int index = imageSequenceListBox.Items.Count - 1;
            while (index > sequenceIndex)
            {
                imageSequenceListBox.Items.RemoveAt(index);
                imageSequenceList.RemoveAt(index);
                index = imageSequenceListBox.Items.Count - 1;
            }
        }

        private void imageSequenceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bitmap bitmap = imageSequenceList[imageSequenceListBox.SelectedIndex].Item2;
            imageProcessingPlot.SetImage(bitmap);
        }

        private void sobelEdgeDetectButton_Click(object sender, EventArgs e)
        {
            int sequenceIndex = imageSequenceListBox.SelectedIndex;
            Bitmap bitmap = imageSequenceList[sequenceIndex].Item2;
            RemoveTrailingItems(sequenceIndex);
            ImageProcessor imageProcessor = new ImageProcessor(bitmap);
            imageProcessor.EdgeDetectSobel();
            imageProcessor.Release();
            Bitmap processedBitmap = imageProcessor.Bitmap;
            imageProcessor.Dispose();
            imageSequenceList.Add(new Tuple<string, Bitmap>("Sobel edge detection", processedBitmap));
            ShowImageSequenceList();
        }

        private void gaussianBlurButton_Click(object sender, EventArgs e)
        {
            int sequenceIndex = imageSequenceListBox.SelectedIndex;
            Bitmap bitmap = imageSequenceList[sequenceIndex].Item2;
            RemoveTrailingItems(sequenceIndex);
            ImageProcessor imageProcessor = new ImageProcessor(bitmap);
            imageProcessor.GaussianBlur3x3();
            imageProcessor.Release();
            Bitmap processedBitmap = imageProcessor.Bitmap;
            imageProcessor.Dispose();
            imageSequenceList.Add(new Tuple<string, Bitmap>("Gaussian blurring", processedBitmap));
            ShowImageSequenceList();
        }

        private void boxBlurButton_Click(object sender, EventArgs e)
        {
            int sequenceIndex = imageSequenceListBox.SelectedIndex;
            Bitmap bitmap = imageSequenceList[sequenceIndex].Item2;
            RemoveTrailingItems(sequenceIndex);
            ImageProcessor imageProcessor = new ImageProcessor(bitmap);
            imageProcessor.BoxBlur3x3();
            imageProcessor.Release();
            Bitmap processedBitmap = imageProcessor.Bitmap;
            imageProcessor.Dispose();
            imageSequenceList.Add(new Tuple<string, Bitmap>("Box blurring", processedBitmap));
            ShowImageSequenceList();
        }


        private void sharpeningFactorTextBox_TextChanged(object sender, EventArgs e)
        {
            sharpenButton.Enabled = false;
            double sharpeningFactor;
            Boolean ok = double.TryParse(sharpeningFactorTextBox.Text, out sharpeningFactor);
            if (ok)
            {
                if (sharpeningFactor > 0) { sharpenButton.Enabled = true; }
            }
        }

        private void sharpenButton_Click(object sender, EventArgs e)
        {
            int sequenceIndex = imageSequenceListBox.SelectedIndex;
            Bitmap bitmap = imageSequenceList[sequenceIndex].Item2;
            RemoveTrailingItems(sequenceIndex);
            double sharpeningFactor = double.Parse(sharpeningFactorTextBox.Text);
            ImageProcessor imageProcessor = new ImageProcessor(bitmap);
            imageProcessor.Sharpen3x3(sharpeningFactor);
            imageProcessor.Release();
            Bitmap processedBitmap = imageProcessor.Bitmap;
            imageProcessor.Dispose();
            imageSequenceList.Add(new Tuple<string, Bitmap>("Sharpening (" + sharpeningFactor.ToString("0.000") + ")", processedBitmap));
            ShowImageSequenceList();
        }

        private void contrastAlphaTextBox_TextChanged(object sender, EventArgs e)
        {
            changeContrastButton.Enabled = false;
            double alpha;
            Boolean ok = double.TryParse(contrastAlphaTextBox.Text, out alpha);
            if (ok)
            {
                if (alpha > 0) { changeContrastButton.Enabled = true; }
            }
        }

        private void changeContrastButton_Click(object sender, EventArgs e)
        {
            double alpha = double.Parse(contrastAlphaTextBox.Text);
            int sequenceIndex = imageSequenceListBox.SelectedIndex;
            Bitmap bitmap = imageSequenceList[sequenceIndex].Item2;
            RemoveTrailingItems(sequenceIndex);
            ImageProcessor imageProcessor = new ImageProcessor(bitmap);
            imageProcessor.ChangeContrast(alpha); //  
            imageProcessor.Release();
            Bitmap processedBitmap = imageProcessor.Bitmap;
            imageSequenceList.Add(new Tuple<string, Bitmap>("Contrast change (" + alpha.ToString("0.000") + ")", processedBitmap));
            ShowImageSequenceList();
        }

        private void relativeBrightnessTextBox_TextChanged(object sender, EventArgs e)
        {
            changeBrightnessButton.Enabled = false;
            double relativeBrightness;
            Boolean ok = double.TryParse(relativeBrightnessTextBox.Text, out relativeBrightness);
            if (ok)
            {
                if (relativeBrightness > 0) { changeBrightnessButton.Enabled = true; }
            }
        }

        private void changeBrightnessButton_Click(object sender, EventArgs e)
        {
            double relativeBrightness = double.Parse(relativeBrightnessTextBox.Text);
            int sequenceIndex = imageSequenceListBox.SelectedIndex;
            Bitmap bitmap = imageSequenceList[sequenceIndex].Item2;
            RemoveTrailingItems(sequenceIndex);
            ImageProcessor imageProcessor = new ImageProcessor(bitmap);
            imageProcessor.ChangeBrightness(relativeBrightness);
            imageProcessor.Release();
            Bitmap processedBitmap = imageProcessor.Bitmap;
            imageSequenceList.Add(new Tuple<string, Bitmap>("Brightness change (" + relativeBrightness.ToString("0.000") + ")", processedBitmap));
            ShowImageSequenceList();
        }

        private void binarizationThresholdTextBox_TextChanged(object sender, EventArgs e)
        {
            binarizeButton.Enabled = false;
            int binarizationThreshold;
            Boolean ok = int.TryParse(binarizationThresholdTextBox.Text, out binarizationThreshold);
            if (ok)
            {
                if ((binarizationThreshold >= 0) && (binarizationThreshold <= 255)) { binarizeButton.Enabled = true; }
            }
        }

        private void binarizeButton_Click(object sender, EventArgs e)
        {
            int binarizationThreshold = int.Parse(binarizationThresholdTextBox.Text);
            int sequenceIndex = imageSequenceListBox.SelectedIndex;
            Bitmap bitmap = imageSequenceList[sequenceIndex].Item2;
            RemoveTrailingItems(sequenceIndex);
            ImageProcessor imageProcessor = new ImageProcessor(bitmap);
            imageProcessor.Binarize(binarizationThreshold);
            imageProcessor.Release();
            Bitmap processedBitmap = imageProcessor.Bitmap;
            imageSequenceList.Add(new Tuple<string, Bitmap>("Binarization (" + binarizationThreshold.ToString() + ")", processedBitmap));
            ShowImageSequenceList();
        }

        private void convertToStandardGrayscaleButton_Click(object sender, EventArgs e)
        {
            int sequenceIndex = imageSequenceListBox.SelectedIndex;
            Bitmap bitmap = imageSequenceList[sequenceIndex].Item2;
            RemoveTrailingItems(sequenceIndex);
            ImageProcessor imageProcessor = new ImageProcessor(bitmap);
            imageProcessor.ConvertToStandardGrayscale();
            imageProcessor.Release();
            Bitmap processedBitmap = imageProcessor.Bitmap;
            imageSequenceList.Add(new Tuple<string, Bitmap>("Grayscale conversion", processedBitmap));
            ShowImageSequenceList();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bitmap = (Bitmap)Image.FromFile(openFileDialog.FileName);
                    imageSequenceList = new List<Tuple<string, Bitmap>>();
                    imageSequenceList.Add(new Tuple<string, Bitmap>("Original image", bitmap));
                    ShowImageSequenceList();
                    saveImageToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Bitmap Image (.bmp)|*.bmp|JPEG Image (.jpeg, .jpg)|*.jpeg;*.jpg|Png Image (.png)|*.png";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imageFormatString = Path.GetExtension(saveFileDialog.FileName).ToLower();
                    ImageFormat imageFormat = ImageFormat.Bmp;
                    if (imageFormatString == ".jpg" || imageFormatString == ".jpeg") { imageFormat = ImageFormat.Jpeg; }
                    else if (imageFormatString == ".png") { imageFormat = ImageFormat.Png; }
                    int sequenceIndex = imageSequenceListBox.SelectedIndex;
                    Bitmap bitmap = imageSequenceList[sequenceIndex].Item2;
                    bitmap.Save(saveFileDialog.FileName, imageFormat);
                }
            }
        }

    }
}
