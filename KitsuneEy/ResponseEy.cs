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

        public static bool GetWebServerContains(WebHeaderCollection headers, string serverStr)
        {
            return GetWebServer(headers).Contains(serverStr);
        }

        public static bool GetXPoweredByContains(WebHeaderCollection headers, string xPoweredByStr)
        {
            return GetXPoweredBy(headers).Contains(xPoweredByStr);
        }

        public static bool GetCookieContains(WebHeaderCollection headers,string cookieStr)
        {
            return headers.Get("set-cookie").Contains(cookieStr);
        }

        public static bool GetItemContains(WebHeaderCollection headers, string itemStr)
        {
            return headers.AllKeys.ToList().ConvertAll(x => x.ToLower()).Contains(itemStr);
        }

        public static string GetItemValue(WebHeaderCollection headers, string itemStr)
        {
            return headers.Get(itemStr);
        }

        public static bool GetItemValueContains(WebHeaderCollection headers, string itemStr, string contextStr)
        {
            return headers.Get(itemStr).ToLower().Contains(contextStr);
        }

        public static bool GetContextContains(WebHeaderCollection headers, string contextStr)
        {
            return headers.ToString().Contains(contextStr);
        }
    }
}
