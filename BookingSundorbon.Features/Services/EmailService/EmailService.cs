using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Services.EmailService
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _config;
        private readonly string _senderEmail;
        private readonly string _senderPassword;
        private readonly string _smtpServer;
        private readonly int _smtpPort;

        public EmailServices(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _senderEmail = _config["EmailSettings:SenderEmail"]
                ?? throw new InvalidOperationException("SenderEmail configuration is missing.");
            _senderPassword = _config["EmailSettings:SenderPassword"]
                ?? throw new InvalidOperationException("SenderPassword configuration is missing.");
            _smtpServer = _config["EmailSettings:SmtpServer"]
                ?? throw new InvalidOperationException("SmtpServer configuration is missing.");
            if (!int.TryParse(_config["EmailSettings:SmtpPort"], out _smtpPort))
            {
                throw new InvalidOperationException("Invalid SmtpPort configuration.");
            }
        }

        public async Task<string> SendMailAsync(string email, string subject, string body)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Recipient email cannot be null or empty.", nameof(email));
                }

                // Extract display name from email if needed
                var senderDisplayName = "Sundarban Cargo Services";
                var fromAddress = new MailAddress(_senderEmail, senderDisplayName);

                var mail = new MailMessage
                {
                    From = fromAddress,
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                    Priority = MailPriority.Normal
                };
                mail.To.Add(email);
                // Add Reply-To header
                mail.ReplyToList.Add(new MailAddress(_senderEmail, senderDisplayName));

                // Add important headers to improve deliverability
                mail.Headers.Add("X-Mailer", "Sundarban Cargo Services");
                mail.Headers.Add("X-Priority", "3");
                mail.Headers.Add("X-MSMail-Priority", "Normal");
                mail.Headers.Add("Importance", "Normal");
                mail.Headers.Add("Message-ID", $"<{Guid.NewGuid()}@{_smtpServer}>");

                // Add plain text alternative (helps with spam filtering)
                var plainTextBody = Regex.Replace(body, "<[^>]+>", "");
                var alternateView = AlternateView.CreateAlternateViewFromString(plainTextBody, null, "text/plain");
                mail.AlternateViews.Add(alternateView);

                using var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
                {
                    Credentials = new NetworkCredential(_senderEmail, _senderPassword),
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 30000 // 30 seconds timeout
                };

                await smtpClient.SendMailAsync(mail);
                return "Email sent successfully!";
            }
            catch (SmtpException smtpEx)
            {
                // Handle specific SMTP errors
                Console.WriteLine($"SMTP error: {smtpEx.Message}");
                return $"SMTP error: {smtpEx.Message}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }



        public async Task<string> SendMailAsync(
                string email,
                string subject,
                string body,
                List<(byte[] FileBytes, string FileName, string ContentType)>? attachments = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("Recipient email cannot be null or empty.", nameof(email));

                var senderDisplayName = "Sundarban Cargo Services";
                var fromAddress = new MailAddress(_senderEmail, senderDisplayName);

                var plainTextBody = Regex.Replace(body, "<[^>]+>", "");
                var mail = new MailMessage { 
                    From = fromAddress, 
                    Subject = subject, 
                    Body = plainTextBody, 
                    IsBodyHtml = false, 
                    Priority = MailPriority.Normal }; 
                mail.To.Add(email); 
                mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(plainTextBody, null, "text/plain")); 
                mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, "text/html"));
                mail.ReplyToList.Add(new MailAddress(_senderEmail, senderDisplayName));

                // Headers (keep yours)
                mail.Headers.Add("X-Mailer", "Sundarban Cargo Services");
                mail.Headers.Add("X-Priority", "3");
                mail.Headers.Add("X-MSMail-Priority", "Normal");
                mail.Headers.Add("Importance", "Normal");
                mail.Headers.Add("Message-ID", $"<{Guid.NewGuid()}@{_smtpServer}>");

                // ✅ ADD ATTACHMENTS HERE
                if (attachments != null && attachments.Any())
                {
                    foreach (var file in attachments)
                    {
                        var stream = new MemoryStream(file.FileBytes);

                        var attachment = new Attachment(stream, file.FileName, file.ContentType);
                        mail.Attachments.Add(attachment);
                    }
                }

                using var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
                {
                    Credentials = new NetworkCredential(_senderEmail, _senderPassword),
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 30000
                };

                await smtpClient.SendMailAsync(mail);

                return "Email sent successfully!";
            }
            catch (SmtpException smtpEx)
            {
                return $"SMTP error: {smtpEx.Message}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
