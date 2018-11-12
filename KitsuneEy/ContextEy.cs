using System.Net;
using System.Text.RegularExpressions;

namespace KitsuneEy
{
    class ContextEy
    {
        public static string GetPageTitle(string url)
        {
            return Regex.Match(new WebClient().DownloadString(url), @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                RegexOptions.IgnoreCase).Groups["Title"].Value;
        }

        public static string GetPageText(string url)
        {
            return new WebClient().DownloadString(url);
        }

        public static bool GetPageTitleContains(string url, string context)
        {
            return GetPageTitle(url).Contains(context);
        }

        public static bool GetPageTextContains(string url, string context)
        {
            return GetPageText(url).Contains(context);
        }
    }
}
