using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Timers;
using System.Runtime.InteropServices;
using AuxiliaryLibrary;

namespace AudioLibrary
{
    #region Enumerations
    public enum MMResult : uint
    {
        MMSYSERR_NOERROR = 0,
        MMSYSERR_ERROR = 1,
        MMSYSERR_BADDEVICEID = 2,
        MMSYSERR_NOTENABLED = 3,
        MMSYSERR_ALLOCATED = 4,
        MMSYSERR_INVALHANDLE = 5,
        MMSYSERR_NODRIVER = 6,
        MMSYSERR_NOMEM = 7,
        MMSYSERR_NOTSUPPORTED = 8,
        MMSYSERR_BADERRNUM = 9,
        MMSYSERR_INVALFLAG = 10,
        MMSYSERR_INVALPARAM = 11,
        MMSYSERR_HANDLEBUSY = 12,
        MMSYSERR_INVALIDALIAS = 13,
        MMSYSERR_BADDB = 14,
        MMSYSERR_KEYNOTFOUND = 15,
        MMSYSERR_READERROR = 16,
        MMSYSERR_WRITEERROR = 17,
        MMSYSERR_DELETEERROR = 18,
        MMSYSERR_VALNOTFOUND = 19,
        MMSYSERR_NODRIVERCB = 20,
        WAVERR_BADFORMAT = 32,
        WAVERR_STILLPLAYING = 33,
        WAVERR_UNPREPARED = 34
    }
    #endregion

    #region Structs
    public struct WaveFormat
    {
        public short formatTag;
        public short channelCount;
        public int sampleRate;
        public int averageBytesPerSecond;
        public short blockAlignment;
        public short bitsPerSample;
        public short extraByteSize;
    }

    public struct WaveHeader
    {
        public IntPtr dataPointer;
        public uint dataLength;
        public int bytesRecorded;
        public IntPtr user;
        public int flags;
        public int loops;
        public IntPtr reservedNext;
        public IntPtr reserved;
    }

    public struct DeviceInfo
    {
        public short manufacturerId;
        public short productId;
        public int driverVersion;
        public int supportedFormats;
        public short supportedChannels;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string productName;
    }

    #endregion

    public class WAVRecorder
    {
        #region Delegates
        public delegate void AudioRecordingDelegate(IntPtr deviceHandle, uint message, IntPtr instance, ref WaveHeader wavehdr, IntPtr reserved2);
        #endregion

        #region Constants
        private const int DEFAULT_DEVICE_ID = 0;
        private const int DEFAULT_SAMPLE_RATE = 16000;
        private const int DEFAULT_NUMBER_OF_CHANNELS = 1;
        private const int DEFAULT_BLOCK_ALIGNMENT = 2;
        private const int DEFAULT_NUMBER_OF_BITS_PER_SAMPLE = 16;
        private const int CALLBACK_FUNCTION = 0x00030000;
        private const int MM_WIM_OPEN = 0x3BE;
        private const int MM_WIM_CLOSE = 0x3BF;
        private const int MM_WIM_DATA = 0x3C0;
        private const int DEFAULT_NUMBER_OF_SNIPPETS_PER_SECOND = 4; // 10;  // Determines the buffer size.
        private const int DEFAULT_ACCESS_MILLISECOND_TIMEOUT = 10;
        private const double DEFAULT_STORAGE_DURATION = 10.0;
        #endregion

        #region Fields
        private int deviceId;
        private AudioRecordingDelegate waveIn;
        private IntPtr waveHandle;
        private WaveHeader waveHeader;
        private WaveFormat waveFormat;
        private GCHandle headerPin;
        private GCHandle bufferPin;
        private byte[] buffer;
        private uint bufferLength;
        private int totalNumberOfBytesRecorded;
        private Boolean isRecording = false;
        private List<Tuple<int, DateTime, byte[]>> timeRecordingList;
        private static object lockObject = new object();
        private int recordingIndex = 0;
        private int numberOfSnippetsPerSecond = DEFAULT_NUMBER_OF_SNIPPETS_PER_SECOND;
        private int accessMillisecondTimeOut = DEFAULT_ACCESS_MILLISECOND_TIMEOUT;
        private double storageDuration = DEFAULT_STORAGE_DURATION;
        private int storageCount; // The NUMBER of snippets stored.
        #endregion

