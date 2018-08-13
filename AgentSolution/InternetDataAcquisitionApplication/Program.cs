using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetDataAcquisitionApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //1.Search request erstellen => url 
            //2.Get Json from Url (http://www.omdbapi.com) http://www.omdbapi.com/?t=scream&apikey=c983ca13
            //3. Parse Json
            //4. Create Movies

            var antwort = GET("http://www.omdbapi.com/?t=scream&apikey=c983ca13");
            Debug.WriteLine(antwort);

            string GET(string url)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                try
                {
                    WebResponse response = request.GetResponse();
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                        return reader.ReadToEnd();
                    }
                }
                catch (WebException ex)
                {
                    WebResponse errorResponse = ex.Response;
                    using (Stream responseStream = errorResponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                        String errorText = reader.ReadToEnd();
                        // log errorText
                    }
                    throw;
                }
            }




            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InternetDataAcquisitionMainForm());
        }
    }
}
