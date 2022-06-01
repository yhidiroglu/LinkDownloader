using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkDownloader
{
    public class Downloader
    {
        public static ConcurrentBag<string> DownloadedUrlList = new ConcurrentBag<string>();
        public const string MainUrl = "https://tretton37.com";

        public Downloader()
        {

        }

        public async Task StartDownload(List<string> urlList, string downloadPath)
        {
            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            Parallel.ForEach(urlList, new ParallelOptions { MaxDegreeOfParallelism = 3 }, async url =>
            {
                if (!DownloadedUrlList.Contains(url))
                {
                    DownloadedUrlList.Add(url);
                    urlList = await DownloadAndSaveLink(url, downloadPath);

                    if (urlList != null)
                    {
                        await StartDownload(urlList, downloadPath);
                    }
                }
            });
        }

        private async Task<List<string>> DownloadAndSaveLink(string url, string downloadPath)
        {
            string path = $@"{downloadPath}/{url.Replace(".", "-").Split("://")[1]}.html"; //use this path with monolith

            using (WebClient client = new WebClient())
            {
                await StartMonolithExe(url, path);
                //in normal, this block should find urls and return this list to download node urls as recursive.
                var htmlContent = client.DownloadString(url);  
                var urlList = FindUrls(htmlContent);
                return null;
            }
        }

        private List<string> FindUrls(string htmlContent)
        {
            var result = new List<string>();

            var urlList = Regex.Matches(htmlContent, @"<a[^>]* href=""([^""]*)""").
                Cast<Match>().Select(m => m.Groups[1].Value).ToList();

            foreach (var url in urlList)
            {
                if (!DownloadedUrlList.Contains(string.Format("{0}{1}", MainUrl, url)))
                {
                    if (url.StartsWith("/"))
                    {
                        result.Add(string.Format("{0}{1}", MainUrl, url));
                    }
                }
            }

            return result;
        }

        private async Task StartMonolithExe(string url, string path)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(@"C:\YilmazWorkspace\LinkDownloader\monolith.exe");
            psi.Arguments = $"{url} -o {path}";
            psi.RedirectStandardOutput = true;
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            psi.UseShellExecute = false;

            var process = System.Diagnostics.Process.Start(psi);
            process.WaitForExit();
        }
    }
}
