using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkDownloader
{
    public class Downloader
    {
        public static ConcurrentBag<string> DownloadedUrlList = new ConcurrentBag<string>();
        public static ConcurrentBag<string> DownloadedComponentList = new ConcurrentBag<string>();

        public const string MainUrl = "https://tretton37.com";

        public Downloader()
        {

        }

        public async Task StartDownload(List<string> urlList, string downloadPath)
        {
            PrepareDownloadFolder(downloadPath);

            Parallel.ForEach(urlList, new ParallelOptions { MaxDegreeOfParallelism = 3 }, async url =>
            {
                urlList = DownloadAndSaveLink(url, downloadPath);

                if (urlList != null)
                {
                    await StartDownload(urlList, downloadPath);
                }
            });
        }

        private List<string> DownloadAndSaveLink(string url, string downloadPath)
        {
            string path = $@"{downloadPath}\{url.Split("://")[1]}.html"; //use this path with monolith

            using (WebClient client = new WebClient())
            {
                //TODO: Call monolith library to download url
                string htmlContent = client.DownloadString(url); //Download htmlcontent to find 

                var urlList = FindUrls(htmlContent);
                return urlList;
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

        private void PrepareDownloadFolder(string downloadPath)
        {
            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            string assetsPath = string.Format("{0}/assets/i", downloadPath);
            if (!Directory.Exists(assetsPath))
            {
                Directory.CreateDirectory(assetsPath);
            }

            string cssPath = string.Format("{0}/assets/css", downloadPath);
            if (!Directory.Exists(cssPath))
            {
                Directory.CreateDirectory(cssPath);
            }
        }
    }
}
