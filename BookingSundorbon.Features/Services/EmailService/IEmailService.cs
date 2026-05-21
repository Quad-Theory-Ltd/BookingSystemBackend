using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Services.EmailService
{
    public interface IEmailServices
    {
        Task<string> SendMailAsync(string email, string subject, string body);
        Task<string> SendMailAsync(
                string email,
                string subject,
                string body,
                List<(byte[] FileBytes, string FileName, string ContentType)>? attachments = null);
    }
}
