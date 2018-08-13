using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Net;
using System.Runtime.Serialization;
using System.Threading;
// using System.Xml;

namespace InternetDataAcquisitionLibrary
{
    [DataContract]
    public class RSSDownloader
    {
        private const int DEFAULT_MILLISECOND_DOWNLOAD_INTERVAL = 2000;
        private const int MILLISECONDS_PER_SECOND = 1000;
        private const int DEFAULT_ACCESS_MILLISECOND_TIMEOUT = 50;

        private string url;
        private string customDateTimeFormat = null;
        private Thread runThread;
        private Boolean running = false;
        private int millisecondDownloadInterval = DEFAULT_MILLISECOND_DOWNLOAD_INTERVAL;
        private int accessMillisecondTimeout = DEFAULT_ACCESS_MILLISECOND_TIMEOUT;
        private List<SyndicationItem> newItemList = null; // Any new items downloaded in the last update
        private List<SyndicationItem> itemList = null;
        private DateTimeOffset mostRecentPublishDate = DateTimeOffset.MinValue;
        private List<string> topicList;
        private static object lockObject = new object();

        public event EventHandler NewItemsFound = null;

        public RSSDownloader(string url)
        {
            this.url = url;
            topicList = new List<string>();
            itemList = new List<SyndicationItem>();
        }

        private void OnNewItemsFound()
        {
            if (NewItemsFound != null)
            {
                EventHandler handler = NewItemsFound;
                handler(this, EventArgs.Empty);
            }
        }

        private void ProcessFeed(SyndicationFeed feed)
        {
            Monitor.Enter(lockObject);
            List<SyndicationItem> feedItemList = feed.Items.ToList();
            
            if (itemList == null)
            {
                itemList = new List<SyndicationItem>();
                mostRecentPublishDate = DateTimeOffset.MinValue;
            }
            newItemList = new List<SyndicationItem>();
            foreach (SyndicationItem feedItem in feedItemList)
            {
                if (feedItem.PublishDate > mostRecentPublishDate)
                {
                    newItemList.Add(feedItem);
                }
            }
            if (newItemList.Count > 0)
            {
                newItemList.Sort((a, b) => a.PublishDate.CompareTo(b.PublishDate));  
                newItemList.Reverse(); 
                foreach (SyndicationItem item in newItemList)
                {
                    itemList.Add(item);
                }
                itemList.Sort((a, b) => a.PublishDate.CompareTo(b.PublishDate));  // Not really needed ...
                itemList.Reverse(); // Newest (= "largest" dateTimeOffSet) first
                mostRecentPublishDate = itemList[0].PublishDate;
                OnNewItemsFound();
            }          
            Monitor.Exit(lockObject);
        }

        private void RunLoop()
        {
            while (running)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                using (CustomXmlReader xmlReader = new CustomXmlReader(url))
                {
                    xmlReader.SetCustomDateTimeFormat(customDateTimeFormat);
                    xmlReader.Read();
                    SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
                    ProcessFeed(feed);
                }
                stopWatch.Stop();
                double elapsedSeconds = stopWatch.ElapsedTicks / (double)Stopwatch.Frequency;
                int elapsedMilliseconds = (int)Math.Round(elapsedSeconds * MILLISECONDS_PER_SECOND);
                int sleepInterval = millisecondDownloadInterval - elapsedMilliseconds;
                if (sleepInterval > 0) { Thread.Sleep(sleepInterval); }
            }
        }

        public void Start()
        {
            running = true;
            runThread = new Thread(new ThreadStart(() => RunLoop()));
            runThread.Start();
        }

        public void Stop()
        {
            running = false;
        }

        public void HardStop()
        {
            running = false;
            runThread.Abort();
        }


        public void SetCustomDateTimeFormat(string customDateTimeFormat)
        {
            this.customDateTimeFormat = customDateTimeFormat;
        }

        public double DownloadInterval
        {
            get { return (millisecondDownloadInterval/MILLISECONDS_PER_SECOND); }
            set
            {
                millisecondDownloadInterval = (int)Math.Round(value * MILLISECONDS_PER_SECOND);
            }
        }

        public List<SyndicationItem> GetNewItems()
        {
            if (Monitor.TryEnter(lockObject, accessMillisecondTimeout))
            {
                List<SyndicationItem> accessedNewItemList = null;
                if (newItemList != null)
                {
                    accessedNewItemList = new List<SyndicationItem>();
                    foreach (SyndicationItem newItem in newItemList)
                    {
                        SyndicationItem copiedItem = newItem.Clone(); // OK here - all fields a copied correctly (deep copy)
                        accessedNewItemList.Add(copiedItem);
                    }
                }
                Monitor.Exit(lockObject);
                return accessedNewItemList;
            }
            else { return null; }
        }

        public List<SyndicationItem> TryGetAllItems()
        {
            if (Monitor.TryEnter(lockObject, accessMillisecondTimeout))
            {
                List<SyndicationItem> accessedItemList = null;
                if (itemList.Count > 0)
                {
                    accessedItemList = new List<SyndicationItem>();
                    foreach (SyndicationItem item in itemList)
                    {
                        SyndicationItem copiedItem = item.Clone(); // OK here - all fields a copied correctly (deep copy)
                        accessedItemList.Add(item);
                    }
                }
                Monitor.Exit(lockObject);
                return accessedItemList;
            }
            else { return null; }
        }

        public List<SyndicationItem> TryGetItems(DateTimeOffset lastPublishedDate)
        {
            if (Monitor.TryEnter(lockObject, accessMillisecondTimeout))
            {
                List<SyndicationItem> accessedItemList = null;
                if (itemList.Count > 0)
                {
                    accessedItemList = new List<SyndicationItem>();
                    int index = 0;
                    DateTimeOffset publishDate = itemList[index].PublishDate;
                    while (publishDate > lastPublishedDate)
                    {
                        SyndicationItem copiedItem = itemList[index].Clone(); // OK here - all fields a copied correctly (deep copy)
                        accessedItemList.Add(copiedItem);
                        index++;
                        if (index >= itemList.Count) { break; }
                        publishDate = itemList[index].PublishDate;
                    }
                }
                Monitor.Exit(lockObject);
                return accessedItemList;
            }
            else { return null; }
        }  

        public string Url
        {
            get { return url; }
        }

        public Boolean Running
        {
            get { return running; }
        }

        public List<string> TopicList
        {
            get { return topicList; }
            set { topicList = value; }
        }

        [DataMember]
        public int MillisecondDownloadInterval
        {
            get { return millisecondDownloadInterval; }
            set { millisecondDownloadInterval = value; }
        }

        [DataMember]
        public int AccessMillisecondTimeout
        {
            get { return accessMillisecondTimeout; }
            set { accessMillisecondTimeout = value; }
        }
    }
}
