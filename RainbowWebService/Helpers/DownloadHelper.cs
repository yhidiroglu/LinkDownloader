using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RainbowWebService.Helper
{
    public class DownloadHelper
    {
        private static ConcurrentBag<string> DownloadedUrlList = new ConcurrentBag<string>();
        public async Task<List<string>> DownloadAndSaveLink(string url, string downloadPath)
        {
            using (WebClient client = new WebClient())
            {
                StartMonolithExe(url, downloadPath).Wait();

                //in normal, this block should find urls and return this list to download node urls as recursive.
                var htmlContent = client.DownloadString(url);
                var urlList = FindUrls(htmlContent, url);
                return null;
            }
        }

        public List<string> FindUrls(string htmlContent, string mainUrl)
        {
            var result = new List<string>();

            var urlList = Regex.Matches(htmlContent, @"<a[^>]* href=""([^""]*)""").
                Cast<Match>().Select(m => m.Groups[1].Value).ToList();

            foreach (var url in urlList)
            {
                if (!DownloadedUrlList.Contains(string.Format("{0}{1}", mainUrl, url)))
                {
                    if (url.StartsWith("/"))
                    {
                        result.Add(string.Format("{0}{1}", mainUrl, url));
                    }
                }
            }

            return result;
        }

        public async Task StartMonolithExe(string url, string path)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo($@"{Directory.GetCurrentDirectory()}\monolith.exe");
            psi.Arguments = $"{url} -o {path}";
            psi.RedirectStandardOutput = true;
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            psi.UseShellExecute = false;

            var process = System.Diagnostics.Process.Start(psi);
            process.WaitForExit();
        }

    }
}
