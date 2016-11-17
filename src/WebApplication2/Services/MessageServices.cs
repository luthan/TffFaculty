using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713

    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        readonly IOptions<AppSettings> _appSettings;

        public AuthMessageSender(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            
            var newMessage = new MailMessage();
            newMessage.To.Add(new MailAddress(email));
            newMessage.Subject = subject;
            newMessage.Body = message;
            newMessage.IsBodyHtml = true;
            newMessage.From = new MailAddress(_appSettings.Value.FromEmail);
            var smtp = new SmtpClient {Host = _appSettings.Value.EmailServer};

            smtp.Send(newMessage);
            return Task.FromResult(0);
        }

        public Task SendEmailAsync(string toAddress, string fromAddress, string displayFromName, string subject, string message)
        {
            var newMessage = new MailMessage();
            newMessage.To.Add(new MailAddress(toAddress));
            newMessage.Subject = subject;
            newMessage.Body = message;
            newMessage.IsBodyHtml = true;
            newMessage.From = new MailAddress(fromAddress, displayFromName);
            var smtp = new SmtpClient { Host = _appSettings.Value.EmailServer };
            smtp.Send(newMessage);

            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }

        
    }
}
