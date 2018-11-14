using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;

namespace KitsuneEy
{
    class HashEy
    {
        public static string GetFileMd5Hash(string url)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] md5Ch;
            using (MemoryStream stream = new MemoryStream(new WebClient().DownloadData(url)))
            {
                md5Ch = md5.ComputeHash(stream);
            }
            md5.Clear();
            return md5Ch.Aggregate("", (current, t) => current + t.ToString("x2"));
        }
    }
}
