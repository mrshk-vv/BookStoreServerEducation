using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Store.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using Store.Shared.Common;

namespace Store.BusinessLogic.Services
{
    public class EmailProvider
    {
        private readonly SmtpOptions _smtpOptions;
        public EmailProvider(IConfiguration configuration, IOptions<SmtpOptions> smtpOptions)
        {
            _smtpOptions = smtpOptions.Value;
        }

        public async Task SendMailAsync(string email, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpOptions.SenderName,
                    _smtpOptions.SenderEmail));
                message.To.Add(new MailboxAddress(string.Empty,email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s,c,h,e) => true;

                    await client.ConnectAsync(_smtpOptions.Server,_smtpOptions.Port);

                    await client.AuthenticateAsync(_smtpOptions.UserName,
                        _smtpOptions.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
