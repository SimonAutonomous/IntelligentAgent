using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApplication.AddedClasses
{
    public sealed class UltraManager
    {
        private static readonly Lazy<UltraManager> Lazy =
            new Lazy<UltraManager>(() => new UltraManager());
        public static UltraManager Instance { get { return Lazy.Value; } }

        private UltraManager()
        {
            this.MovieList = new List<Movie>();
            this.UserList = new List<User>();
            this.RatingList = new List<Rating>();
        }

        public List<Movie> MovieList { get; }
        public List<User> UserList { get; }
        public List<Rating> RatingList { get; }

    }

}
