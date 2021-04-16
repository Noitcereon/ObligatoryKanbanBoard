using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Smtp;
using KanbanBoardMVCApp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace KanbanBoardMVCApp.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly SmtpSender _sender = new SmtpSender(() => new SmtpClient("localhost")
        {
            EnableSsl = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            DeliveryFormat = SmtpDeliveryFormat.International,
            Port = 25,
        });

        public EmailSender(ILogger<EmailSender> logger)
        {
            Email.DefaultSender = _sender;
            _logger = logger;
        }

        public EmailSender(SmtpSender sender, ILogger<EmailSender> logger)
        {
            _sender = sender;
            _logger = logger;
            Email.DefaultSender = _sender;
        }

        public async Task SendMailAsync(string receiverEmail, string subject, string emailBody)
        {
            var email = await Email
                .From("tba@live.dk", "KanbanBoard Application")
                .To(receiverEmail)
                .Subject(subject)
                .Body(emailBody)
                .SendAsync();
            if (email.Successful)
            {
                _logger.LogInformation($"E-mail sent. Subject: {subject}");
            }
            else
            {
                _logger.LogError("E-mail failed to send.");
            }
        }

        public async Task SendMailToMultipleAsync(List<string> receiverEmails, string subject, string emailBody)
        {
            foreach (string email in receiverEmails)
            {
                await SendMailAsync(email, subject, emailBody);
            }
        }
    }
}
