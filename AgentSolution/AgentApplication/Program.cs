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


            var ultraManager = UltraManager.Instance;


            User user1 = new User("TestUser", false, "");

            Movie movie1 = new Movie();
            Movie movie2 = new Movie();
            Movie movie3 = new Movie();

            //user1.Ratings.Add(movie1, 3.2);
            //user1.Ratings.Add(movie2, 2.3);
            //user1.Ratings.Add(movie3, 5);

            


            var userList = new List<User>
            {
                new User("newUser2", false, ""),
                new User("newUser3", false, ""),
                new User("newUser4", false, "")
            };

            foreach (var user in userList)
            {
                Debug.WriteLine(user.Name);
            }


            //-----------------------------------------------------------------------------------------------------------------
            //SERIALIZATION
            //https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/data-contract-known-types
            //TODO: how to use without changing library?
            //ObjectXmlSerializer sdata = new ObjectXmlSerializer();
            List<Type> typeList = new List<Type>();
            typeList.Add(Type.GetType("Dicitonary"));
            ObjectXmlSerializer.SerializeObject("test", user1, typeList);

            ObjectXmlSerializer.ObtainSerializedObject("test", typeof(User), typeList);

            User user10 = (User)ObjectXmlSerializer.ObtainSerializedObject("test", typeof(User), typeList);

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
