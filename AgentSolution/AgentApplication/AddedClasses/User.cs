using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AgentApplication.AddedClasses
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Dictionary<Movie, double> Ratings { get; set; }
        [DataMember]
        public bool OpenRaiting { get; set; }

        public User()
        {
            Name = "New User";
            Ratings = new Dictionary<Movie, double>();
            OpenRaiting = false;
        }

        public User(string name, bool openRaiting)
        {
            Name = name;
            Ratings = new Dictionary<Movie, double>();
            OpenRaiting = openRaiting;
        }


    }
}