namespace City_Vibe.Interfaces
{
    public interface ISendGridEmail
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
