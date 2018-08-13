using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CustomUserControlsLibrary;
using InternetDataAcquisitionLibrary;

namespace RSSReaderApplication
{
    public partial class MainForm : Form
    {
        private const int MILLISECONDS_PER_SECOND = 1000;

        private RSSDownloader rssDownloader = null;

        public MainForm()
        {
            InitializeComponent();
            rssItemsListBox.SelectedItemBackColor = Color.White;
            rssItemsListBox.SelectedItemForeColor = Color.Black;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rssDownloader != null) { rssDownloader.HardStop(); }
            Application.Exit();
        }

        private void ShowNewItems(List<SyndicationItem> newItemList)
        {
            newItemList.Reverse();
            foreach (ColorListBoxItem colorListBoxItem in rssItemsListBox.Items)
            {
                colorListBoxItem.ItemForeColor = Color.Gray;
            }
            foreach (SyndicationItem newItem in newItemList)
            {
                string itemText = newItem.PublishDate.ToString("yyyyMMdd HHmmss") + ": " + newItem.Title.Text;
                ColorListBoxItem colorListBoxItem = new ColorListBoxItem(itemText, rssItemsListBox.BackColor, Color.Lime);
                rssItemsListBox.Items.Insert(0, colorListBoxItem);
            }
        }

        private void HandleNewItemsFound(object sender, EventArgs e)
        {
            List<SyndicationItem> newItemList = rssDownloader.GetNewItems();
            if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => ShowNewItems(newItemList))); }
            else { ShowNewItems(newItemList); }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;

            rssItemsListBox.Items.Clear();
            // RSS reader thread
            string url = rssFeedURLTextBox.Text;
            string dateFormat = dateFormatTextBox.Text;
            rssDownloader = new RSSDownloader(url);
            rssDownloader.SetCustomDateTimeFormat(dateFormat);
            rssDownloader.DownloadInterval = double.Parse(downloadIntervalTextBox.Text);
            rssDownloader.NewItemsFound += new EventHandler(HandleNewItemsFound);
            rssDownloader.Start();
            stopButton.Enabled = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = false;
            rssDownloader.HardStop();
            startButton.Enabled = true;
        }
    }
}
