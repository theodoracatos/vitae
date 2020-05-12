using Library.Resources;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;

using System;
using System.Diagnostics;

using Vitae.Code.PageModels;

namespace Vitae.Pages
{
    public class ErrorModel : LandingPageBaseModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public IHttpRequestFeature RequestFeature { get; set; }
        public Exception Exception { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Method => RequestFeature?.Method;
        public string Origin => RequestFeature.RawTarget;

        public bool ShowException => Exception != null;
        public string ExceptionType => Exception?.GetType().ToString();
        public string ExceptionMessage => Exception?.Message;

        public void OnGet(int statusCode) => HandleStatusCode(statusCode);
        public void OnPost(int statusCode) => HandleStatusCode(statusCode);

        private void HandleStatusCode(int statusCode)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            Code = statusCode;

            RequestFeature = HttpContext.Features.Get<IHttpRequestFeature>();
            Exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            Message = GetCustomizedMessage(statusCode) ?? ReasonPhrases.GetReasonPhrase(statusCode); // https://httpstatuses.com/


        }

        private string GetCustomizedMessage(int statusCode)
        {
            string message = null;

            switch(statusCode)
            {
                case 404:
                    {
                        message = SharedResource.Status404;
                        break;
                    }
            }

            return message;
        }
    }
}