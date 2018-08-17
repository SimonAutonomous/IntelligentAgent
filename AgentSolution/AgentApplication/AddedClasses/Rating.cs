using System.Runtime.Serialization;

namespace AgentApplication.AddedClasses
{

    [DataContract]
    public class Rating
    {
        [DataMember]
        public string MovieTitle { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public double RatingValue { get; set; }

        public Rating(string movieTitle, string userName, double ratingValue)
        {
            this.MovieTitle = movieTitle;
            this.UserName = userName;
            this.RatingValue = ratingValue;
        }
    }
}