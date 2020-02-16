using Library.Helper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Vitae.Code
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Constants.SENDER_MAIL, Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddAttachment("Logo.png", Convert.ToBase64String(File.ReadAllBytes($@"{CodeHelper.AssemblyDirectory}/MailTemplates/Logo.png")), "image/png", "inline", "logo");
            msg.AddAttachment("Background.jpg", Convert.ToBase64String(File.ReadAllBytes($@"{CodeHelper.AssemblyDirectory}/MailTemplates/Background.jpg")), "image/jpg", "inline", "background");
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}