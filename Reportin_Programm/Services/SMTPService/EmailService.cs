using Reportin_Programm.Services.DTOs;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Reportin_Programm.Services.SMTPService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendMail(EmailDTO mailInfo)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Smtp:EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(mailInfo.To));
            email.Subject = mailInfo.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = mailInfo.Body };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(
                    _config.GetSection("Smtp:EmailHost").Value,
                    Convert.ToInt32(_config.GetSection("Smtp:EmailPort").Value),
                    SecureSocketOptions.StartTls);
                smtpClient.Authenticate(
                    _config.GetSection("Smtp:EmailUsername").Value, 
                    _config.GetSection("Smtp:EmailPassword").Value);
                smtpClient.Send(email);
                smtpClient.Disconnect(true);
            }
        }
    }
}
