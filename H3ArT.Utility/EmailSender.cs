using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace H3ArT.Utility
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string _from = "nguyenferbvn@gmail.com";
            MailMessage message = new MailMessage(_from, email, subject, htmlMessage);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);

            using var smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("nguyenferbvn@gmail.com", "xhpy chdu vevr emlk");

            try
            {
                await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
