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

            int nbrOfUsers = userList.Capacity;
            int userInsertionIndex = 0;
            int nbrOfMovies = movieList.Capacity;
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
                for (int k=0; k<setRatedBoth.Capacity; ++k)
                {
                    sum1 = sum1 + (ratingTable[0, setRatedBoth[k]] - meanRatings[0]) * (ratingTable[i, setRatedBoth[k]] - meanRatings[i]);
                    sum2 = sum2 + Math.Pow(ratingTable[0, setRatedBoth[k]] - meanRatings[0], 2);
                    sum3 = sum3 + Math.Pow(ratingTable[i, setRatedBoth[k]] - meanRatings[i], 2);
                    simToUser.Add = sum1 / (Math.Sqrt(sum2 * sum3));
                }
            }

            AsynchronousDialogueItemEventArgs e = new AsynchronousDialogueItemEventArgs(originalContext, outputAction.TargetContext, outputAction.TargetID);
            OnRunCompleted(e);
        }

        //public override void RunLoop(List<object> parameterList, out string targetContext, out string targetID)
        //{
        //    base.Run(parameterList, out targetContext, out targetID);


        //    string currentUser = "";
        //    MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
        //    if (itemSought != null)  // 20171201
        //    {
        //        currentUser = (string)itemSought.GetContent();
        //    }

        //    List<string> openRatings = new List<string> { };
        //    foreach (var movie in _ultraManager.MovieList)
        //    {
        //        Boolean addItem = true;
        //        //movie.Title ==
        //        foreach (var rating in _ultraManager.RatingList)
        //        {
        //            if (rating.UserName == currentUser && rating.MovieTitle == movie.Title)
        //            {
        //                addItem = false;
        //            }
        //        }
        //        if (addItem)
        //        {
        //            openRatings.Add(movie.Title);
        //        }
        //    }

        //    if (repetitionCount < maximumRepetitionCount)
        //    {
        //        targetContext = successTargetContext;
        //        targetID = successTargetID;
        //    }
        //    else
        //    {
        //        targetContext = failureTargetContext;
        //        targetID = failureTargetID;
        //    }

        //    throw new NotImplementedException();
        //}

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
