using APIServer.Domain;
using APIServer.Domain.Persistence;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Persistence.Repositories
{
    public class ShareRepository : IShareRepository
    {
        public readonly EmailSettings _emailSettings;
        public ShareRepository(IOptions<EmailSettings> emailsSettings)
        {
            _emailSettings = emailsSettings.Value;
        }

        public bool SendEmailAsync(EmailModel message)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress(_emailSettings.Name, _emailSettings.EmailId);
                emailMessage.From.Add(emailFrom);

                MailboxAddress emailTo = new MailboxAddress(message.EmailToName, message.EmailToId);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = message.EmailSubject;

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = message.EmailBody;
                emailMessage.Body = emailBodyBuilder.ToMessageBody();

                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSSL);
                emailClient.Authenticate(_emailSettings.EmailId, _emailSettings.Password);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }
    }
}
