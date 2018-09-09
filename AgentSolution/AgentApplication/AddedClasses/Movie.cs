using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgentApplication.AddedClasses
{
    [DataContract]
    public class Movie
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public double ImdbRating { get; set; }
        [DataMember]
        public string Genre { get; set; }

        public Movie()
        {
            Title = "Test Movie";
            Year = 9999;
            ImdbRating = 0.1;
            Genre = "Drama";
        }

        public Movie(string title, int year, double imdbRating, string genre)
        {
            Title = title;
            Year = year;
            ImdbRating = imdbRating;
            Genre = genre;
        }
    }
}
