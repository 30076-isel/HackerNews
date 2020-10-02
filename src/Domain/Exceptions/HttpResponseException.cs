using System;
using System.Net;

namespace HackerNewsAPI.Domain.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpStatusCode Status { get; private set; }

        public HttpResponseException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }
    }
}
