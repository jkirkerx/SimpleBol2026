using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Models.Smtp
{
    public class SmtpResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public HttpContent? Body { get; set; }
        public HttpResponseHeaders? Headers { get; set; }
        public SmtpError TypeError { get; set; }
        public string? ExceptionMessage { get; set; }

        // Constructor
        public SmtpResponse(bool success, HttpStatusCode statusCode, HttpContent? body, HttpResponseHeaders? headers, SmtpError typeError, string? exceptionMessage)
        {
            Success = success;
            StatusCode = statusCode;
            Body = body;
            Headers = headers;
            TypeError = typeError;
            ExceptionMessage = exceptionMessage;
        }
    }

    public enum SmtpError
    {
        None,
        Program_Code,
        Smtp_Code,
        Smtp_Socket
    }

}
