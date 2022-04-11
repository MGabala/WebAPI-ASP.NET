namespace WebAPI_ASP.NET6.Services;
public class MailService : IMailService
{
    private string mailTo = "example@example.com";
    private string mailFrom = "noreply@example.com";

    public void Send(string subject, string message)
    {
        Console.WriteLine($"Mail from {mailFrom} to {mailTo}, "+$"with {nameof(MailService)}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Message: {message}");
    }
}

