using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Grosvenor.Portal.Model.Config;

namespace Grosvenor.Portal.Business
{
    public class EmailManager : IEmailManager
    {
        private readonly EmailConfig config;

        public EmailManager(IOptions<EmailConfig> options) => this.config = options.Value;

        public async Task<bool> SendAsync(string toEmailAddresses, string subject, string body)
        {
            if (!config.Enabled)
            {
                return true;
            }

            var message = new MimeMessage();
            if (string.IsNullOrEmpty(config.FromName))
            {
                message.From.Add(MailboxAddress.Parse(config.FromEmail));
            }
            else
            {
                message.From.Add(new MailboxAddress(config.FromName, config.FromEmail));
            }

            message.To.AddRange(InternetAddressList.Parse((string.IsNullOrEmpty(config.ToEmailMask) ? toEmailAddresses : config.ToEmailMask).Replace(";", ",")));

            if (string.IsNullOrEmpty(config.SubjectPrefix))
            {
                message.Subject = subject;
            }
            else
            {
                message.Subject = $"{config.SubjectPrefix} {subject}";
            }

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };

            message.Body = bodyBuilder.ToMessageBody();
           
            using var client = new SmtpClient();
            await client.ConnectAsync(config.Host, config.Port, MailKit.Security.SecureSocketOptions.StartTls);
            
            if (!string.IsNullOrEmpty(config.Username) && !string.IsNullOrEmpty(config.Password))
            {
                await client.AuthenticateAsync(config.Username, config.Password);
            }

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            return true;
        }

    }
}
