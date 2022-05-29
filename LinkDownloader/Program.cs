using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

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

            List<string> urlList = new List<string> { MainUrl };

            foreach (var url in urlList)
            {
                DownloadLink(url);
            }

        }


        public static List<string> DownloadLink(string url)
        {
            string path = $@"{DownloadPath}{url.Split("://")[1]}.html";
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                string htmlCode = client.DownloadString(url);
                File.WriteAllText(path, htmlCode);
            }
            return new List<string>();
        }

        public static bool SaveDownloadedFile(string content)
        {
            return true;
        }
    }
}
