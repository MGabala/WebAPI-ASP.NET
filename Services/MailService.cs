namespace WebAPI_ASP.NET6.Services;
 public class MailService
    {
    private string mailTo = "example@example.com";
    private string mailFrom = "noreply@example.com";

    public void Send(string subject, string message)
    {
        Console.WriteLine($"Mail from {mailFrom} to {mailTo}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Message: {message}");
    }
    }

