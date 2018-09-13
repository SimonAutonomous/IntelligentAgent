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

            //foreach (var user in userList)
            //{
            //    Debug.WriteLine(user.Name);
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AgentMainForm());

        }
    }
}
