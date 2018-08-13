using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgentApplication.AddedClasses;

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



            User user1 = new User("TestUser");

            Movie movie1 = new Movie();
            Movie movie2 = new Movie();
            Movie movie3 = new Movie();

            user1.Ratings.Add(movie1, 3.2);
            user1.Ratings.Add(movie2, 2.3);
            user1.Ratings.Add(movie3, 5);

            


            var users = new List<User>
            {
                new User("newUser2"),
                new User("newUser3"),
                new User("newUser4")
            };

            foreach (var user in users)
            {
                Debug.WriteLine(user.Name);
            }

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
