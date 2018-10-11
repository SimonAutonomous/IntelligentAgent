using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AgentLibrary.DialogueItems;
using AgentLibrary.Memories;

namespace AgentApplication.AddedClasses.DialogueItems
{
    [DataContract]
    public class UltraManagerRetrieveMovieItem : DialogueItem
    {
        private List<string> inputQueryTagList;
        private readonly UltraManager _ultraManager = UltraManager.Instance;

        private string targetContext;
        private string targetID;
        private string outputQueryTag;

        public UltraManagerRetrieveMovieItem() { }

        public UltraManagerRetrieveMovieItem(string id, List<string> inputQueryTagList, string outputQueryTag, string targetContext, string targetID)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
            this.targetContext = targetContext;
            this.targetID = targetID;
            this.outputQueryTag = outputQueryTag;
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);

            string movieTitle = "";
            string movieInformation = "";

            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought != null)
            {
                movieTitle = (string)itemSought.GetContent();
            }

            foreach (var movie in _ultraManager.MovieList)
            {
                if (movie.Title.ToLower() == movieTitle.ToLower())
                {
                    movieInformation = movie.Title + "/" + movie.Year + "/" + movie.ImdbRating + "/" + movie.Genre;
                }
            }

            StringMemoryItem movieTitleMemoryItem = new StringMemoryItem();
            movieTitleMemoryItem.TagList = new List<string>() { outputQueryTag };
            movieTitleMemoryItem.SetContent(movieInformation);
            ownerAgent.WorkingMemory.AddItem(movieTitleMemoryItem);

            targetContext = this.targetContext;
            targetID = this.targetID;

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
