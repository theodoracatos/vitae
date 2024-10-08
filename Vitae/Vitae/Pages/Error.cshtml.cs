using Library.Constants;
using Library.Helper;
using Library.Resources;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.WebUtilities;

using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Vitae.Code.Mailing;
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

        public async Task OnGetAsync(int statusCode) => await HandleStatusCodeAsync(statusCode);
        public async Task OnPostAsync(int statusCode) => await HandleStatusCodeAsync(statusCode);

        private readonly IEmailSender _emailSender;
        private readonly HttpContext _httpContext;
        private readonly IRequestCultureFeature requestCulture;

        public ErrorModel(IEmailSender emailSender, IHttpContextAccessor httpContextAccessor)
        {
            _emailSender = emailSender;
            _httpContext = httpContextAccessor.HttpContext;
            requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
        }

        private async Task HandleStatusCodeAsync(int statusCode)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            Code = statusCode;

            RequestFeature = HttpContext.Features.Get<IHttpRequestFeature>();
            Exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            Message = GetCustomizedMessage(statusCode) ?? ReasonPhrases.GetReasonPhrase(statusCode); // https://httpstatuses.com/

            // Send mail...
            if (statusCode != (int)HttpStatusCode.NotFound)
            {
                var message = new Message(new string[] { Globals.ADMIN_VITAE_MAIL }, $"Error {(Code)}", GetHtmlMessage(), null);
                await _emailSender.SendEmailAsync(message);
            }
        }

        private string GetCustomizedMessage(int statusCode)
        {
            string message = null;

            switch(statusCode)
            {
                case (int)HttpStatusCode.NotFound:
                    {
                        message = SharedResource.Status404;
                        break;
                    }
                case (int)HttpStatusCode.Forbidden:
                    {
                        message = SharedResource.Status403;
                        break;
                    }
                case (int)HttpStatusCode.InternalServerError:
                    {
                        message = SharedResource.Status500;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return message;
        }

        private string GetHtmlMessage()
        {
            var message = new StringBuilder();
            message.Append($"<html><body>");
            message.Append($"<h2>{Code}: {Message}</h2><hr />");
            message.Append($"<table>");
            message.Append($"<tr><td valign='top' style='width: 100px'><b>Method</b></td><td>{Method}</td></tr>");
            message.Append($"<tr><td valign='top'><b>Origin</b></td><td>{Origin}</td></tr>");
            message.Append($"<tr><td valign='top'><b>Ip</b></td><td>{_httpContext.Connection.RemoteIpAddress}</td></tr>");
            message.Append($"<tr><td valign='top'><b>Agent</b></td><td>{CodeHelper.GetUserAgent(_httpContext)}</td></tr>");
            message.Append($"<tr><td valign='top'><b>Uri</b></td><td>{CodeHelper.GetCalledUri(_httpContext)}</td></tr>");
            message.Append($"<tr><td valign='top'><b>Culture</b></td><td>{requestCulture.RequestCulture.UICulture.Name}</td></tr>");

            if (CodeHelper.GetCurriculumID(_httpContext) != Guid.Empty)
            {
                var curriculumID = CodeHelper.GetCurriculumID(_httpContext);
                message.Append($"<tr><td><b>CurriculumID</b></td><td>{curriculumID}</td></tr>");
            }

            var e = Exception;
            var order = 1;
            while (e != null)
            {
                message.Append($"<tr><td valign='top'><b>{order}.&nbsp;Type</b></td><td>{ e.GetType() }</td></tr>");
                message.Append($"<tr><td valign='top'><b>{order}.&nbsp;Message</b></td><td>{ e.Message }</td></tr>");
                message.Append($"<tr><td valign='top'><b>{order++}.&nbsp;Stacktrace</b></td><td>{ e.StackTrace }</td></tr>");
                e = e.InnerException;
            }
            message.Append("</table></body></html>");

            return message.ToString();
        }
    }
}