using System;
using System.Net;
using System.Text.RegularExpressions;

namespace KitsuneEy
{
    class RouteEy
    {
        public static HttpWebResponse GetPage(string url,bool allowAutoRedirect = true)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = allowAutoRedirect;
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                return (HttpWebResponse)e.Response;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message} : {url}");
                return null;
            }
        }

        public static bool GetPageExists(string url)
        {
            return GetPage(url).StatusCode == HttpStatusCode.OK;
        }

        public static bool GetPageStatusCode(string url, HttpStatusCode statusCode)
        {
            return GetPage(url).StatusCode == statusCode;
        }
    }
}
