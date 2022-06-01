using RainbowWebService.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RainbowWebService.Managers
{
    public class DownloadManager
    {
        public static ConcurrentBag<string> DownloadedUrlList = new ConcurrentBag<string>();
        public static string MainUrl = string.Empty;

        public DownloadManager()
        {

        }

        public async Task StartDownload(List<string> urlList, string downloadPath)
        {
            DownloadHelper downloadHelper = new DownloadHelper();

            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            Parallel.ForEach(urlList, new ParallelOptions { MaxDegreeOfParallelism = 3 }, async url =>
            {
                if (!DownloadedUrlList.Contains(url))
                {
                    DownloadedUrlList.Add(url);
                    urlList = await downloadHelper.DownloadAndSaveLink(url, downloadPath);

                    if (urlList != null)
                    {
                        await StartDownload(urlList, downloadPath);
                    }
                }
            });
        }
    }
}
