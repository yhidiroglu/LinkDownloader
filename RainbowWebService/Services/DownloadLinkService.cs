﻿using RainbowWebService.Infrastructure;
using RainbowWebService.Managers;
using Serilog;
using System;
using System.Collections.Generic;

namespace RainbowWebService.Services
{
    public class DownloadLinkService : IDownloadLinkService
    {
        public const string MainDownloadPath = @"C:\LinkDownloader\";

        public DownloadLinkService()
        {

        }

        public string DownloadLink(string weburl, out string errorMessage)
        {
            errorMessage = string.Empty;
            string result = string.Empty;

            if (string.IsNullOrEmpty(weburl))
            {
                errorMessage = "weburl parameter should not be null or empty.";
                Log.Error($"{errorMessage}");
                return string.Empty;
            }

            try
            {
                List<string> urlList = new List<string> { weburl };
                string downloadPath = string.Format("{0}{1}-{2}",
                    MainDownloadPath, weburl.Replace(".", "-").Split("://")[1] ,DateTime.Now.ToString("yyyyMMddHHmmss"));

                DownloadManager downloadManager = new DownloadManager();
                downloadManager.StartDownload(urlList, downloadPath).Wait();

                return downloadPath;
            }
            catch (Exception ex)
            {
                errorMessage = "Cannot convert weburl to deeplink.";
                Log.Error($"An error has occured when converting weburl to deeplink. Exception: {ex}");
                return result;
            }
        }

    }
}
