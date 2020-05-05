using Library.Constants;
using Library.Helper;
using MailKit.Net.Smtp;

using MimeKit;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vitae.Code.AppSettings;

namespace Vitae.Code.Mailing
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage() {};
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = $"{Globals.APPLICATION_NAME} - {message.Subject}";

            var bodyBuilder = new BodyBuilder { HtmlBody = message.Content };
            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    var entity = bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.Name));
                    entity.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);
                    entity.ContentId = attachment.FileName;
                }

            }

            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, AesHandler.Decrypt(_emailConfig.Password, Globals.APPLICATION_NAME));

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}

/*
 * 
        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Globals.SENDER_MAIL, Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            // <img alt="myvitae.ch" id="logo" style="vertical-align: middle;" src="cid:logo" />
            msg.AddAttachment($"{CodeHelper.AssemblyDirectory}", Convert.ToBase64String(File.ReadAllBytes($@"{CodeHelper.AssemblyDirectory}/MailTemplates/Logo.png")), "image/png", "inline", "logo");
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }

    */
