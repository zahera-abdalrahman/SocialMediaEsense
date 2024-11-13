using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Hosting.Server;
using SocialMedia.Migrations;

namespace SocialMedia.Services
{
    public class SendEmail:ISendEmail
    {


        private readonly IWebHostEnvironment _environment;

        public SendEmail(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        public async Task SendEmailAsync(string email, string subject, string message)
        {

        MailMessage mail = new MailMessage
            {
                Subject = subject,
                Body = message,
                From = new MailAddress("socialmediaproject1201@gmail.com", "Social Media"),
                IsBodyHtml = true,

            };
            mail.To.Add(email);
            string appPassword = "siwbzukoapxnxuff";

            NetworkCredential networkCredential = new NetworkCredential("socialmediaproject1201@gmail.com", appPassword);

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = networkCredential
            };

            await client.SendMailAsync(mail);

        }


        private async Task SendConfirmEmail(string Email, string Name, string RandomCode)
        {
            string path = Path.Combine(_environment.WebRootPath, "EmailTemplate", "email.html");
            var emailBody = await File.ReadAllTextAsync(path);

            emailBody = emailBody.Replace("{{Name}}", Name);
            emailBody = emailBody.Replace("{{code}}", RandomCode);

            await SendEmailAsync(Email, "Confirm Email", emailBody);
        }



        public class GenerateRandom
        {
            public static string GenerateRandomAlphanumericString(int length = 10)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                var random = new Random();
                var randomString = new string(Enumerable.Repeat(chars, length)
                                                        .Select(s => s[random.Next(s.Length)]).ToArray());
                return randomString;
            }
        }



    }
}