        #region Event handlers
     //   public event EventHandler<WAVRecorderEventArgs> SnippetRecorded = null;
     //   public event EventHandler RecordingStopped = null;
        #endregion

        #region External methods
        [DllImport("winmm.dll")]
        private static extern int waveInGetNumDevs();

        [DllImport("winmm.dll", EntryPoint = "waveInGetDevCaps")]
        private static extern int waveInGetDevCapsA(int deviceId, ref DeviceInfo deviceInfor, int size);

        [DllImport("winmm.dll")]
        private static extern int waveInOpen(ref IntPtr waveHandle, uint deviceId, ref WaveFormat waveFormat, IntPtr callback, uint instance, uint flags);

        [DllImport("winmm.dll")]
        private static extern uint waveInStart(IntPtr waveHandle);

        [DllImport("winmm.dll")]
        private static extern int waveInStop(IntPtr waveHandle);

        [DllImport("winmm.dll")]
        private static extern uint waveInReset(IntPtr waveHandle);

        [DllImport("winmm.dll")]
        private static extern uint waveInClose(IntPtr waveHandle);

        [DllImport("winmm.dll")]
        private static extern MMResult waveInPrepareHeader(IntPtr waveHandle, ref WaveHeader waveHeader, uint waveHeaderSize);

        [DllImport("winmm.dll")]
        private static extern MMResult waveInUnprepareHeader(IntPtr waveHandle, ref WaveHeader waveHeader, uint waveHeaderSize);

        [DllImport("winmm.dll", EntryPoint = "waveInAddBuffer", SetLastError = true)]
        private static extern uint waveInAddBuffer(IntPtr waveHandle, ref WaveHeader waveHeader, uint waveHeaderSize);
        #endregion

        #region Constructors
        public WAVRecorder()
        {
            List<string> deviceNames = WAVRecorder.GetDeviceNames();
            if (deviceNames.Count > 0)
            {
                deviceId = DEFAULT_DEVICE_ID;
            }
            InitializeWaveFormat();
            isRecording = false;
        }
        #endregion

        #region Private methods
        private void SetupBuffer()
        {
            waveHeader.dataPointer = bufferPin.AddrOfPinnedObject();
            waveHeader.dataLength= bufferLength;
            waveHeader.flags = 0;
            waveHeader.bytesRecorded = 0;
            waveHeader.loops = 0;
            waveHeader.user = IntPtr.Zero;
            waveHeader.reservedNext = IntPtr.Zero;
            waveHeader.reserved= IntPtr.Zero;

            waveInPrepareHeader(waveHandle, ref waveHeader, Convert.ToUInt32(Marshal.SizeOf(waveHeader)));
            waveInAddBuffer(waveHandle, ref waveHeader, Convert.ToUInt32(Marshal.SizeOf(waveHeader)));
        }

        private void InitializeWaveFormat()
        {
            waveFormat = new WaveFormat();
            waveFormat.formatTag = 1;
            waveFormat.channelCount = DEFAULT_NUMBER_OF_CHANNELS;
            waveFormat.sampleRate = DEFAULT_SAMPLE_RATE; // 16000; //  12000;
            waveFormat.blockAlignment = DEFAULT_BLOCK_ALIGNMENT;
            waveFormat.averageBytesPerSecond = waveFormat.sampleRate * waveFormat.blockAlignment; // 24000;
            waveFormat.bitsPerSample = DEFAULT_NUMBER_OF_BITS_PER_SAMPLE;
            waveFormat.extraByteSize = 0;
        }

