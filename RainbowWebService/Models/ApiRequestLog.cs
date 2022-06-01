using System;

namespace RainbowWebService.Models
{
    public class ApiRequestLog
    {
        public string Endpoint { get; set; }
        public string Method { get; set; }
        public string Request { get; set; }
        public int ResponseStatusCode { get; set; }
        public DateTime InsertedAt { get; set; }

    }
}
