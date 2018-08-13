using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApplication.AddedClasses
{
    public class Movie
    {
        public string Title { get; }
        public int Year { get; }
        public double ImdbRating { get; }

        public Movie()
        {
            Title = "Test Movie";
            Year = 9999;
            ImdbRating = 0.1;
        }

        public Movie(string title, int year, double imdbRating)
        {
            Title = title;
            Year = year;
            ImdbRating = imdbRating;
        }
    }
}
