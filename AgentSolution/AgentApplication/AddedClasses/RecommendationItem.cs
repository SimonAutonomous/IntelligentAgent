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
        private List<string> inputQueryTagList;
        private OutputAction outputAction;
        private readonly UltraManager _ultraManager = UltraManager.Instance;

        //private string successTargetContext;
        //private string successTargetID;
        //private string failureTargetContext;
        //private string failureTargetID;

        //private int maximumRepetitionCount;
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
            string recommendation = "";
            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought != null)  // 20171201
            {
                currentUser = (string)itemSought.GetContent();
            }

            // 1) create ratingTable --> matrix of all users and movies
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

            // 2) calculate mean rating for each user
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

            // 3) calculate Pearson correlation between current user and all other users
            List<double> simToUser = new List<double> { 0 }; 
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

            // 4) Get most similar user to current user
            int mostSimilarUser = simToUser.IndexOf(simToUser.Max());
            Debug.WriteLine(userList[mostSimilarUser]);

            // 5) Return random unseen movie of all movies rated above 5 by most similar user
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
                recommendation = unseenMovies[randomIndex];
            }
            else
            {
                noMoreRecommendations = true;
            }
            // END of algorithm
            
            // If no recommendation according to most similar user can be made --> recommend random movie instead
            // This section is not part of the recommendation algorithm anymore -----------------------------------------------------------------------------------
            if (noMoreRecommendations)
            {
                List<string> seenMoviesCurrentUser = new List<string> { };
                List<string> unseenMoviesCurrentUser = new List<string> { };
                foreach (var rating in _ultraManager.RatingList)
                {
                    if (rating.UserName == currentUser)
                    {
                        seenMoviesCurrentUser.Add(rating.MovieTitle);
                    }
                }

                foreach (var movie in _ultraManager.MovieList)
                {
                    Boolean movieNotSeen = true;
                    foreach (var movieTitle in seenMoviesCurrentUser)
                    {
                        if (movie.Title == movieTitle)
                        {
                            movieNotSeen = false;
                        }
                    }

                    if (movieNotSeen)
                    {
                        unseenMoviesCurrentUser.Add(movie.Title);
                    }
                }
                int randomIndex = ownerAgent.RandomNumberGenerator.Next(0, unseenMoviesCurrentUser.Count);
                recommendation = unseenMoviesCurrentUser[randomIndex];
            }
            // ----------------------------------------------------------------------------------------------------------------------------------------------------

            // Set openRating so that agent asks user to rate the movie after he has seen it --> next time introductionDialogue is triggerd 
            int listIndexOfCurrentUser = 0;
            foreach (var user in _ultraManager.UserList)
            {
                if (user.Name == currentUser)
                {
                    listIndexOfCurrentUser = _ultraManager.UserList.IndexOf(user);
                }
            }
            _ultraManager.UserList[listIndexOfCurrentUser] = new User(_ultraManager.UserList[listIndexOfCurrentUser].Name, true, recommendation);

            // Return movie recommendation 
            StringMemoryItem rateMemoryItem = new StringMemoryItem();
            rateMemoryItem.TagList = new List<string>() { outputQueryTag };
            rateMemoryItem.SetContent(recommendation);
            ownerAgent.WorkingMemory.AddItem(rateMemoryItem);

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
