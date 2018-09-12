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
using System.Diagnostics;

namespace AgentApplication.AddedClasses
{
    class RecommendationItem : AsynchronousDialogueItem
    {
        //private OutputAction outputAction = new OutputAction();
        private List<string> inputQueryTagList;
        private OutputAction outputAction;
        private readonly UltraManager _ultraManager = UltraManager.Instance;

        private string successTargetContext;
        private string successTargetID;
        private string failureTargetContext;
        private string failureTargetID;

        private int maximumRepetitionCount;
        private string outputQueryTag;

        public RecommendationItem() { }

        public RecommendationItem(string id, List<string> inputQueryTagList, string outputQueryTag)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
            this.outputQueryTag = outputQueryTag;
        }

        public override void Initialize(Agent ownerAgent)
        {
            base.Initialize(ownerAgent);
        }

        protected override void RunLoop(List<object> parameterList)
        {
            string originalContext = ownerAgent.WorkingMemory.CurrentContext;
            string currentUser = "";
            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought != null)  // 20171201
            {
                currentUser = (string)itemSought.GetContent();
            }

            // Recommend!

            // Get similarity of currentUser to all other users-------------------------------------------------

            // create ratingTable
            List<string> userList = new List<string> { };
            List<string> movieList = new List<string> { };
            userList.Add(currentUser);
            foreach (var rating in _ultraManager.RatingList)
            {
                if (! userList.Exists(x => string.Equals(x, rating.UserName, StringComparison.OrdinalIgnoreCase)))
                {
                    userList.Add(rating.UserName);
                }
            }
            foreach (var rating in _ultraManager.RatingList)
            {
                if (! movieList.Exists(x => string.Equals(x, rating.MovieTitle, StringComparison.OrdinalIgnoreCase)))
                {
                    movieList.Add(rating.MovieTitle);
                }
            }

            int nbrOfUsers = userList.Count;
            int userInsertionIndex = 0;
            int nbrOfMovies = movieList.Count;
            int movieInsertionIndex = 0;
            double[,] ratingTable = new double[nbrOfUsers, nbrOfMovies]; 

            foreach (var rating in _ultraManager.RatingList)
            {
                userInsertionIndex = userList.FindIndex(x => x.StartsWith(rating.UserName));
                movieInsertionIndex = movieList.FindIndex(x => x.StartsWith(rating.MovieTitle));
                ratingTable[userInsertionIndex, movieInsertionIndex] = rating.RatingValue;
            }

            // calculate mean rating for each user
            double[] meanRatings = new double[nbrOfUsers];
            for(int i=0; i<nbrOfUsers; ++i)
            {
                double ratingSum = 0;
                int nbrOfRated = 0;
                for(int j=0; j<nbrOfMovies; ++j)
                {
                    if (ratingTable[i, j] > 0)
                    {
                        ratingSum = ratingSum + ratingTable[i, j];
                        ++nbrOfRated;
                    }
                }
                if(ratingSum > 0 && nbrOfRated > 0)
                {
                    meanRatings[i] = ratingSum / nbrOfRated;
                }
            }

            // calculate Pearson correlation
            List<double> simToUser = new List<double> { 0 }; // TODO: use dictionary
            for (int i=1; i<nbrOfUsers; ++i)
            {
                List<int> setRatedBoth = new List<int> { };
                for (int j=0; j<nbrOfMovies; ++j)
                {
                    if(ratingTable[0, j]>0 && ratingTable[i, j] > 0)
                    {
                        setRatedBoth.Add(j);
                    }
                }

                double sum1 = 0;
                double sum2 = 0;
                double sum3 = 0;
                for (int k=0; k<setRatedBoth.Count; ++k)
                {
                    sum1 = sum1 + (ratingTable[0, setRatedBoth[k]] - meanRatings[0]) * (ratingTable[i, setRatedBoth[k]] - meanRatings[i]);
                    sum2 = sum2 + Math.Pow(ratingTable[0, setRatedBoth[k]] - meanRatings[0], 2);
                    sum3 = sum3 + Math.Pow(ratingTable[i, setRatedBoth[k]] - meanRatings[i], 2);
                }
                simToUser.Add(sum1 / (Math.Sqrt(sum2 * sum3)));
            }

            // Get most similar user
            int mostSimilarUser = simToUser.IndexOf(simToUser.Max());
            Debug.WriteLine(userList[mostSimilarUser]);

            // Return random unseen movie
            List<string> unseenMovies = new List<string> { };
            for (int j = 0; j < nbrOfMovies; ++j)
            {
                if (ratingTable[0, j] == 0 && ratingTable[mostSimilarUser, j] > 5)
                {
                    unseenMovies.Add(movieList[j]);
                }
            }
            Boolean noMoreRecommendations = false;
            if (unseenMovies.Count > 0)
            {
                int randomIndex = ownerAgent.RandomNumberGenerator.Next(0, unseenMovies.Count);
                string recommendation = unseenMovies[randomIndex];
                StringMemoryItem rateMemoryItem = new StringMemoryItem();
                rateMemoryItem.TagList = new List<string>() { outputQueryTag };
                rateMemoryItem.SetContent(recommendation);
                ownerAgent.WorkingMemory.AddItem(rateMemoryItem);
            }
            else
            {
                noMoreRecommendations = true;
            }

            AsynchronousDialogueItemEventArgs e = new AsynchronousDialogueItemEventArgs(originalContext, outputAction.TargetContext, outputAction.TargetID);
            OnRunCompleted(e);
        }

        [DataMember]
        public OutputAction OutputAction
        {
            get { return outputAction; }
            set { outputAction = value; }
        }

        public List<string> InputQueryTagList
        {
            get { return inputQueryTagList; }
            set { inputQueryTagList = value; }
        }
    }


}
