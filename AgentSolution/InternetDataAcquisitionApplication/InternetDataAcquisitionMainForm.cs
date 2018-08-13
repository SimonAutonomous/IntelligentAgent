using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using AgentLibrary;
using AgentLibrary.Memories;
using CommunicationLibrary;
using CustomUserControlsLibrary;
using InternetDataAcquisitionLibrary;

namespace InternetDataAcquisitionApplication
{
    public partial class InternetDataAcquisitionMainForm : Form
    {
        private const string CLIENT_NAME = "Internet";
        private const string DEFAULT_IP_ADDRESS = "127.0.0.1";
        private const int DEFAULT_PORT = 7;
        private const string DATETIME_FORMAT = "yyyyMMdd HH:mm:ss";

        private const string WIKIPEDIA_BASE_URL = "http://en.wikipedia.org/w/api.php?format=xml&action=query&prop=extracts&titles=";
        private const string WIKIPEDIA_SUFFIX = "&redirects=true";

        private const int MARGIN = 10;

        private string ipAddress = DEFAULT_IP_ADDRESS;
        private int port = DEFAULT_PORT;
        private Client client = null;

        private List<RSSDownloader> rssDownloaderList = null;
        private List<InternetItem> itemList = null;

        public InternetDataAcquisitionMainForm()
        {
            InitializeComponent();
            Initialize();
            Connect();
        }

        private void Initialize()
        {
            Size screenSize = Screen.GetBounds(this).Size;
            this.Location = new Point(screenSize.Width - this.Width, screenSize.Height - this.Height);
            mainTabControl.SelectedTab = communicationLogTabPage;
        }

        private void Connect()
        {
            client = new Client();
            client.Received += new EventHandler<DataPacketEventArgs>(HandleClientReceived);
            client.Progress += new EventHandler<CommunicationProgressEventArgs>(HandleClientProgress);
            client.Name = CLIENT_NAME;
            client.Connect(ipAddress, port);
        }

