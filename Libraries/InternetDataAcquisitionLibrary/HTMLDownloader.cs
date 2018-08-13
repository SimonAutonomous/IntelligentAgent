using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace InternetDataAcquisitionLibrary
{
    public class HTMLDownloader
    {
        private const int DEFAULT_CAPACITY = 10;
        private const int DEFAULT_MILLISECOND_DOWNLOAD_INTERVAL = 2000;
        private const int MILLISECONDS_PER_SECOND = 1000;

        private string url;
        private int capacity = DEFAULT_CAPACITY;
        private List<Tuple<DateTime, string>> dateHtmlList = null;
        private static object lockObject = new object();
        private Boolean storeOnlyIfDifferent = false;
        private int millisecondDownloadInterval = DEFAULT_MILLISECOND_DOWNLOAD_INTERVAL;
        private Boolean running = false;
        private Boolean runRepeatedly = false;
        private Thread downloadThread = null;

        public event EventHandler NewDataAvailable = null;
        public event EventHandler<ErrorEventArgs> Error = null;

        private void OnNewDataAvailable()
        {
            if (NewDataAvailable != null)
            {
                EventHandler handler = NewDataAvailable;
                handler(this, EventArgs.Empty);
            }
        }

        private void OnError(string errorString)
        {
            if (Error != null)
            {
                EventHandler<ErrorEventArgs> handler = Error;
                ErrorEventArgs e = new ErrorEventArgs(errorString);
                handler(this, e);
            }
        }

        private Boolean StoreData(DateTime dateTime, string html)
        {
            Boolean changed = true;
            Monitor.Enter(lockObject);
            if (dateHtmlList == null) { dateHtmlList = new List<Tuple<DateTime, string>>(); }
            if (!storeOnlyIfDifferent) { dateHtmlList.Add(new Tuple<DateTime, string>(dateTime, html)); }
            else
            {
                if (dateHtmlList.Count == 0) { dateHtmlList.Add(new Tuple<DateTime, string>(dateTime, html)); }
                else
                {
                    string previousLastString = dateHtmlList.Last().Item2;
                    if (previousLastString != html) { dateHtmlList.Add(new Tuple<DateTime, string>(dateTime, html)); }
                    else { changed = false; }
                }
            }
            if (dateHtmlList.Count > capacity) { dateHtmlList.RemoveAt(0); }
            Monitor.Exit(lockObject);
            return changed;
        }

        private void DownLoadLoop()
        {
            while (running)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                using (WebClient webClient = new WebClient())
                {
                    try
                    {
                        string html = webClient.DownloadString(url);
                        DateTime dateTime = DateTime.Now;
                        Boolean newDataStored = StoreData(dateTime, html);
                        if (newDataStored) { OnNewDataAvailable(); }
                    }
                    catch (WebException e)
                    {
                        running = false;
                        OnError(e.Status.ToString());
                    }
                }
                stopWatch.Stop();
                double elapsedSeconds = stopWatch.ElapsedTicks / (double)Stopwatch.Frequency;
                int elapsedMilliseconds = (int)Math.Round(elapsedSeconds * MILLISECONDS_PER_SECOND);
                int sleepInterval = millisecondDownloadInterval - elapsedMilliseconds;
                if (sleepInterval > 0) { Thread.Sleep(sleepInterval); }
                if (!runRepeatedly) { running = false; }
            }
        }

        public void Start(string url)
        {
            this.url = url;
            running = true;
            downloadThread = new Thread(new ThreadStart(() => DownLoadLoop()));
            downloadThread.Start();
        }

        public DateTime GetLastDateTime()
        {
            Monitor.Enter(lockObject);
            DateTime lastDateTime = dateHtmlList.Last().Item1;
            Monitor.Exit(lockObject);
            return lastDateTime;
        }

        public string GetLastString()
        {
            Monitor.Enter(lockObject);
            string lastString = dateHtmlList.Last().Item2;
            Monitor.Exit(lockObject);
            return lastString;
        }

        public Boolean StoreOnlyIfDifferent
        {
            get { return storeOnlyIfDifferent; }
            set { storeOnlyIfDifferent = value; }
        }

        public double DownloadInterval
        {
            get { return MILLISECONDS_PER_SECOND * millisecondDownloadInterval; }
            set
            {
                millisecondDownloadInterval = (int)Math.Round(value / MILLISECONDS_PER_SECOND);
            }
        }

        public Boolean RunRepeatedly
        {
            get { return runRepeatedly; }
            set { runRepeatedly = value; }
        }
    }
}
