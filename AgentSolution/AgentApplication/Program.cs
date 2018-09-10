using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgentApplication.AddedClasses;
using System.Runtime.Serialization;
using ObjectSerializerLibrary;
using System.Web.Script.Serialization;
using AgentLibrary;
using InternetDataAcquisitionApplication.AddedClasses;

namespace AgentApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //TODO: copy to InetDataAcq
            //1. Search request erstellen => url 
            //2. Get Json from Url (http://www.omdbapi.com) http://www.omdbapi.com/?t=scream&apikey=c983ca13
            //3. Parse Json 
            //4. Create Movies

            //var antwort = GET("http://www.omdbapi.com/?t=scream&apikey=c983ca13");
            //Debug.WriteLine(antwort);

            //string GET(string url)
            //{
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //    try
            //    {
            //        WebResponse response = request.GetResponse();
            //        using (Stream responseStream = response.GetResponseStream())
            //        {
            //            StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
            //            return reader.ReadToEnd();
            //        }
            //    }
            //    catch (WebException ex)
            //    {
            //        WebResponse errorResponse = ex.Response;
            //        using (Stream responseStream = errorResponse.GetResponseStream())
            //        {
            //            StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
            //            String errorText = reader.ReadToEnd();
            //            // log errorText
            //        }
            //        throw;
            //    }
            //}
            
            var _ultraManager = UltraManager.Instance;
            //_ultraManager.LoadFromFile(DataTypeSelector.MovieList);
            //_ultraManager.LoadFromFile(DataTypeSelector.UserList);
            //_ultraManager.LoadFromFile(DataTypeSelector.RatingList);
            //List<string> lines = new List<string>();
            //lines.Add("line1");
            //lines.Add("line2");
            //System.IO.File.WriteAllLines(@"C:\Users\Simon\Desktop\WriteLines.txt", lines);

            string test = "Imdb|" + " " + "saw";
            List<string> requestSplit = test.Split(new char[] { AgentConstants.INTERNET_SEARCH_REQUEST_SEPARATOR_CHARACTER },
                StringSplitOptions.RemoveEmptyEntries).ToList();
            if (requestSplit[0].ToUpper().TrimEnd(new char[] { ' ' }) == "IMDB")
            {
                string txtMovieName = requestSplit[1].Replace(" ", "");
                Debug.WriteLine(txtMovieName.Trim());
                string url = "http://www.omdbapi.com/?t=" + txtMovieName.Trim() + "&apikey=c983ca13";
                using (WebClient wc = new WebClient())
                {
                    var json = wc.DownloadString(url);
                    JavaScriptSerializer oJS = new JavaScriptSerializer();
                    ImdbEntity obj = new ImdbEntity();
                    obj = oJS.Deserialize<ImdbEntity>(json);
                    if (obj.Response == "True")
                    {
                        string movieTitle = obj.Title;
                        double imdbRating = Convert.ToDouble(obj.imdbRating);
                        int year = Convert.ToInt16(obj.Year);
                        string genre = obj.Genre;

                        Movie newMovie = new Movie(movieTitle, year, imdbRating, genre);
                        Debug.WriteLine(newMovie.Title);
                        Debug.WriteLine(newMovie.Year);
                        Debug.WriteLine(newMovie.ImdbRating);
                        Debug.WriteLine(newMovie.Genre);
                        //_ultraManager.MovieList.Add(newMovie);

                    }
                    else
                    {
                        Debug.WriteLine("not found");
                    }
                }
            }
            
            

            



            /*string test = "Imdb|" + " " + "scream";
            List<string> requestSplit = test.Split(new char[] { AgentConstants.INTERNET_SEARCH_REQUEST_SEPARATOR_CHARACTER },
                StringSplitOptions.RemoveEmptyEntries).ToList();
            if (requestSplit[0].ToUpper().TrimEnd(new char[] { ' ' }) == "IMDB")
            {
                string txtMovieName = requestSplit[1].Replace(" ", "");
                Debug.WriteLine(txtMovieName.Trim());
            }*/

                /*
                Movie movie1 = new Movie("Pulp fiction", 1991, 8.9, "Drama");
                Movie movie2 = new Movie("The big lebowski", 1991, 8.9, "Drama");
                Movie movie3 = new Movie("Rum diary", 1991, 8.9, "Drama");
                Movie movie4 = new Movie("The room", 1991, 8.9, "Drama");
                Movie movie5 = new Movie("Chinatown", 1991, 8.9, "Drama");
                Movie movie6 = new Movie("Get out", 1991, 8.9, "Drama");
                Movie movie7 = new Movie("Jaws", 1991, 8.9, "Drama");

                _ultraManager.MovieList.Add(movie1);
                _ultraManager.MovieList.Add(movie2);
                _ultraManager.MovieList.Add(movie3);
                _ultraManager.MovieList.Add(movie4);
                _ultraManager.MovieList.Add(movie5);
                _ultraManager.MovieList.Add(movie6);
                _ultraManager.MovieList.Add(movie7);


                //jack
                Rating rating11 = new Rating("Pulp fiction", "jack", 2.0);
                _ultraManager.RatingList.Add(rating11);
                Rating rating12 = new Rating("The big lebowski", "jack", 1.0);
                _ultraManager.RatingList.Add(rating12);
                Rating rating13 = new Rating("The room", "jack", 5.0);
                _ultraManager.RatingList.Add(rating13);
                Rating rating14 = new Rating("Chinatown", "jack", 8.0);
                _ultraManager.RatingList.Add(rating14);
                Rating rating15 = new Rating("Get out", "jack", 10.0);
                _ultraManager.RatingList.Add(rating15);
                Rating rating16 = new Rating("Jaws", "jack", 9.0);
                _ultraManager.RatingList.Add(rating16);

                //gösta
                Rating rating21 = new Rating("Pulp fiction", "goesta", 10.0);
                _ultraManager.RatingList.Add(rating21);
                Rating rating22 = new Rating("The big lebowski", "goesta", 9.0);
                _ultraManager.RatingList.Add(rating22);
                Rating rating23 = new Rating("The room", "goesta", 7.0);
                _ultraManager.RatingList.Add(rating23);
                Rating rating24 = new Rating("Chinatown", "goesta", 3.0);
                _ultraManager.RatingList.Add(rating24);
                Rating rating25 = new Rating("Get out", "goesta", 8.0);
                _ultraManager.RatingList.Add(rating25);
                Rating rating26 = new Rating("Jaws", "goesta", 1.0);
                _ultraManager.RatingList.Add(rating26);
                Rating rating27 = new Rating("Rum diary", "goesta", 6.0);
                _ultraManager.RatingList.Add(rating27);

                //fredrik
                Rating rating31 = new Rating("Pulp fiction", "fredrik", 10.0);
                _ultraManager.RatingList.Add(rating31);
                Rating rating32 = new Rating("The big lebowski", "fredrik", 9.0);
                _ultraManager.RatingList.Add(rating32);
                Rating rating34 = new Rating("Chinatown", "fredrik", 2.0);
                _ultraManager.RatingList.Add(rating34);
                Rating rating37 = new Rating("Rum diary", "fredrik", 1.0);
                _ultraManager.RatingList.Add(rating37);

                //simon
                Rating rating41 = new Rating("Pulp fiction", "simon", 3.0);
                _ultraManager.RatingList.Add(rating41);
                Rating rating42 = new Rating("The big lebowski", "simon", 2.0);
                _ultraManager.RatingList.Add(rating42);
                Rating rating43 = new Rating("The room", "simon", 5.0);
                _ultraManager.RatingList.Add(rating43);
                Rating rating44 = new Rating("Chinatown", "simon", 9.0);
                _ultraManager.RatingList.Add(rating44);
                Rating rating45 = new Rating("Get out", "simon", 8.0);
                _ultraManager.RatingList.Add(rating45);
                Rating rating47 = new Rating("Rum diary", "simon", 9.0);
                _ultraManager.RatingList.Add(rating47);

                //eliane
                Rating rating51 = new Rating("Pulp fiction", "eliane", 3.0);
                _ultraManager.RatingList.Add(rating51);
                Rating rating54 = new Rating("Chinatown", "eliane", 9.0);
                _ultraManager.RatingList.Add(rating54);
                Rating rating56 = new Rating("Jaws", "eliane", 2.0);
                _ultraManager.RatingList.Add(rating56);
                Rating rating57 = new Rating("Rum diary", "eliane", 6.0);
                _ultraManager.RatingList.Add(rating57);

                //samuel
                Rating rating61 = new Rating("Pulp fiction", "samuel", 4.0);
                _ultraManager.RatingList.Add(rating61);
                Rating rating62 = new Rating("The big lebowski", "samuel", 3.0);
                _ultraManager.RatingList.Add(rating62);
                Rating rating63 = new Rating("The room", "samuel", 6.0);
                _ultraManager.RatingList.Add(rating63);
                Rating rating64 = new Rating("Chinatown", "samuel", 9.0);
                _ultraManager.RatingList.Add(rating64);
                Rating rating66 = new Rating("Jaws", "samuel", 8.0);
                _ultraManager.RatingList.Add(rating66);
                Rating rating67 = new Rating("Rum diary", "samuel", 8.0);
                _ultraManager.RatingList.Add(rating67);


                User user1 = new User("simon", false, "");
                _ultraManager.UserList.Add(user1);
                User user2 = new User("goesta", false, "");
                _ultraManager.UserList.Add(user2);
                User user3 = new User("jack", false, "");
                _ultraManager.UserList.Add(user3);
                User user4 = new User("samuel", false, "");
                _ultraManager.UserList.Add(user4);
                User user5 = new User("eliane", false, "");
                _ultraManager.UserList.Add(user5);
                User user6 = new User("fredrik", false, "");
                _ultraManager.UserList.Add(user6);
                */

                //var userList = new List<User>
                //{
                //    new User("newUser2", false, ""),
                //    new User("newUser3", false, ""),
                //    new User("newUser4", false, "")
                //};

                //foreach (var user in userList)
                //{
                //    Debug.WriteLine(user.Name);
                //}


                //-----------------------------------------------------------------------------------------------------------------
                //SERIALIZATION
                //https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/data-contract-known-types
                //TODO: how to use without changing library?
                //ObjectXmlSerializer sdata = new ObjectXmlSerializer();

                //User version
                //List<Type> typeList = new List<Type>();
                //typeList.Add(Type.GetType("Dicitonary"));
                //ObjectXmlSerializer.SerializeObject("test", user1, typeList);
                //ObjectXmlSerializer.ObtainSerializedObject("test", typeof(User), typeList);
                //User user10 = (User)ObjectXmlSerializer.ObtainSerializedObject("test", typeof(User), typeList);
                /*
                //Singleton version
                List<Type> typeList = new List<Type>();
                typeList.Add(Type.GetType("UltraManager"));
                ObjectXmlSerializer.SerializeObject("testSingleton", _ultraManager, typeList);
                ObjectXmlSerializer.ObtainSerializedObject("testSingleton", typeof(UltraManager), typeList);
                UltraManager _ultraManagerNew = (UltraManager)ObjectXmlSerializer.ObtainSerializedObject("testSingleton", typeof(UltraManager), typeList);

                foreach (var movie in _ultraManagerNew.MovieList)
                {
                    Debug.WriteLine(movie.Title);
                }*/

                //user10.Ratings.Add(movie3, 3);

                //foreach (var user10Rating in user10.Ratings)
                //{
                //    Debug.Write(user10Rating.Value);
                //}
                //-----------------------------------------------------------------------------------------------------------------


                //foreach (var rating in user1.Ratings)
                //{
                //    Debug.WriteLine($"{rating.Key.Title} - {rating.Value}");
                //}


                //var movies = new List<Movie>
                //{
                //    new Movie(),
                //    new Movie(),
                //    new Movie()
                //};

                //Linq Queries
                //users.First(user => user.Name == "newUser2").Ratings.Add(movies.First(),15);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AgentMainForm());

        }
    }
}