        private void HandleClientProgress(object sender, CommunicationProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => ShowProgress(e)));
            }
            else { ShowProgress(e); }
        }

        private void ShowProgress(CommunicationProgressEventArgs e)
        {
            ColorListBoxItem item;
            item = new ColorListBoxItem(e.Message, communicationLogColorListBox.BackColor, communicationLogColorListBox.ForeColor);
            communicationLogColorListBox.Items.Insert(0, item);
        }

        private Boolean ProcessRSSRequest(string url, List<string> topicList, string customDateTimeFormatString, RSSAction rssAction)
        {
            if (rssAction == RSSAction.Start)
            {
                if (rssDownloaderList == null) { rssDownloaderList = new List<RSSDownloader>(); }
                Boolean alreadyAvailable = false;
                RSSDownloader availableRSSDownloader = null;
                foreach (RSSDownloader rssDownloader in rssDownloaderList)
                {
                    if (rssDownloader.Url == url)
                    {
                        alreadyAvailable = true;
                        availableRSSDownloader = rssDownloader;
                        break;
                    }
                }
                if (alreadyAvailable)
                {
                    if (!availableRSSDownloader.Running)
                    {
                        availableRSSDownloader.Start();
                    }
                }
                else
                {
                    RSSDownloader rssDownloader = new RSSDownloader(url);
                    rssDownloader.TopicList = topicList;
                    if (customDateTimeFormatString != null)
                    {
                        rssDownloader.SetCustomDateTimeFormat(customDateTimeFormatString);
                    }
                    rssDownloader.NewItemsFound += new EventHandler(HandleNewRSSItemsFound);
                    rssDownloader.Start();
                    rssDownloaderList.Add(rssDownloader);
                }
            }
            else if (rssAction == RSSAction.Stop)
            {
                if (rssDownloaderList != null)
                {
                    RSSDownloader rssDownloader = rssDownloaderList.Find(d => d.Url == url);
                    if (rssDownloader != null)
                    {
                        rssDownloader.Stop();
                    }
                }
            }
            return true;  // The Boolean parameter is not currently used, but is retained for possible future use (e.g. error-checking)
        }

        private void ShowNewRSSItems(List<SyndicationItem> newRSSItemList)
        {
            int index = 0;
            foreach (SyndicationItem newRSSItem in newRSSItemList)
            {
                ColorListBoxItem item = new ColorListBoxItem(newRSSItem.Title.Text,
                    acquiredDataColorListBox.BackColor, acquiredDataColorListBox.ForeColor);
                acquiredDataColorListBox.Items.Insert(index, item);
                index++;
            }
        }

        private void ThreadSafeShowNewRSSItems(List<SyndicationItem> newRSSItemList)
        {
            if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => ShowNewRSSItems(newRSSItemList))); }
            else { ShowNewRSSItems(newRSSItemList); }
        }

        private void ThreadSafeShowSearchResult(string searchResult)
        {
            ColorListBoxItem item = new ColorListBoxItem(searchResult,
                                          acquiredDataColorListBox.BackColor, acquiredDataColorListBox.ForeColor);
            if (InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => acquiredDataColorListBox.Items.Insert(0, item)));
            }
            else { acquiredDataColorListBox.Items.Insert(0, item); }
        }

        private void HandleNewRSSItems(List<SyndicationItem> newRSSItemList, List<string> rssDownloaderTopicList)
        {
            if (newRSSItemList != null)
            {
                if (itemList == null) { itemList = new List<InternetItem>(); }
                int index = 0;
                List<InternetItem> sendList = new List<InternetItem>();
                foreach (SyndicationItem newItem in newRSSItemList)
                {
                    // Convert the SyndicationItem to the more generic InternetItem class used here:
                    InternetItem internetItem = InternetItem.GenerateFromSyndicationItem(newItem, rssDownloaderTopicList);
                    if (client != null)
                    {
                        if (client.Connected)
                        {
                            sendList.Add(internetItem);
                        }
                    }
                    itemList.Insert(index, internetItem);
                    index++;
                }
                SendRSSItem(sendList);
            }
        }

        private void SendRSSItem(List<InternetItem> sendList)
        {
            foreach (InternetItem internetItem in sendList)
            {
                string message = "[" + AgentConstants.WORKING_MEMORY_NAME + "]";
                message += "[{";
                foreach (string tag in internetItem.TagList)
                {
                    message += tag + ",";
                }
                message = message.TrimEnd(new char[] { ',' });
                message += "}]";
                message += "[" + internetItem.Content + "]";
                client.Send(message);
                Thread.Sleep(100);
            }
        }

        private void HandleNewRSSItemsFound(object sender, EventArgs e)
        {
            RSSDownloader rssDownloader = (RSSDownloader)sender;
            List<SyndicationItem> newItemList = rssDownloader.GetNewItems();
            if (newItemList != null)
            {
                HandleNewRSSItems(newItemList, rssDownloader.TopicList);
                ThreadSafeShowNewRSSItems(newItemList);
            }
        }

        private Boolean ProcessRequest(string searchRequestString)
        {
            List<string> requestSplit = searchRequestString.Split(new char[] { AgentConstants.INTERNET_SEARCH_REQUEST_SEPARATOR_CHARACTER },
                StringSplitOptions.RemoveEmptyEntries).ToList();
            if (requestSplit[0].ToUpper() == "RSS")
            {
                if (requestSplit.Count == 5)
                {
                    RSSAction rssAction;
                    try
                    {
                        rssAction = (RSSAction)Enum.Parse(typeof(RSSAction), requestSplit[1]);
                    }
                    catch
                    {
                        return false; 
                    }
                    string topicString = requestSplit[2];
                    List<string> topicList = topicString.Split(new char[] { AgentConstants.INTERNET_SEARCH_REQUEST_LIST_LEFT_CHARACTER,
                            AgentConstants.INTERNET_SEARCH_REQUEST_LIST_RIGHT_CHARACTER, AgentConstants.INTERNET_SEARCH_REQUEST_LIST_SEPARATOR_CHARACTER}, 
                            StringSplitOptions.RemoveEmptyEntries).
                     ToList();
                    string url = requestSplit[3];
                    string customDateTimeFormatString = requestSplit[4];
                    Boolean rssOK = ProcessRSSRequest(url, topicList, customDateTimeFormatString, rssAction);
                    return rssOK;
                }
                else { return false; }
            }
            else if (requestSplit[0].ToUpper().TrimEnd(new char[] { ' ' }) == "WIKI") 
            {
                if (requestSplit.Count == 3)
                {
                    string category = requestSplit[1].Replace(" ", "");
                    string rawRequestString = requestSplit[2].TrimStart(new char[] { ' ' }).TrimEnd(new char[] { });
                    List<string> requestStringSplit = rawRequestString.Split(new char[] { ' ' }, 
                        StringSplitOptions.RemoveEmptyEntries).ToList();
                    string requestString = "";
                    foreach (string requestItem in requestStringSplit)
                    {
                        requestString += requestItem[0].ToString().ToUpper() + requestItem.Remove(0, 1) + "_";
                    }
                    requestString = requestString.TrimEnd(new char[] { '_' });
                    Boolean wikiOK = ProcessWikipediaRequest(category, requestString);
                    if (!wikiOK) { return false; }
                }
                else { return false; }
            }
            else // To be written: Processing other requests than RSS requests and Wikipedia searches
            {
                return false;
            }
            return true;
        }

        private string ParseWikipediaText(string rawText)
        {
            byte[] bytes = Encoding.Default.GetBytes(rawText);
            string text = Encoding.UTF8.GetString(bytes);
            string newText = "";
            int index = 0;
            while (index < text.Length)
            { 
                if (text[index] == '<')
                {
                    while (text[index] != '>') { index++; }
                    index++;
                    if (index >= text.Length) { break; }
                }
                if (text[index] != '<')
                {
                    if (text[index] != '?')  // Sligthly ugly hack to removed special characters
                    {
                        newText += text[index];
                    }
                    index++;
                }
            }
            return newText;
        }

        private Boolean ProcessWikipediaRequest(string category, string requestString)
        {
            WebClient webClient = new WebClient();
            string url = WIKIPEDIA_BASE_URL + requestString + WIKIPEDIA_SUFFIX;
            var pageSource = webClient.DownloadString(url);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(pageSource);
            var node = xmlDocument.GetElementsByTagName("extract")[0]; // A bit ugly, but OK
            if (node != null)  // node will be null if the page does not exist.
            {
                try
                {
                    string text = node.InnerText;
                    string processedText = ParseWikipediaText(text);
                    List<string> sentenceList = processedText.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    //       ShowSearchString(sentenceList[0]);  // Send only the first sentence.
                    sentenceList[0] = sentenceList[0].Replace('\u2013', ',');  // replaces long dash by ,
                    sentenceList[0] = sentenceList[0].Replace('\u2014', ',');  // replaces long dash by ,
                    char[] retainedCharacters = sentenceList[0].Where(c => (char.IsLetterOrDigit(c) ||
                                     char.IsWhiteSpace(c) ||
                                    c == '-') || c == ',' || c == ';' || c == '.').ToArray();
                    string messageContent = new string(retainedCharacters);


                    // Send only the first sentence:
                    string message = "[" + AgentConstants.LONG_TERM_MEMORY_NAME + "]";
                    message += "[{";
                    message += category;
                    message += "}]";
                    string name = "";
                    List<string> nameSplit = requestString.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string nameSplitItem in nameSplit)
                    {
                        name += nameSplitItem + " ";
                    }
                    name = name.TrimEnd(new char[] { ' ' });
                    message += "[name = " + name + AgentConstants.MEMORY_ITEM_SEPARATION_CHARACTER + "description = " + messageContent + "]";
                    client.Send(message);
                    ThreadSafeShowSearchResult(message);
                    return true;
                }
                catch { return false; }
            }
            else
            {
                return false;
            }
        }

        private void HandleClientReceived(object sender, DataPacketEventArgs e)
        {
            string searchRequestString = e.DataPacket.Message;
            Boolean ok = ProcessRequest(searchRequestString);
            if (ok)
            {
                if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => ShowSearchString(searchRequestString))); }
                else { ShowSearchString(searchRequestString); }
            }
            else
            {

            }
        }

        private void ShowSearchString(string searchRequestString)
        {
            Color backColor = searchRequestsColorListBox.BackColor;
            Color foreColor = searchRequestsColorListBox.ForeColor;
            string nowAsString = DateTime.Now.ToString(DATETIME_FORMAT);
            ColorListBoxItem requestItem = new ColorListBoxItem(nowAsString + ": " + searchRequestString, backColor, foreColor);
            searchRequestsColorListBox.Items.Insert(0, requestItem);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rssDownloaderList != null)
            {
                foreach (RSSDownloader rssDownloader in rssDownloaderList)
                {
                    rssDownloader.Stop();
                }
            }
            Application.Exit();
        }

        // Ugly hard-coding here. Just a test!
        private void startRSSReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //     string requestString = "RSS|Start|{News,World}|http://feeds.bbci.co.uk/news/world/rss.xml"+"|ddd, dd MMM yyyy HH:mm:ss 'GMT'";
          //     string requestString = "RSS|Start|{News,World}|http://feeds.foxnews.com/foxnews/world?format=xml" + " |ddd, dd MMM yyyy HH:mm:ss 'GMT'";
            string requestString = "RSS|Start|{News,World}|https://www.cnbc.com/id/100003114/device/rss/rss.html" + "| ddd, dd MMM yyyy HH:mm 'GMT'";
            ProcessRequest(requestString);
        }

    /*    private void sendWikipediaRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessRequest("wiki | object| banana");
        }  */

        private void searchRequestToolStrip_Resize(object sender, EventArgs e)
        {
            searchRequestTextBox.Width = searchRequestToolStrip.Width - searchRequestTextBox.Bounds.Left - MARGIN;
        }

        private void InternetDataAcquisitionMainForm_Load(object sender, EventArgs e)
        {
            searchRequestToolStrip_Resize(this, e);
        }

        private void sendSearchRequestButton_Click(object sender, EventArgs e)
        {
            string searchRequestString = searchRequestTextBox.Text;
            Boolean ok = ProcessRequest(searchRequestString);
            if (ok)
            {
                ShowSearchString(searchRequestString);
            }
            else
            {
                ShowSearchString("Invalid search: " + searchRequestString);
            }
        }

        private void InternetDataAcquisitionMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rssDownloaderList != null)
            {
                foreach (RSSDownloader rssDownloader in rssDownloaderList)
                {
                    rssDownloader.Stop();
                }
            }
            Application.Exit();
        }
    }
}
