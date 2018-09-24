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

namespace AgentApplication.AddedClasses
{
    [DataContract]
    public class RatingItem : DialogueItem
    {
        //private OutputAction outputAction = new OutputAction();
        private List<string> inputQueryTagList;
        //private OutputAction outputAction;
        private readonly UltraManager _ultraManager = UltraManager.Instance;

        private string successTargetContext;
        private string successTargetID;
        private string failureTargetContext;
        private string failureTargetID;

        public RatingItem() { }

        public RatingItem(string id, List<string> inputQueryTagList, string successTargetContext, string successTargetID, string failureTargetContext, string failureTargetID)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
            this.successTargetContext = successTargetContext;
            this.successTargetID = successTargetID;
            this.failureTargetContext = failureTargetContext;
            this.failureTargetID = failureTargetID;
        }

        //public override void Initialize(Agent ownerAgent)
        //{
        //    base.Initialize(ownerAgent);
        //    foreach (Pattern pattern in outputAction.PatternList)
        //    {
        //        pattern.ProcessDefinition();
        //        //     pattern.ProcessDefinitionList();
        //    }
        //}

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);

            string movieTitle = inputQueryTagList[0];
            string movieRatingString = inputQueryTagList[1];
            string currentUser = inputQueryTagList[2];
            double movieRating = -1.0;
            Boolean successfulConversion = false;

            //TODO: get all in one call
            MemoryItem itemSought1 = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought1 != null) 
            {
                movieTitle = (string)itemSought1.GetContent();
            }

            MemoryItem itemSought2 = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[1]);
            if (itemSought2 != null)
            {
                movieRatingString = (string)itemSought2.GetContent();
            }
            //double movieRating = Convert.ToDouble(movieRatingString);  //TODO: exception
            try
            {
                movieRating = Convert.ToDouble(movieRatingString);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.GetType().Name, "is not a valid type");
            }
            if (movieRating > -0.1 && movieRating < 10.1)
            {
                successfulConversion = true;
            }

            MemoryItem itemSought3 = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[2]);
            if (itemSought3 != null)
            {
                currentUser = (string)itemSought3.GetContent();
            }

            if (successfulConversion)
            {
                Boolean existingRating = false;
                foreach (var rating in _ultraManager.RatingList)
                {
                    if (rating.UserName == currentUser)
                    {
                        if (rating.MovieTitle == movieTitle)
                        {
                            existingRating = true;
                            rating.RatingValue = movieRating;
                        }
                    }
                }
                if (existingRating == false && movieRating > 0) // if someone rates a movie with 0 means that the current user hasn't seen the movie
                {
                    var rating = new Rating(movieTitle, currentUser, movieRating);
                    _ultraManager.RatingList.Add(rating);
                }
                else if (existingRating == false && movieRating == 0) // Add to TasteProfilingBlacklist so the same movie will not be mentioned twice
                {
                    var blacklistRating = new Rating(movieTitle, currentUser, 0);
                    _ultraManager.TasteProfilingBlacklist.Add(blacklistRating); 
                }
            }

            if (successfulConversion)
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

        [DataMember]
        public string SuccessTargetContext
        {
            get { return successTargetContext; }
            set { successTargetContext = value; }
        }

        [DataMember]
        public string SuccessTargetID
        {
            get { return successTargetID; }
            set { successTargetID = value; }
        }

        [DataMember]
        public string FailureTargetContext
        {
            get { return failureTargetContext; }
            set { failureTargetContext = value; }
        }

        [DataMember]
        public string FailureTargetID
        {
            get { return failureTargetID; }
            set { failureTargetID = value; }
        }

        //[DataMember]
        //public OutputAction OutputAction
        //{
        //    get { return outputAction; }
        //    set { outputAction = value; }
        //}
    }
}
