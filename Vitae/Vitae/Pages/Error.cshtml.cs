using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

using System;
using System.Diagnostics;

namespace Vitae.Pages
{
    public class ErrorModel : PageModel
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

            Message = ReasonPhrases.GetReasonPhrase(statusCode); // https://httpstatuses.com/
        }
    }
}