using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;

namespace InternetDataAcquisitionLibrary
{
    public class InternetItem
    {
        public const string DATE_TIME_FORMAT = "yyyyMMdd HH:mm:ss";

        public List<string> TagList { get; set; }
        public string Content { get; set; }

        public InternetItem()
        {
            TagList = new List<string>();
        }

        public static InternetItem GenerateFromSyndicationItem(SyndicationItem syndicationItem, List<string> rssDownloaderTopicList)
        {
            InternetItem internetItem = new InternetItem();
            internetItem.TagList.Add("RSS");
            foreach (string rssDownloaderTopic in rssDownloaderTopicList) { internetItem.TagList.Add(rssDownloaderTopic); }

            string cleanedTitle = syndicationItem.Title.Text.Replace('_', '-');
            string cleanedSummary = syndicationItem.Summary.Text.Replace('_', '-');

            internetItem.Content = "Title = " + cleanedTitle + "|" + "Summary = " + cleanedSummary + "|" +
                "PublishDate = " + syndicationItem.PublishDate.ToString(DATE_TIME_FORMAT) + "|";
            if (syndicationItem.Id != null)
            {
                string cleanedID = syndicationItem.Id.Replace('_', '-');
                internetItem.Content += "Id = " + cleanedID;
            }
            else { internetItem.Content += "Id = null"; }
            return internetItem;
        }
    }
}
