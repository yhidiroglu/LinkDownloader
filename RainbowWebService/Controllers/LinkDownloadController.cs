using Microsoft.AspNetCore.Mvc;
using RainbowWebService.Infrastructure;
using RainbowWebService.Models;

namespace RainbowWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinkDownloadController : ControllerBase
    {
        private readonly IDownloadLinkService _downloadLinkService;

        public LinkDownloadController(IDownloadLinkService downloadConvertService)
        {
            _downloadLinkService = downloadConvertService;
        }

        [Route("DownloadWebPageLink")]
        [HttpGet]
        public IActionResult DownloadWebPageLink(string webUrl)
        {
            Response response;

            string result = _downloadLinkService.DownloadLink(webUrl, out string errorMessage);

            if (string.IsNullOrEmpty(result))
            {
                response = new Response
                {
                    Result = result,
                    Message = errorMessage
                };

                return BadRequest(response);
            }

            response = new Response
            {
                Result = result,
                Message = errorMessage
            };

            return Ok(response);
        }

    }
}
