using MailKit.Net.Smtp;
using MailKit.Security;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using OnionArchitecture.Application.DTOs.Mail;
using OnionArchitecture.Application.DTOs.Settings;
using OnionArchitecture.Application.Interfaces.Services;

namespace OnionArchitecture.Infrastructure.Services
{
    public class SmtpMailService : IMailService
    {
        public MailSettings _mailSettings { get; }
        public ILogger<SmtpMailService> _logger { get; }

        public SmtpMailService(IOptions<MailSettings> mailSettings, ILogger<SmtpMailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(MailRequest request)
        {
            try
            {
                var email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(request.From ?? _mailSettings.From)
                };
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder
                {
                    HtmlBody = request.Body
                };
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}