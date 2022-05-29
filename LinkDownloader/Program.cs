using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkDownloader
{
    class Program
    {
        public const string DownloadPath = @"C:\LinkDownloader\";
        public const string MainUrl = "https://tretton37.com/";
        static void Main(string[] args)
        {
            if (!Directory.Exists(DownloadPath))
                Directory.CreateDirectory(DownloadPath);

            var urlList = new List<string> { MainUrl };

            StartDownload(urlList);
        }

        public static void StartDownload(List<string> urlList)
        {
            Parallel.ForEach(urlList, new ParallelOptions { MaxDegreeOfParallelism = 3 }, async url =>
            {
                urlList = await DownloadAndSaveLink(url);
            });

            if (urlList != null)
            {
                StartDownload(urlList);
            }
        }

        public async static Task<List<string>> DownloadAndSaveLink(string url)
        {
            string path = $@"{DownloadPath}{url.Split("://")[1]}.html";
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                string htmlCode = await client.DownloadStringTaskAsync(url);
                File.WriteAllText(path, htmlCode);

                var urlList = FindUrls(htmlCode);
                return urlList;
            }
        }

        public static List<string> FindUrls(string htmlContent)
        {
            var urlList = Regex.Matches(htmlContent, @"<a[^>]* href=""([^""]*)""").
                Cast<Match>().Select(m => m.Groups[1].Value).ToList();

            return urlList;
        }
    }
}
