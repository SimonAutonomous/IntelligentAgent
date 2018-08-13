using System.Collections.Generic;

namespace AgentApplication.AddedClasses
{
    public class User
    {
        public string Name { get; }
        public Dictionary<Movie, double> Ratings { get; set; }

        public User(string name)
        {
            Name = name;
            Ratings = new Dictionary<Movie, double>();
        }


    }
}