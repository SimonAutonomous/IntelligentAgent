using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DirectShowLib;

namespace ImageProcessingLibrary.Cameras
{
    public partial class CameraSetupControl : UserControl
    {
        #region Fields
        private Camera camera;
        private Boolean running;
        private Bitmap cameraBitmap;
        private Thread cameraThread = null;
        private int millisecondSleepTime;
        #endregion

        #region Constants
        private const int RELATIVE_LARGE_TRACKBAR_STEP = 5;
        #endregion

        #region Constructors
        public CameraSetupControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Private methods
        private void ThreadSafeShowBitmap(Bitmap bitmap, PictureBox pictureBox)
        {
            cameraPictureBox.Invoke(new MethodInvoker(
                    delegate { ShowBitmap(bitmap, pictureBox); }
             ));
        }

        private void ShowBitmap(Bitmap bitmap, PictureBox pictureBox)
        {
            pictureBox.Image = bitmap;
        }

        private void CameraLoop()
        {
            while (running)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                if (!camera.Connected)
                {
                    running = false;
                    break;
                }
                cameraBitmap = camera.GetBitmap(); // camera.Read();
                if ((cameraBitmap != null) && (!cameraPictureBox.IsDisposed))
                {
                    ThreadSafeShowBitmap(cameraBitmap, cameraPictureBox);
                }
                stopWatch.Stop();
                int loopMillisecondSleepTime = millisecondSleepTime - (int)stopWatch.ElapsedMilliseconds;
                if (loopMillisecondSleepTime > 0) { Thread.Sleep(loopMillisecondSleepTime); }
            }
            cameraThread = null;
        }

        private void GetBrightnessRange()
        {
            CameraPropertySettings brightnessSettings = camera.CaptureDevice.GetVideoPropertySettings(VideoProcAmpProperty.Brightness);
            brightnessTrackBar.Minimum = brightnessSettings.Minimum;
            brightnessTrackBar.Maximum = brightnessSettings.Maximum;
            brightnessTrackBar.SmallChange = brightnessSettings.Step;
            brightnessTrackBar.LargeChange = RELATIVE_LARGE_TRACKBAR_STEP * brightnessSettings.Step;
            int currentValue = camera.CaptureDevice.GetVideoProperty(VideoProcAmpProperty.Brightness);
            brightnessTrackBar.Value = currentValue;
            brightnessTextBox.Text = currentValue.ToString();
            brightnessTrackBar.ValueChanged +=new EventHandler(BrightnessValueChanged);
        }

        public void BrightnessValueChanged(object sender, EventArgs e)
        {
            int newBrightnessValue = brightnessTrackBar.Value;
            brightnessTextBox.Text = newBrightnessValue.ToString();
            camera.CaptureDevice.SetVideoProperty(VideoProcAmpProperty.Brightness, newBrightnessValue);
        }

        private void GetContrastRange()
        {
            CameraPropertySettings contrastSettings = camera.CaptureDevice.GetVideoPropertySettings(VideoProcAmpProperty.Contrast);
            contrastTrackBar.Minimum = contrastSettings.Minimum;
            contrastTrackBar.Maximum = contrastSettings.Maximum;
            contrastTrackBar.SmallChange = contrastSettings.Step;
            contrastTrackBar.LargeChange = RELATIVE_LARGE_TRACKBAR_STEP * contrastSettings.Step;
            int currentValue = camera.CaptureDevice.GetVideoProperty(VideoProcAmpProperty.Contrast);
            contrastTrackBar.Value = currentValue;
            contrastTextBox.Text = currentValue.ToString();
            contrastTrackBar.ValueChanged += new EventHandler(ContrastValueChanged);
        }

        public void ContrastValueChanged(object sender, EventArgs e)
        {
            int newContrastValue = contrastTrackBar.Value;
            contrastTextBox.Text = newContrastValue.ToString();
            camera.CaptureDevice.SetVideoProperty(VideoProcAmpProperty.Contrast, newContrastValue);
        }

        private void GetSharpnessRange()
        {
            CameraPropertySettings sharpnessSettings = camera.CaptureDevice.GetVideoPropertySettings(VideoProcAmpProperty.Sharpness);
            sharpnessTrackBar.Minimum = sharpnessSettings.Minimum;
            sharpnessTrackBar.Maximum = sharpnessSettings.Maximum;
            sharpnessTrackBar.SmallChange = sharpnessSettings.Step;
            sharpnessTrackBar.LargeChange = RELATIVE_LARGE_TRACKBAR_STEP * sharpnessSettings.Step;
            int currentValue = camera.CaptureDevice.GetVideoProperty(VideoProcAmpProperty.Sharpness);
            sharpnessTrackBar.Value = currentValue;
            sharpnessTextBox.Text = currentValue.ToString();
            sharpnessTrackBar.ValueChanged += new EventHandler(SharpnessValueChanged);
        }

