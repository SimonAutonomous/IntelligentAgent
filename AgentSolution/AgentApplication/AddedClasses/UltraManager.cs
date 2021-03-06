﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgentApplication.AddedClasses
{
    [DataContract]
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

        [DataMember]
        public List<Movie> MovieList { get; }
        [DataMember]
        public List<User> UserList { get; }
        [DataMember]
        public List<Rating> RatingList { get; }

    }

}
