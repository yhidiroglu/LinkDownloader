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
    class Program
    {
        public const string MainDownloadPath = @"C:\LinkDownloader\";
        public const string MainUrl = "https://tretton37.com";

        static void Main(string[] args)
        {
            string downloadPath = string.Format("{0}Tretto37com-{1}", MainDownloadPath, DateTime.Now.ToString("yyyyMMddHHmmss"));
            var urlList = new List<string> { MainUrl };

            var _downloader = new Downloader();

            _downloader.StartDownload(urlList, downloadPath).Wait();
        }

    }
}