        public void SharpnessValueChanged(object sender, EventArgs e)
        {
            int newSharpnessValue = sharpnessTrackBar.Value;
            sharpnessTextBox.Text = newSharpnessValue.ToString();
            camera.CaptureDevice.SetVideoProperty(VideoProcAmpProperty.Sharpness, newSharpnessValue);
        }

        private void GetSaturationRange()
        {
            CameraPropertySettings saturationSettings = camera.CaptureDevice.GetVideoPropertySettings(VideoProcAmpProperty.Saturation);
            saturationTrackBar.Minimum = saturationSettings.Minimum;
            saturationTrackBar.Maximum = saturationSettings.Maximum;
            saturationTrackBar.SmallChange = saturationSettings.Step;
            saturationTrackBar.LargeChange = RELATIVE_LARGE_TRACKBAR_STEP * saturationSettings.Step;
            int currentValue = camera.CaptureDevice.GetVideoProperty(VideoProcAmpProperty.Saturation);
            saturationTrackBar.Value = currentValue;
            saturationTextBox.Text = currentValue.ToString();
            saturationTrackBar.ValueChanged += new EventHandler(SaturationValueChanged);
        }

        public void SaturationValueChanged(object sender, EventArgs e)
        {
            int newSaturationValue = saturationTrackBar.Value;
            saturationTextBox.Text = newSaturationValue.ToString();
            camera.CaptureDevice.SetVideoProperty(VideoProcAmpProperty.Saturation, newSaturationValue);
        }

        private void GetHueRange()
        {
            CameraPropertySettings hueSettings = camera.CaptureDevice.GetVideoPropertySettings(VideoProcAmpProperty.Hue);
            hueTrackBar.Minimum = hueSettings.Minimum;
            hueTrackBar.Maximum = hueSettings.Maximum;
            hueTrackBar.SmallChange = hueSettings.Step;
            hueTrackBar.LargeChange = RELATIVE_LARGE_TRACKBAR_STEP * hueSettings.Step;
            int currentValue = camera.CaptureDevice.GetVideoProperty(VideoProcAmpProperty.Hue);
            hueTrackBar.Value = currentValue;
            hueTextBox.Text = currentValue.ToString();
            hueTrackBar.ValueChanged += new EventHandler(HueValueChanged);
        }

        public void HueValueChanged(object sender, EventArgs e)
        {
            int newHueValue = hueTrackBar.Value;
            hueTextBox.Text = newHueValue.ToString();
            camera.CaptureDevice.SetVideoProperty(VideoProcAmpProperty.Hue, newHueValue);
        }

        private void GetGainRange()
        {
            CameraPropertySettings gainSettings = camera.CaptureDevice.GetVideoPropertySettings(VideoProcAmpProperty.Gain);
            gainTrackBar.Minimum = gainSettings.Minimum;
            gainTrackBar.Maximum = gainSettings.Maximum;
            gainTrackBar.SmallChange = gainSettings.Step;
            gainTrackBar.LargeChange = RELATIVE_LARGE_TRACKBAR_STEP * gainSettings.Step;
            int currentValue = camera.CaptureDevice.GetVideoProperty(VideoProcAmpProperty.Gain);
            gainTrackBar.Value = currentValue;
            gainTextBox.Text = currentValue.ToString();
            if (gainSettings.Minimum == gainSettings.Maximum)
            {
                gainTrackBar.Enabled = false;
                gainTextBox.Enabled = false;
            }
            else
            {
                gainTrackBar.ValueChanged += new EventHandler(GainValueChanged);
            }
        }

        public void GainValueChanged(object sender, EventArgs e)
        {
            int newGainValue = gainTrackBar.Value;
            gainTextBox.Text = newGainValue.ToString();
            camera.CaptureDevice.SetVideoProperty(VideoProcAmpProperty.Gain, newGainValue);
        }

        private void GetGammaRange()
        {
            CameraPropertySettings gammaSettings = camera.CaptureDevice.GetVideoPropertySettings(VideoProcAmpProperty.Gamma);
            gammaTrackBar.Minimum = gammaSettings.Minimum;
            gammaTrackBar.Maximum = gammaSettings.Maximum;
            gammaTrackBar.SmallChange = gammaSettings.Step;
            gammaTrackBar.LargeChange = RELATIVE_LARGE_TRACKBAR_STEP * gammaSettings.Step;
            int currentValue = camera.CaptureDevice.GetVideoProperty(VideoProcAmpProperty.Gamma);
            gammaTrackBar.Value = currentValue;
            gammaTextBox.Text = currentValue.ToString();
            if (gammaSettings.Minimum == gammaSettings.Maximum)
            {
                gammaTrackBar.Enabled = false;
                gammaTextBox.Enabled = false;
            }
            else
            {
                gammaTrackBar.ValueChanged += new EventHandler(GammaValueChanged);
            }
        }

