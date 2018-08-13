using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using DirectShowLib;


namespace ImageProcessingLibrary.Cameras
{
    public class CaptureDevice : ISampleGrabberCB, IDisposable
    {
        #region Constants
        private const int timeout = 5000;
        #endregion

        #region Fields
        private IntPtr imageHandle;
        private bool running = false;
        private IFilterGraph2 filterGraph;
        private IMediaControl mediaControl;
        private volatile bool hasPicture = false;
        #endregion

        #region Auto properties
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Stride { get; private set; }
        public int Dropped { get; private set; }
        #endregion

        #region Wait handles
        private ManualResetEvent pictureReady;
        #endregion

        #region External methods
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        private static extern void CopyMemory(IntPtr destination, IntPtr source, int length);
        #endregion

        #region Constructor and destructor
        public CaptureDevice(int deviceId, int frameRate, int width, int height)
        {
            Dropped = 0;

            DsDevice[] captureDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            try
            {
                SetupGraph(captureDevices[deviceId], frameRate, width, height);

                pictureReady = new ManualResetEvent(false);
                hasPicture = true;
                running = false;
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        ~CaptureDevice()
        {
            Dispose();
        }
        #endregion

        #region Public methods
        public void Start()
        {
            if (!running)
            {
                int hr = mediaControl.Run();
                DsError.ThrowExceptionForHR(hr);
                running = true;
            }
        }

        public void Pause()
        {
            if (running)
            {
                int hr = mediaControl.Pause();
                DsError.ThrowExceptionForHR(hr);
                running = false;
            }
        }

        public void Test()
        {

        }

        public void Dispose()
        {
            int hr;
            try
            {
                if (mediaControl != null)
                {
                    hr = mediaControl.Stop();
                    running = false;
                }
            }
            catch { }

            if (filterGraph != null)
            {
                Marshal.ReleaseComObject(filterGraph);
                filterGraph = null;
            }

            if (pictureReady != null)
            {
                pictureReady.Close();
                pictureReady = null;
            }
        }

        public IntPtr GetBitMap()
        {
            imageHandle = IntPtr.Zero;
            imageHandle = Marshal.AllocCoTaskMem(Stride * Height);

            try
            {
                pictureReady.Reset();
                hasPicture = false;

                if (!pictureReady.WaitOne(timeout, false))
                {
                    throw new Exception("Timeout waiting to get picture.");
                }
            }
            catch
            {
                Marshal.FreeCoTaskMem(imageHandle);
                throw;
            }
            return imageHandle;
        }

        public CameraPropertySettings GetCameraControlPropertySettings(CameraControlProperty property)
        {
            int filterHandle;
            IBaseFilter captureFilter = null;
            filterHandle = filterGraph.FindFilterByName("Video input", out captureFilter);
            if (captureFilter != null)
            {
                int min = 0;
                int max = 0;
                int stepDelta = 1;
                int defaultValue = 0;
                CameraControlFlags cameraControlFlags;
                IAMCameraControl iC = captureFilter as IAMCameraControl;
                ((IAMCameraControl)iC).GetRange(property, out min, out max, out stepDelta, out defaultValue, out cameraControlFlags);
                CameraPropertySettings videoPropertySettings = new CameraPropertySettings();
                videoPropertySettings.Minimum = min;
                videoPropertySettings.Maximum = max;
                videoPropertySettings.Step = stepDelta;
                videoPropertySettings.DefaultValue = defaultValue;
                return videoPropertySettings;
            }
            else
            {
                return null;
            }
        }

        public int GetCameraControlProperty(CameraControlProperty property)
        {
            int filterHandle;
            IBaseFilter captureFilter = null;
            filterHandle = filterGraph.FindFilterByName("Video input", out captureFilter);
            int propertyValue = 0;
            CameraControlFlags cameraControlFlags;
            if (captureFilter != null)
            {
                IAMCameraControl iC = captureFilter as IAMCameraControl;
                ((IAMCameraControl)iC).Get(property, out propertyValue, out cameraControlFlags);
            }
            return propertyValue;
        }

        public void SetCameraControlProperty(CameraControlProperty property, int propertyValue)
        {
            int filterHandle;
            IBaseFilter captureFilter = null;
            filterHandle = filterGraph.FindFilterByName("Video input", out captureFilter);
            if (captureFilter != null)
            {
                IAMCameraControl iC = captureFilter as IAMCameraControl;
                ((IAMCameraControl)iC).Set(property, propertyValue, CameraControlFlags.Manual);
            }
        }

        public CameraPropertySettings GetVideoPropertySettings(VideoProcAmpProperty property)
        {
            int filterHandle;
            IBaseFilter captureFilter = null;
            filterHandle = filterGraph.FindFilterByName("Video input", out captureFilter);
            if (captureFilter != null)
            {
                int min = 0;
                int max = 0;
                int stepDelta = 1;
                int defaultValue = 0;
                VideoProcAmpFlags videoFlags;
                IAMCameraControl iC = captureFilter as IAMCameraControl;
                ((IAMVideoProcAmp)iC).GetRange(property, out min, out max, out stepDelta, out defaultValue, out videoFlags);
                CameraPropertySettings videoPropertySettings = new CameraPropertySettings();
                videoPropertySettings.Minimum = min;
                videoPropertySettings.Maximum = max;
                videoPropertySettings.Step = stepDelta;
                videoPropertySettings.DefaultValue = defaultValue;
                return videoPropertySettings;
            }
            else
            {
                return null;
            }
        }

        public int GetVideoProperty(VideoProcAmpProperty property)
        {
            int filterHandle;
            IBaseFilter captureFilter = null;
            filterHandle = filterGraph.FindFilterByName("Video input", out captureFilter);
            int propertyValue = 0;
            VideoProcAmpFlags videoProcAmpFlags;
            if (captureFilter != null)
            {
                IAMCameraControl iC = captureFilter as IAMCameraControl;
                ((IAMVideoProcAmp)iC).Get(property, out propertyValue, out videoProcAmpFlags);
            }
            return propertyValue;
        }

        public void SetVideoProperty(VideoProcAmpProperty property, int propertyValue)
        {
            int filterHandle;
            IBaseFilter captureFilter = null;
            filterHandle = filterGraph.FindFilterByName("Video input", out captureFilter);
            if (captureFilter != null)
            {
                IAMCameraControl iC = captureFilter as IAMCameraControl;
                ((IAMVideoProcAmp)iC).Set(property, propertyValue, VideoProcAmpFlags.Manual);
            }
        }

        public static List<CameraResolution> GetAvailableResolutions(int deviceIndex)  // DsDevice vidDev)
        {
            try
            {
                DsDevice[] captureDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
                DsDevice vidDev = captureDevices[deviceIndex];
                int hr;
                int max = 0;
                int bitCount = 0;

                IBaseFilter sourceFilter = null;

                var mFilterGraph2 = new FilterGraph() as IFilterGraph2;

                hr = mFilterGraph2.AddSourceFilterForMoniker(vidDev.Mon, null, vidDev.Name, out sourceFilter);

                var pRaw2 = DsFindPin.ByCategory(sourceFilter, PinCategory.Capture, 0);

                var AvailableResolutions = new List<CameraResolution>();

                VideoInfoHeader videoInfoHeader = new VideoInfoHeader();
                IEnumMediaTypes mediaTypeEnum;
                hr = pRaw2.EnumMediaTypes(out mediaTypeEnum);

                AMMediaType[] mediaTypes = new AMMediaType[1];
                IntPtr fetched = IntPtr.Zero;
                hr = mediaTypeEnum.Next(1, mediaTypes, fetched);

                while (fetched != null && mediaTypes[0] != null)
                {
                    Marshal.PtrToStructure(mediaTypes[0].formatPtr, videoInfoHeader);
                    if (videoInfoHeader.BmiHeader.Size != 0 && videoInfoHeader.BmiHeader.BitCount != 0)
                    {
                        if (videoInfoHeader.BmiHeader.BitCount > bitCount)
                        {
                            AvailableResolutions.Clear();
                            max = 0;
                            bitCount = videoInfoHeader.BmiHeader.BitCount;
                        }
                        CameraResolution availableResolution = new CameraResolution();
                        availableResolution.HorizontalResolution = videoInfoHeader.BmiHeader.Width;
                        availableResolution.VerticalResolution = videoInfoHeader.BmiHeader.Height;
                        AvailableResolutions.Add(availableResolution);
                        if (videoInfoHeader.BmiHeader.Width > max || videoInfoHeader.BmiHeader.Height > max)
                            max = (Math.Max(videoInfoHeader.BmiHeader.Width, videoInfoHeader.BmiHeader.Height));
                    }
                    hr = mediaTypeEnum.Next(1, mediaTypes, fetched);
                }
                return AvailableResolutions;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        // Obsolete test method.
      /*  public void SetCameraSettings()
        {
            int hr;
            IBaseFilter captureFilter = null;
            //     filterGraph = (IFilterGraph2)new FilterGraph();
            hr = filterGraph.FindFilterByName("Video input", out captureFilter);
            if (captureFilter != null)
            {
                int min;
                int max;
                int i1;
                int i2;
                int iValue;
                VideoProcAmpFlags vFlags;
                IAMCameraControl iC = captureFilter as IAMCameraControl;
                ((IAMVideoProcAmp)iC).Get(VideoProcAmpProperty.Brightness, out iValue, out vFlags);
                iValue += 10;
                ((IAMVideoProcAmp)iC).GetRange(VideoProcAmpProperty.Brightness, out min, out max, out i1, out i2, out vFlags);
                ((IAMVideoProcAmp)iC).Set(VideoProcAmpProperty.Brightness, iValue, VideoProcAmpFlags.Manual);
            }
        } */
        #endregion

        #region Private methods
        

        private void SetupGraph(DsDevice captureDevice, int frameRate, int width, int height)
        {
            int hr;

            ISampleGrabber sampleGrabber = null;
            IBaseFilter captureFilter = null;
            ICaptureGraphBuilder2 captureGraphBuilder = null;

            filterGraph = (IFilterGraph2)new FilterGraph();
            mediaControl = filterGraph as IMediaControl;
            try
            {
                captureGraphBuilder = (ICaptureGraphBuilder2)(new CaptureGraphBuilder2());
                sampleGrabber = (ISampleGrabber)(new SampleGrabber());

                hr = captureGraphBuilder.SetFiltergraph(filterGraph);
                DsError.ThrowExceptionForHR(hr);

                hr = filterGraph.AddSourceFilterForMoniker(captureDevice.Mon, null, "Video input", out captureFilter);
                DsError.ThrowExceptionForHR(hr);

                IBaseFilter baseGrabFlt = (IBaseFilter)sampleGrabber;
                ConfigureSampleGrabber(sampleGrabber);

                hr = filterGraph.AddFilter(baseGrabFlt, "Ds.NET Grabber");
                DsError.ThrowExceptionForHR(hr);

                SetConfigParameters(captureGraphBuilder, captureFilter, frameRate, width, height);

                hr = captureGraphBuilder.RenderStream(PinCategory.Capture, MediaType.Video, captureFilter, null, baseGrabFlt);
                DsError.ThrowExceptionForHR(hr);

                SaveSizeInfo(sampleGrabber);
            }
            finally
            {
                if (captureFilter != null)
                {

                    // Focus test
                    IAMCameraControl iC = captureFilter as IAMCameraControl;
                    int min;
                    int max;
                    int i1;
                    int i2;


                    CameraControlFlags flags;
                    ((IAMCameraControl)iC).GetRange(CameraControlProperty.Exposure, out min, out max, out i1, out i2, out flags);
                    ((IAMCameraControl)iC).Get(CameraControlProperty.Exposure, out i1, out flags);
                    ((IAMCameraControl)iC).Set(CameraControlProperty.Exposure, -1, CameraControlFlags.Manual);
                    ((IAMCameraControl)iC).GetRange(CameraControlProperty.Iris, out min, out max, out i1, out i2, out flags);
                    ((IAMCameraControl)iC).Set(CameraControlProperty.Iris, -10, CameraControlFlags.Manual);
                    ((IAMCameraControl)iC).GetRange(CameraControlProperty.Focus, out min, out max, out i1, out i2, out flags);
                    ((IAMCameraControl)iC).Set(CameraControlProperty.Focus, 20, CameraControlFlags.Manual);
                    ((IAMCameraControl)iC).GetRange(CameraControlProperty.Zoom, out min, out max, out i1, out i2, out flags);
                    ((IAMCameraControl)iC).Set(CameraControlProperty.Zoom, 0, CameraControlFlags.Manual);
                    VideoProcAmpFlags vFlags;
                    ((IAMVideoProcAmp)iC).GetRange(VideoProcAmpProperty.BacklightCompensation, out min, out max, out i1, out i2, out vFlags);
                    ((IAMVideoProcAmp)iC).Set(VideoProcAmpProperty.BacklightCompensation, 0, VideoProcAmpFlags.Manual);
                    ((IAMVideoProcAmp)iC).GetRange(VideoProcAmpProperty.Brightness, out min, out max, out i1, out i2, out vFlags);
                    ((IAMVideoProcAmp)iC).Set(VideoProcAmpProperty.Brightness, 74, VideoProcAmpFlags.Manual);
                    ((IAMVideoProcAmp)iC).GetRange(VideoProcAmpProperty.Contrast, out min, out max, out i1, out i2, out vFlags);
                    ((IAMVideoProcAmp)iC).Set(VideoProcAmpProperty.Contrast, 141, VideoProcAmpFlags.Manual);
                    ((IAMVideoProcAmp)iC).GetRange(VideoProcAmpProperty.Sharpness, out min, out max, out i1, out i2, out vFlags);
                    ((IAMVideoProcAmp)iC).Set(VideoProcAmpProperty.Sharpness, 87, VideoProcAmpFlags.Manual);
                    ((IAMVideoProcAmp)iC).GetRange(VideoProcAmpProperty.WhiteBalance, out min, out max, out i1, out i2, out vFlags);
                    ((IAMVideoProcAmp)iC).Set(VideoProcAmpProperty.WhiteBalance, 5600, VideoProcAmpFlags.Manual);
                    ((IAMVideoProcAmp)iC).GetRange(VideoProcAmpProperty.Gamma, out min, out max, out i1, out i2, out vFlags);
                    ((IAMVideoProcAmp)iC).Set(VideoProcAmpProperty.Gamma, 3000, VideoProcAmpFlags.Manual);
                    ((IAMVideoProcAmp)iC).GetRange(VideoProcAmpProperty.Saturation, out min, out max, out i1, out i2, out vFlags);
                    ((IAMVideoProcAmp)iC).Set(VideoProcAmpProperty.Saturation, 115, VideoProcAmpFlags.Manual);
                    ((IAMVideoProcAmp)iC).GetRange(VideoProcAmpProperty.Gain, out min, out max, out i1, out i2, out vFlags);
                    ((IAMVideoProcAmp)iC).Set(VideoProcAmpProperty.Gain, 100, VideoProcAmpFlags.Manual);
                    ((IAMVideoProcAmp)iC).GetRange(VideoProcAmpProperty.Hue, out min, out max, out i1, out i2, out vFlags);
                    ((IAMVideoProcAmp)iC).Set(VideoProcAmpProperty.Hue, 10, VideoProcAmpFlags.Manual);
                    // End test




                    Marshal.ReleaseComObject(captureFilter);
                    captureFilter = null;
                }
                if (sampleGrabber != null)
                {
                    Marshal.ReleaseComObject(sampleGrabber);
                    sampleGrabber = null;
                }
                if (captureGraphBuilder != null)
                {
                    Marshal.ReleaseComObject(captureGraphBuilder);
                    captureGraphBuilder = null;
                }
            }
        }

        private void SaveSizeInfo(ISampleGrabber sampleGrabber)
        {
            AMMediaType mediaType = new AMMediaType();
            int hr = sampleGrabber.GetConnectedMediaType(mediaType);
            DsError.ThrowExceptionForHR(hr);

            if ((mediaType.formatType != FormatType.VideoInfo) || (mediaType.formatPtr == IntPtr.Zero))
            {
                throw new NotSupportedException("Unknown Grabber Media Format");
            }

            // Grab the size info
            VideoInfoHeader videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(mediaType.formatPtr, typeof(VideoInfoHeader));
            Width = videoInfoHeader.BmiHeader.Width;
            Height = videoInfoHeader.BmiHeader.Height;
            Stride = Width * (videoInfoHeader.BmiHeader.BitCount / 8);

            DsUtils.FreeAMMediaType(mediaType);
        }

        private void ConfigureSampleGrabber(ISampleGrabber sampGrabber)
        {
            // Set the media type to Video/RBG24.
            AMMediaType mediaType = new AMMediaType();
            mediaType.majorType = MediaType.Video;
            mediaType.subType = MediaSubType.RGB24;
            mediaType.formatType = FormatType.VideoInfo;
            int hr = sampGrabber.SetMediaType(mediaType);
            DsError.ThrowExceptionForHR(hr);

            DsUtils.FreeAMMediaType(mediaType);
            mediaType = null;

            hr = sampGrabber.SetCallback(this, 1);
            DsError.ThrowExceptionForHR(hr);
        }

        private void SetConfigParameters(ICaptureGraphBuilder2 captureGraphBuilder, IBaseFilter captureFilter, int frameRate, int width, int height)
        {
            object outObject;
            int hr = captureGraphBuilder.FindInterface(PinCategory.Capture, MediaType.Video, captureFilter, typeof(IAMStreamConfig).GUID, out outObject);

            IAMStreamConfig videoStreamConfig = outObject as IAMStreamConfig;
            if (videoStreamConfig == null)
            {
                throw new Exception("Failed to get IAMStreamConfig");
            }

            AMMediaType outMedia;
            hr = videoStreamConfig.GetFormat(out outMedia);
            DsError.ThrowExceptionForHR(hr);

            VideoInfoHeader videoInfoHeader = new VideoInfoHeader();
            Marshal.PtrToStructure(outMedia.formatPtr, videoInfoHeader);

            videoInfoHeader.AvgTimePerFrame = 10000000 / frameRate;
            videoInfoHeader.BmiHeader.Width = width;
            videoInfoHeader.BmiHeader.Height = height;

            Marshal.StructureToPtr(videoInfoHeader, outMedia.formatPtr, false);

            hr = videoStreamConfig.SetFormat(outMedia);
            DsError.ThrowExceptionForHR(hr);


            DsUtils.FreeAMMediaType(outMedia);
            outMedia = null;

        }
        #endregion

        #region Callbacks
        /// <summary> sample callback, NOT USED. </summary>
        int ISampleGrabberCB.SampleCB(double sampleTime, IMediaSample sample)
        {
            if (!hasPicture)
            {
                hasPicture = true;
                IntPtr bufferPointer;

                sample.GetPointer(out bufferPointer);
                int iBufferLen = sample.GetSize();
                if (sample.GetSize() > Stride * Height)
                {
                    throw new Exception("Buffer is wrong size");
                }

                CopyMemory(imageHandle, bufferPointer, Stride * Height);

                pictureReady.Set();
            }

            Marshal.ReleaseComObject(sample);
            return 0;
        }

        /// <summary> buffer callback, COULD BE FROM FOREIGN THREAD. </summary>
        int ISampleGrabberCB.BufferCB(double sampleTime, IntPtr bufferPointer, int bufferLength)
        {
            if (!hasPicture)
            {
                if (bufferLength <= Stride * Height)
                {
                    CopyMemory(imageHandle, bufferPointer, Stride * Height);
                }
                else
                {
                    throw new Exception("Buffer is wrong size");
                }
                hasPicture = true;
                pictureReady.Set();
            }
            else
            {
                Dropped++;
            }
            return 0;
        }
        #endregion

        #region Static public methods
        public static List<string> GetDeviceNames()
        {
            List<string> deviceNames = new List<string>();
            DsDevice[] captureDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            foreach (DsDevice captureDevice in captureDevices)
            {
                deviceNames.Add(captureDevice.Name);
            }
            return deviceNames;
        }

   /*     public static List<List<int>> GetAvailableResolutions(int deviceIndex)
        {
            DsDevice[] captureDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            List<List<int>> resolutionList = CaptureDevice.GetAllAvailableResolution(captureDevices[deviceIndex]);
            return resolutionList;
        }  */
        #endregion
    }
}

