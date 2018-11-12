using System.Linq;
using System.Net;

namespace KitsuneEy
{
    class ResponseEy
    {
        public static string GetWebServer(WebHeaderCollection headers)
        {
            return headers.Get("server");
        }

        public static string GetXPoweredBy(WebHeaderCollection headers)
        {
            return headers.Get("x-powered-by");
        }

        public static bool GetCookieContains(WebHeaderCollection headers,string cookieStr)
        {
            return headers.Get("set-cookie").Contains(cookieStr);
        }

        public static bool GetItemContains(WebHeaderCollection headers, string itemStr)
        {
            return headers.AllKeys.Contains(itemStr);
        }
    }
}
