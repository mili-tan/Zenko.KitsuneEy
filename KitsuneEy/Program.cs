using System;
using System.Linq;
using System.Net;

namespace KitsuneEy
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpWebResponse response = RouteEy.GetPage("http://www.discuz.net/forum.php");
            Console.WriteLine(ResponseEy.GetCookieContains(response.Headers, "_saltkey="));
            Console.ReadKey();
        }
    }
}
