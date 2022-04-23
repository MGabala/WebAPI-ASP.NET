namespace WebAPI_ASP.NET6.Services;
public class MailService : IMailService
{
    private readonly string _mailTo = String.Empty;
    private readonly string _mailFrom = String.Empty;

    public MailService(IConfiguration configuration)
    {
        _mailTo = configuration["mailSettings:mailTo"];
        _mailFrom = configuration["mailSettings:mailFrom"];
    }
    public void Send(string subject, string message)
    {
        Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, "+$"with {nameof(MailService)}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Message: {message}");
    }
}

//