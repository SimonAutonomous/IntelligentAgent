using System.Runtime.Serialization;

namespace AgentApplication.AddedClasses
{

    [DataContract]
    public class Rating
    {
        [DataMember] public Movie Movie { get; set; }
        [DataMember] public User User { get; set; }
        [DataMember] public double RatingValue { get; set; }

        public Rating(Movie movie, User user, double ratingValue)
        {
            this.Movie = movie;
            this.User = user;
            this.RatingValue = ratingValue;
        }
    }
}