        private void callbackWaveIn(IntPtr deviceHandle, uint message, IntPtr instance, ref WaveHeader waveHeader, IntPtr reserved2)
        {
            if (message == MM_WIM_DATA)
            {
                if (waveHeader.bytesRecorded > 0)
                {
                    int numberOfBytesRecorded = waveHeader.bytesRecorded;
                    if (numberOfBytesRecorded > 0)
                    {
                        Monitor.Enter(lockObject);  // This does not seem to help.. still get access violations when accessing the data.
                        byte[] newSoundData = new byte[numberOfBytesRecorded];
                        Marshal.Copy(waveHeader.dataPointer, newSoundData, 0, numberOfBytesRecorded);
                        totalNumberOfBytesRecorded += numberOfBytesRecorded;
                        timeRecordingList.Add(new Tuple<int, DateTime, byte[]>(recordingIndex, DateTime.Now, newSoundData));
                        if (timeRecordingList.Count > storageCount) { timeRecordingList.RemoveAt(0); }
                        recordingIndex++;
                        Monitor.Exit(lockObject);
                        GC.Collect();  // This IS ugly, but it appears to work ...
                    }
                }
                MMResult i = waveInUnprepareHeader(waveHandle, ref waveHeader, Convert.ToUInt32(Marshal.SizeOf(waveHeader)));
                if (i != MMResult.MMSYSERR_NOERROR)
                {
                }
                SetupBuffer();
            }
            else if (message == MM_WIM_CLOSE)
            {
            }
            else
            {
            }
        }

        // OLD methods (did not work with event handlers in connection with the unmanaged code...)
        /*
        private void GenerateSoundSnippet()
        {
            int numberOfBytesRecorded = waveHeader.bytesRecorded;
            if (numberOfBytesRecorded > 0)
            {
                byte[] newSoundData = new byte[numberOfBytesRecorded];
                Marshal.Copy(waveHeader.dataPointer, newSoundData, 0, numberOfBytesRecorded);
                totalNumberOfBytesRecorded += numberOfBytesRecorded;
                OnSnippetRecorded(newSoundData, totalNumberOfBytesRecorded);  
            }
        }

        private void OnSnippetRecorded(byte[] recordedBytes, int numberofBytes)
        {
            if (SnippetRecorded != null)
            {
                WAVRecorderEventArgs e = new WAVRecorderEventArgs(recordedBytes, numberofBytes);
                EventHandler<WAVRecorderEventArgs> handler = SnippetRecorded;
                handler(this, e);
            }
        }

        private void OnRecordingStopped()
        {
            if (RecordingStopped != null)
            {
                EventHandler handler = RecordingStopped;
                handler(this, EventArgs.Empty);
            }
        }  */

        #endregion 

        #region Public methods
        public void Start()
        {
            recordingIndex = 0;
            timeRecordingList = new List<Tuple<int,DateTime, byte[]>>();
            storageCount = (int)Math.Round(storageDuration * numberOfSnippetsPerSecond);
            isRecording = true;
            waveIn = new AudioRecordingDelegate(callbackWaveIn);
            waveHandle = new IntPtr();
            InitializeWaveFormat();
            bufferLength = (uint)(waveFormat.averageBytesPerSecond / numberOfSnippetsPerSecond);  
            headerPin = GCHandle.Alloc(waveHeader, GCHandleType.Pinned);
            buffer = new byte[bufferLength];
            bufferPin = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            waveInOpen(ref waveHandle, (uint)deviceId, ref waveFormat, Marshal.GetFunctionPointerForDelegate(waveIn), (uint)0, (uint)CALLBACK_FUNCTION);
            SetupBuffer();
            waveInStart(waveHandle);
        }

        public void Stop()
        {
            int i = waveInStop(waveHandle);
            if (i != 0)
            {
            }
            isRecording = false;
            // More needed here?
        }

     /*   public List<byte[]> GetAllData()
        {
            List<byte[]> allDataList = new List<byte[]>();
            for (int ii = 0; ii < timeRecordingList.Count; ii++)
            {
                int copiedDataLength = timeRecordingList[ii].Item3.Length;
                byte[] copiedData = new byte[copiedDataLength];
                Array.Copy(timeRecordingList[ii].Item3, copiedData, copiedDataLength);
                allDataList.Add(copiedData);
            }
            return allDataList;
        }  */

