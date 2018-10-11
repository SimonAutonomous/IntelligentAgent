using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AgentApplication.AddedClasses
{
    [DataContract]
    public sealed class UltraManager
    {
        private const string _userFilePath = "users";
        private const string _movieFilePath = "movies";
        private const string _ratingFilePath = "ratings";

        public List<Rating> TasteProfilingBlacklist;
        public List<Rating> RecommendationBlacklist;

        private static readonly Lazy<UltraManager> Lazy =
            new Lazy<UltraManager>(() => new UltraManager());

        public static UltraManager Instance { get { return Lazy.Value; } }

        private UltraManager()
        {
            this.MovieList = new ObservableCollection<Movie>();
            this.UserList = new ObservableCollection<User>();
            this.RatingList = new ObservableCollection<Rating>();
            this.TasteProfilingBlacklist = new List<Rating>();
            this.RecommendationBlacklist = new List<Rating>();
            PopulateListsFromSavedData();
            AddEvents();
        }

        private void AddEvents()
        {
            MovieList.CollectionChanged += (sender, e) =>
            {
                SerializeList<ObservableCollection<Movie>>(_movieFilePath, MovieList);
            };
            UserList.CollectionChanged += (sender, e) =>
            {
                SerializeList<ObservableCollection<User>>(_userFilePath, UserList);
            };
            RatingList.CollectionChanged += (sender, e) =>
            {
                SerializeList<ObservableCollection<Rating>>(_ratingFilePath, RatingList);
            };
        }

        private void PopulateListsFromSavedData()
        {
            foreach (var dataTypeSelector in Enum.GetValues(typeof(DataTypeSelector)).Cast<DataTypeSelector>())
            {
                LoadFromFile(dataTypeSelector);
            }
        }

        [DataMember]
        public ObservableCollection<Movie> MovieList { get; set; }
        [DataMember]
        public ObservableCollection<User> UserList { get; set; }
        [DataMember]
        public ObservableCollection<Rating> RatingList { get; set; }

        public void LoadFromFile(DataTypeSelector dataTypeSelector)
        {
            switch (dataTypeSelector)
            {
                case DataTypeSelector.MovieList:
                {
                    MovieList = DeserializeList<ObservableCollection<Movie>>(_movieFilePath, MovieList);
                    return;
                }
                case DataTypeSelector.UserList:
                {
                    UserList = DeserializeList<ObservableCollection<User>>(_userFilePath, UserList);
                    return;
                }
                case DataTypeSelector.RatingList:
                {
                    RatingList = DeserializeList<ObservableCollection<Rating>>(_ratingFilePath, RatingList);
                    return;
                }
            }
        }

        private void SerializeList<T>(string filePath, T listToSave)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (FileStream fileStream = File.Create(filePath))
            {
                xmlSerializer.Serialize(fileStream, listToSave);
            }
        }

        private T DeserializeList<T>(String filePath, T listToLoad) where T : new ()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            if (File.Exists(filePath))
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    var newList = (T)xmlSerializer.Deserialize(streamReader);
                    listToLoad = newList;
                    return listToLoad;
                }
            }
            return new T();
        }
    }

}
