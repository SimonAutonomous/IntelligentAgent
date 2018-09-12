using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AgentLibrary.Memories;
using AgentLibrary.DialogueItems;
using AgentLibrary;

namespace AgentApplication.AddedClasses //TODO: right namespace?
{
    [DataContract]
    public class TasteProfilingItem : DialogueItem
    {
        //private OutputAction outputAction = new OutputAction();
        private List<string> inputQueryTagList;
        private readonly UltraManager _ultraManager = UltraManager.Instance;

        private string successTargetContext;
        private string successTargetID;
        private string failureTargetContext;
        private string failureTargetID;

        private int maximumRepetitionCount;
        private string outputQueryTag;

        public TasteProfilingItem() { }

        public TasteProfilingItem(string id, List<string> inputQueryTagList, int maximumRepetitionCount, string outputQueryTag, string successTargetContext,
            string successTargetID, string failureTargetContext, string failureTargetID)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
            this.successTargetContext = successTargetContext;
            this.successTargetID = successTargetID;
            this.failureTargetContext = failureTargetContext;
            this.failureTargetID = failureTargetID;
            this.maximumRepetitionCount = maximumRepetitionCount;
            this.outputQueryTag = outputQueryTag;
        }

        /* public override void Initialize(Agent ownerAgent) // TODO use pattern list
         {
             base.Initialize(ownerAgent);
             //foreach (Pattern pattern in outputAction.PatternList)
             //{
             //    pattern.ProcessDefinition();
             //    //     pattern.ProcessDefinitionList();
             //}
         }*/

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);


            string currentUser = "";
            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought != null)  // 20171201
            {
                currentUser = (string)itemSought.GetContent();
            }

            List<string> openRatings = new List<string> { };
            foreach (var movie in _ultraManager.MovieList)
            {
                Boolean addItem = true;
                //movie.Title ==
                foreach (var rating in _ultraManager.RatingList)
                {
                    if (rating.UserName == currentUser && rating.MovieTitle == movie.Title)
                    {
                        addItem = false;
                    }
                }
                if (addItem)
                {
                    openRatings.Add(movie.Title);
                }
            }

            // return random unseen movie to rate as outputQueryTag
            Boolean noMoreRatngs = false;
            if(openRatings.Count > 0)
            {
                // Random random = new Random();
                // int randomIndex = random.Next(0, openRatings.Count);
                int randomIndex = ownerAgent.RandomNumberGenerator.Next(0, openRatings.Count);
                string openRating = openRatings[randomIndex];
                StringMemoryItem rateMemoryItem = new StringMemoryItem();
                rateMemoryItem.TagList = new List<string>() { outputQueryTag };
                rateMemoryItem.SetContent(openRating);
                ownerAgent.WorkingMemory.AddItem(rateMemoryItem);
            }
            else
            {
                noMoreRatngs = true;
            }


            if (repetitionCount < maximumRepetitionCount && noMoreRatngs == false)
            {
                targetContext = successTargetContext;
                targetID = successTargetID;
            }
            else
            {
                targetContext = failureTargetContext;
                targetID = failureTargetID;
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
