using System;
using System.Linq;
using System.Net;

namespace KitsuneEy
{
    class Program
    {
        static void Main(string[] args)
        {
            string domainUrl = "https://cdn.sspai.com/static/js/vue-lazyload.js";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(domainUrl);
                request.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Console.WriteLine($"{Convert.ToInt32(response.StatusCode)} {response.StatusCode}");
                Console.WriteLine("server : " + response.Headers.Get("server"));
                if (response.Headers.AllKeys.Contains("x-powered-by"))
                {
                    Console.WriteLine("x-powered-by : " + response.Headers.Get("x-powered-by"));
                }

                response.Close();
            }
            catch (WebException e)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                HttpWebResponse response = (HttpWebResponse)e.Response;
                try
                {
                    Console.WriteLine($"{Convert.ToInt32(response.StatusCode)} {response.StatusCode}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{exception.Message}");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message} : {domainUrl}");
            }

            Console.ReadKey();
        }
    }
}
