using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace KitsuneEy
{
    class RouteEy
    {
        public static HttpWebResponse GetPage(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = false;
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

        public static string GetPageTitle(string url)
        {
            return Regex.Match(new WebClient().DownloadString(url), @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                RegexOptions.IgnoreCase).Groups["Title"].Value;
        }

        public static string GetPageContext(string url)
        {
            return new WebClient().DownloadString(url);
        }

        public static bool GetPageTitleContains(string url, string context)
        {
            return GetPageTitle(url).Contains(context);
        }

        public static string GetPageFileMd5Hash(string url)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] md5Ch;
            //new WebClient().DownloadFile(url,"temp");
            using (MemoryStream stream = new MemoryStream(new WebClient().DownloadData(url)))
            {
                md5Ch = md5.ComputeHash(stream);
            }
            md5.Clear();
            string md5Str = "";
            foreach (var t in md5Ch)
            {
                md5Str = md5Str + t.ToString("x2");
            }
            return md5Str;
        }
    }
}
