using SendGrid;
using SendGrid.Helpers.Mail;

namespace AuctionHouse.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly string? apiKey = Environment.GetEnvironmentVariable("SendGridKey"); // possible null value
        private readonly EmailAddress fromEmail = new EmailAddress("dbozhkov09@gmail.com", "AuctionHouse");

        public async Task SendEmailToVerifyAsync(string toEmail, Guid token)
        {
            var client = new SendGridClient(apiKey);
            var subject = "Verify your account";
            var to = new EmailAddress(toEmail);
            var plainTextContent = "You can verify your account here: https://localhost:3000/verify/" + token.ToString();
            var htmlContent = "<strong>" + plainTextContent + "</strong>";
            var msg = MailHelper.CreateSingleEmail(fromEmail, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }

        public async Task SendEmailToForgotPassword(string toEmail, Guid token)
        {
            var client = new SendGridClient(apiKey);
            var subject = "Forgot password";
            var to = new EmailAddress(toEmail);
            var plainTextContent = "You can change your password here: https://localhost:3000/reset-password/" + token.ToString();
            var htmlContent = "<strong>" + plainTextContent + "</strong>";
            var msg = MailHelper.CreateSingleEmail(fromEmail, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }

        public async Task SendEmailToNotifyOutbid(string toEmail, string itemName, float bid)
        {
            var client = new SendGridClient(apiKey);
            string subject = "You have been outbid!";
            var to = new EmailAddress(toEmail);
            var plainTextContent = "You have been outbid on " + itemName + " with a bid of " + bid + "!";
            var htmlContent = "<strong>" + plainTextContent + "</strong>";
            var msg = MailHelper.CreateSingleEmail(fromEmail, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }

        public async Task SendEmailToNotifyWon(string toEmail, string itemName, float bid)
        {
            var client = new SendGridClient(apiKey);
            var subject = "You have won an auction!";
            var to = new EmailAddress(toEmail);
            var plainTextContent = "You have won " + itemName + " with a bid of " + bid + "!";
            var htmlContent = "<strong>" + plainTextContent + "</strong>";
            var msg = MailHelper.CreateSingleEmail(fromEmail, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
