using Library.Constants;
using Library.Helper;
using MailKit.Net.Smtp;

using MimeKit;
using System;
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
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, AesHandler.Decrypt(_emailConfig.Password, Globals.APPLICATION_NAME));
                    await client.SendAsync(mailMessage);

                    await client.DisconnectAsync(true);
                }
                catch (Exception e)
                {
                    //log an error message or throw an exception or both.
                    throw e;
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