        public void GammaValueChanged(object sender, EventArgs e)
        {
            int newGammaValue = gammaTrackBar.Value;
            gammaTextBox.Text = newGammaValue.ToString();
            camera.CaptureDevice.SetVideoProperty(VideoProcAmpProperty.Gamma, newGammaValue);
        }

        private void GetWhiteBalance()
        {
            CameraPropertySettings whiteBalanceSettings = camera.CaptureDevice.GetVideoPropertySettings(VideoProcAmpProperty.WhiteBalance);
            whiteBalanceTrackBar.Minimum = whiteBalanceSettings.Minimum;
            whiteBalanceTrackBar.Maximum = whiteBalanceSettings.Maximum;
            whiteBalanceTrackBar.SmallChange = whiteBalanceSettings.Step;
            whiteBalanceTrackBar.LargeChange = RELATIVE_LARGE_TRACKBAR_STEP * whiteBalanceSettings.Step;
            int currentValue = camera.CaptureDevice.GetVideoProperty(VideoProcAmpProperty.WhiteBalance);
            whiteBalanceTrackBar.Value = currentValue;
            whiteBalanceTextBox.Text = currentValue.ToString();
            if (whiteBalanceSettings.Minimum == whiteBalanceSettings.Maximum)
            {
                whiteBalanceTrackBar.Enabled = false;
                whiteBalanceTextBox.Enabled = false;
            }
            else
            {
                whiteBalanceTrackBar.ValueChanged += new EventHandler(WhiteBalanceChanged);
            }
        }

        public void WhiteBalanceChanged(object sender, EventArgs e)
        {
            int newWhiteBalanceValue = whiteBalanceTrackBar.Value;
            whiteBalanceTextBox.Text = newWhiteBalanceValue.ToString();
            camera.CaptureDevice.SetVideoProperty(VideoProcAmpProperty.WhiteBalance, newWhiteBalanceValue);
        }

        private void GetBacklightCompensationRange()
        {
            CameraPropertySettings backlightCompensationSettings = camera.CaptureDevice.GetVideoPropertySettings(VideoProcAmpProperty.BacklightCompensation);
            backlightCompensationComboBox.SelectedIndex = backlightCompensationSettings.DefaultValue;
            if (backlightCompensationSettings.Minimum == backlightCompensationSettings.Maximum)
            {
                backlightCompensationComboBox.Enabled = false;
            }
            else
            {
                backlightCompensationComboBox.SelectedIndexChanged += new EventHandler(BacklightCompensationChanged);
            }
        }

        public void BacklightCompensationChanged(object sender, EventArgs e)
        {
            int valueIndex = backlightCompensationComboBox.SelectedIndex;
            camera.CaptureDevice.SetVideoProperty(VideoProcAmpProperty.BacklightCompensation, valueIndex);
        }

        private void GetExposureRange()
        {
            CameraPropertySettings exposureSettings = camera.CaptureDevice.GetCameraControlPropertySettings(CameraControlProperty.Exposure);
            exposureTrackBar.Minimum = exposureSettings.Minimum;
            exposureTrackBar.Maximum = exposureSettings.Maximum;
            exposureTrackBar.SmallChange = exposureSettings.Step;
            exposureTrackBar.LargeChange = RELATIVE_LARGE_TRACKBAR_STEP * exposureSettings.Step;
            int currentValue = camera.CaptureDevice.GetCameraControlProperty(CameraControlProperty.Exposure);
            exposureTrackBar.Value = currentValue;
            exposureTextBox.Text = currentValue.ToString();
            if (exposureSettings.Minimum == exposureSettings.Maximum)
            {
                exposureTrackBar.Enabled = false;
                exposureTextBox.Enabled = false;
            }
            else
            {
                exposureTrackBar.ValueChanged += new EventHandler(ExposureValueChanged);
            }
        }

        public void ExposureValueChanged(object sender, EventArgs e)
        {
            int newExposureValue = exposureTrackBar.Value;
            exposureTextBox.Text = newExposureValue.ToString();
            camera.CaptureDevice.SetCameraControlProperty(CameraControlProperty.Exposure, newExposureValue);
        }

