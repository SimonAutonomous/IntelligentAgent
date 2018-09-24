using AgentLibrary;
using AgentLibrary.DialogueItems;
using AgentLibrary.Memories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgentApplication.AddedClasses
{
    [DataContract]
    public class MovieInformationItem : DialogueItem
    {
        private List<string> inputQueryTagList;
        private string successTargetContext;
        private string successTargetID;
        private string failureTargetContext;
        private string failureTargetID;
        private string outputQueryTag;

        public MovieInformationItem() { }

        public MovieInformationItem(string id, List<string> inputQueryTagList, string outputQueryTag, string successTargetContext, string successTargetID, string failureTargetContext, string failureTargetID)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
            this.successTargetContext = successTargetContext;
            this.successTargetID = successTargetID;
            this.failureTargetContext = failureTargetContext;
            this.failureTargetID = failureTargetID;
            this.outputQueryTag = outputQueryTag;
        }

        public override void Initialize(Agent ownerAgent)
        {
            base.Initialize(ownerAgent);
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);

            //string queryTag = inputQueryTagList[0]; //TODO maybe not needed or change GetLastStringByTag
            string movieTitle = "";
            string movieInformationString = "";

            MemoryItem itemSought1 = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            DateTime timeToCheckFor = itemSought1.InsertionTime; // Movie title consists of at least one word --> if the insertion time of the other query items is the same concat name
            if (itemSought1 != null)
            {
                movieTitle = (string)itemSought1.GetContent();
            }
            MemoryItem itemSought2 = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[1]);
            if (itemSought2 != null)
            {
                DateTime insertionTime2 = itemSought2.InsertionTime;
                if (insertionTime2 == timeToCheckFor)
                {
                    movieTitle = movieTitle + " " + (string)itemSought2.GetContent();
                }
            }
            MemoryItem itemSought3 = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[2]);
            if (itemSought3 != null)
            {
                DateTime insertionTime3 = itemSought3.InsertionTime;
                if (insertionTime3 == timeToCheckFor)
                {
                    movieTitle = movieTitle + " " + (string)itemSought3.GetContent();
                }
            }

            var _ultraManager = UltraManager.Instance;
            Boolean existingMovie = false;
            foreach (var movie in _ultraManager.MovieList)
            {
                if (movie.Title.ToLower() == movieTitle.ToLower())
                {
                    existingMovie = true;
                    movieInformationString = movie.Title + " is a " + movie.Genre + " movie from " + movie.Year + " that has a imdb rating of " + movie.ImdbRating;
                }
            }

            if (existingMovie)
            {
                ownerAgent.SendSpeechOutput(movieInformationString);
                targetContext = successTargetContext;
                targetID = successTargetID;
            }
            else
            {
                targetContext = failureTargetContext;
                targetID = failureTargetID;

                StringMemoryItem imdbSearchMemoryItem = new StringMemoryItem();
                imdbSearchMemoryItem.TagList = new List<string>() { outputQueryTag };
                imdbSearchMemoryItem.SetContent(movieTitle);
                ownerAgent.WorkingMemory.AddItem(imdbSearchMemoryItem);
            }
            return true;
        }

        [DataMember]
        public List<string> InputQueryTagList
        {
            get { return inputQueryTagList; }
            set { inputQueryTagList = value; }
        }
    }
}
