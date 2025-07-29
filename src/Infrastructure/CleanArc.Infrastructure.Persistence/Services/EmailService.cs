using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using CleanArc.Domain.Settings;

namespace CleanArc.Infrastructure.Persistence.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(
                _emailSettings.DisplayName,
                _emailSettings.FromEmail));

            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = message };
            using var client = new SmtpClient();

            await client.ConnectAsync(
                _emailSettings.SmtpServer,
                _emailSettings.Port,
                SecureSocketOptions.StartTls);

            // Remove XOAUTH2 authentication if not needed
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            await client.AuthenticateAsync(
                _emailSettings.Username,
                _emailSettings.Password);

            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
