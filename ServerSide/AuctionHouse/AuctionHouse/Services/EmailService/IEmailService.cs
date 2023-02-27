namespace AuctionHouse.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailToVerifyAsync(string toEmail, Guid token);

        Task SendEmailToForgotPassword(string toEmail, Guid token);

        Task SendEmailToNotifyOutbid(string toEmail, string itemName, float bid);

        Task SendEmailToNotifyWon(string toEmail, string itemName, float bid);
    }
}
