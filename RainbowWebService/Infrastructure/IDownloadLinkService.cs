namespace RainbowWebService.Infrastructure
{
    public interface IDownloadLinkService
    {
        string DownloadLink(string weburl, out string errorMessage);

    }
}