        public byte[] GetAllRecordedBytes()
        {
            if (Monitor.TryEnter(lockObject, accessMillisecondTimeOut))
            {
                if (timeRecordingList.Count > 0)
                {
                    Tuple<int, DateTime, byte[]> soundData = GetSoundData(timeRecordingList[0].Item1);
                    Monitor.Exit(lockObject);
                    return soundData.Item3;
                }
                else
                {
                    Monitor.Exit(lockObject);
                    return null;
                }
            }
            else { return null; }
        }

        public byte[] GetAllRecordedBytes(out DateTime startTime, out DateTime endTime)
        {
            startTime = DateTime.MinValue;
            endTime = DateTime.MinValue;
            if (Monitor.TryEnter(lockObject, accessMillisecondTimeOut))
            {
                if (timeRecordingList.Count > 0)
                {
                    Tuple<int, DateTime, byte[]> soundData = GetSoundData(timeRecordingList[0].Item1);
                    Monitor.Exit(lockObject);
                    startTime = timeRecordingList[0].Item2;
                    endTime = timeRecordingList.Last().Item2;
                    return soundData.Item3;
                }
                else
                {
                    Monitor.Exit(lockObject);
                    return null;
                }
            }
            else { return null; }
        }

        public Tuple<int, DateTime, byte[]> GetSoundData(int startID)
        {
            if (Monitor.TryEnter(lockObject, accessMillisecondTimeOut))
            {
                if (timeRecordingList.Count > 0)
                {
                    if (startID >= timeRecordingList.First().Item1)
                    {
                        int numberOfBytes = 0;
                        int iStart = timeRecordingList.FindIndex(i => i.Item1 == startID);  // perhaps loop backward from last instead? (Faster).
                        for (int ii = iStart; ii < timeRecordingList.Count; ii++) { numberOfBytes += timeRecordingList[ii].Item3.Length; }
                        byte[] accessedSoundData = new byte[numberOfBytes];
                        int currentIndex = 0;
                        for (int ii = iStart; ii < timeRecordingList.Count; ii++)
                        {
                            int dataLength = timeRecordingList[ii].Item3.Length;
                            Array.Copy(timeRecordingList[ii].Item3, 0, accessedSoundData, currentIndex, dataLength);
                            currentIndex += dataLength;
                        }
                        Monitor.Exit(lockObject);
                        return new Tuple<int, DateTime, byte[]>(timeRecordingList.Last().Item1, timeRecordingList.Last().Item2, accessedSoundData);
                    }
                    else
                    {
                        Monitor.Exit(lockObject);
                        return null;
                    }
                }
                else
                {
                    Monitor.Exit(lockObject);
                    return null;
                }
            }
            else { return null; }
        }
        #endregion 

        #region Public static methods
        public static List<string> GetDeviceNames()
        {
            List<string> deviceNames = new List<string>();
            int deviceCount = waveInGetNumDevs();
            for (int i = 0; i < deviceCount; i++)
            {
                DeviceInfo deviceInfo = new DeviceInfo();
                waveInGetDevCapsA(i, ref deviceInfo, (int)Marshal.SizeOf(deviceInfo));
                deviceNames.Add(deviceInfo.productName);
            }
            return deviceNames;
        }
        #endregion 

        #region Properties
        public int DeviceId
        {
            get { return deviceId; }
            set
            {
                if (!isRecording) { deviceId = value; }
            }
        }

        public int SampleRate
        {
            get { return waveFormat.sampleRate; }
            set
            {
                if (!isRecording) { waveFormat.sampleRate = value; }
            }
        }

        public short NumberOfChannels
        {
            get { return waveFormat.channelCount; }
        }

        public short NumberOfBitsPerSample
        {
            get { return waveFormat.bitsPerSample; }
        }

        public double StorageDuration
        {
            get { return storageDuration; }
            set { storageDuration = value; }
        }


        public Boolean IsRecording
        {
            get { return isRecording; }
        }
        #endregion
    }
}
