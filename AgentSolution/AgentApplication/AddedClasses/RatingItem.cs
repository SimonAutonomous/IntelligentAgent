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
        private OutputAction outputAction;
        private readonly UltraManager _ultraManager = UltraManager.Instance;

        public RatingItem() { }

        public RatingItem(string id, List<string> inputQueryTagList)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);

            string movieTitle = inputQueryTagList[0];
            string movieRatingString = inputQueryTagList[1];
            double movieRating = Convert.ToDouble(movieRatingString);

            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag("currentUser");
            string currentUser = ""; //TODO must get currentUser from working memory
            if (itemSought != null)  // 20171201
            {
                currentUser = (string)itemSought.GetContent();
            }

            Boolean existingRating = false;

            foreach (var rating in _ultraManager.RatingList)
            {
                if (rating.UserName == currentUser)
                {
                    if(rating.MovieTitle == movieTitle)
                    {
                        existingRating = true;
                        rating.RatingValue = movieRating;
                    }
                }
            }
            if(existingRating == false)
            {
                var rating = new Rating(movieTitle, currentUser, movieRating);
               _ultraManager.RatingList.Add(rating);
            }

            //MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            //if (itemSought != null)  // 20171201
            //{
            //    currentUser = (string)itemSought.GetContent();
            //}

            //Boolean existingUser = false;
            //foreach (var user in _ultraManager.UserList)
            //{
            //    if (user.Name == currentUser)
            //    {
            //        existingUser = true;
            //    }
            //}

            targetContext = outputAction.TargetContext;
            targetID = outputAction.TargetID;
            return true;
        }

        [DataMember]
        public List<string> InputQueryTagList
        {
            get { return inputQueryTagList; }
            set { inputQueryTagList = value; }
        }

        [DataMember]
        public OutputAction OutputAction
        {
            get { return outputAction; }
            set { outputAction = value; }
        }
    }
}