        private void GetFocusRange()
        {
            CameraPropertySettings focusSettings = camera.CaptureDevice.GetCameraControlPropertySettings(CameraControlProperty.Focus);
            focusTrackBar.Minimum = focusSettings.Minimum;
            focusTrackBar.Maximum = focusSettings.Maximum;
            focusTrackBar.SmallChange = focusSettings.Step;
            focusTrackBar.LargeChange = RELATIVE_LARGE_TRACKBAR_STEP * focusSettings.Step;
            int currentValue = camera.CaptureDevice.GetCameraControlProperty(CameraControlProperty.Focus);
            focusTrackBar.Value = currentValue;
            focusTextBox.Text = currentValue.ToString();
            if (focusSettings.Minimum == focusSettings.Maximum)
            {
                focusTrackBar.Enabled = false;
                focusTextBox.Enabled = false;
            }
            else
            {
                focusTrackBar.ValueChanged += new EventHandler(FocusValueChanged);
            }
        }

        public void FocusValueChanged(object sender, EventArgs e)
        {
            int newFocusValue = focusTrackBar.Value;
            focusTextBox.Text = newFocusValue.ToString();
            camera.CaptureDevice.SetCameraControlProperty(CameraControlProperty.Focus, newFocusValue);
        }

        private void GetIrisRange()
        {
            CameraPropertySettings irisSettings = camera.CaptureDevice.GetCameraControlPropertySettings(CameraControlProperty.Iris);
            irisTrackBar.Minimum = irisSettings.Minimum;
            irisTrackBar.Maximum = irisSettings.Maximum;
            irisTrackBar.SmallChange = irisSettings.Step;
            irisTrackBar.LargeChange = RELATIVE_LARGE_TRACKBAR_STEP * irisSettings.Step;
            int currentValue = camera.CaptureDevice.GetCameraControlProperty(CameraControlProperty.Iris);
            irisTrackBar.Value = currentValue;
            irisTextBox.Text = currentValue.ToString();
            if (irisSettings.Minimum == irisSettings.Maximum)
            {
                irisTrackBar.Enabled = false;
                irisTextBox.Enabled = false;
            }
            else
            {
                irisTrackBar.ValueChanged += new EventHandler(IrisValueChanged);
            }
        }

        public void IrisValueChanged(object sender, EventArgs e)
        {
            int newIrisValue = irisTrackBar.Value;
            irisTextBox.Text = newIrisValue.ToString();
            camera.CaptureDevice.SetCameraControlProperty(CameraControlProperty.Iris, newIrisValue);
        }

        private void GetZoomRange()
        {
            CameraPropertySettings zoomSettings = camera.CaptureDevice.GetCameraControlPropertySettings(CameraControlProperty.Zoom);
            zoomTrackBar.Minimum = zoomSettings.Minimum;
            zoomTrackBar.Maximum = zoomSettings.Maximum;
            zoomTrackBar.SmallChange = zoomSettings.Step;
            zoomTrackBar.LargeChange = RELATIVE_LARGE_TRACKBAR_STEP * zoomSettings.Step;
            int currentValue = camera.CaptureDevice.GetCameraControlProperty(CameraControlProperty.Zoom);
            if (currentValue < zoomTrackBar.Minimum) { currentValue = zoomTrackBar.Minimum; }
            zoomTrackBar.Value = currentValue;
            zoomTextBox.Text = currentValue.ToString();
            if (zoomSettings.Minimum == zoomSettings.Maximum)
            {
                zoomTrackBar.Enabled = false;
                zoomTextBox.Enabled = false;
            }
            else
            {
                zoomTrackBar.ValueChanged += new EventHandler(ZoomValueChanged);
            }
        }

        private void ZoomValueChanged(object sender, EventArgs e)
        {
            int newZoomValue = zoomTrackBar.Value;
            zoomTextBox.Text = newZoomValue.ToString();
            camera.CaptureDevice.SetCameraControlProperty(CameraControlProperty.Zoom, newZoomValue);
        }
        #endregion

        #region Public methods
        
        public void SetCamera(Camera camera)
        {
            this.camera = camera;
            millisecondSleepTime = (int)Math.Round(1000.0 / camera.FrameRate);
            if (camera.Connected)
            {
                if (!running)
                {
                    cameraThread = new Thread(new ThreadStart(CameraLoop));
                    running = true;
                    cameraThread.Start();
                    GetBrightnessRange();
                    GetContrastRange();
                    GetSharpnessRange();
                    GetSaturationRange();
                    GetHueRange();
                    GetGainRange();
                    GetGammaRange();
                    GetWhiteBalance();
                    GetBacklightCompensationRange();
                    //   GetColorEnableRange();
                    GetExposureRange();
                    GetFocusRange();
                    GetIrisRange();
                    GetZoomRange();
                }
            }
            else
            {
                MessageBox.Show("Please connect the camera first!");
            }
        }

        public void Stop()
        {
            if (cameraThread != null)
            {
                running = false;
            //    cameraThread.Abort();
            }
        }
        #endregion

        public Boolean Running
        {
            get { return running; }
        }
    }
}
