using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApplication.AddedClasses
{


    //TODO: remove?
    class MovieRatings
    {
        #region Constants
        private const string DEFAULT_MOVIE_RATING_RELATIVE_PATH = "..\\..\\..\\Data\\LongTermMemory.xml";
        #endregion

        #region Fields
        protected String currentUser;
        protected String[] userList;
        protected String[] movieList;

        #endregion

        #region Constructors
        public MovieRatings()
        {


        }
        #endregion

    }
}
