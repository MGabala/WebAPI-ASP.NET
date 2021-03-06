//--------------------------------------------------------------------------------------------------------------------------
//CRUD with Authorization by Policy with search & filter & pagination metadata. Documentation completed for sample methods.|
//--------------------------------------------------------------------------------------------------------------------------
namespace WebAPI_ASP.NET6.Services;

    public class CloudMailService : IMailService
    {
    private string mailTo = "example@example.com";
    private string mailFrom = "noreply@example.com";

    public void Send(string subject, string message)
    {
        Console.WriteLine($"Mail from {mailFrom} to {mailTo},"+ $" with {nameof(CloudMailService)}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Message: {message}");
    }
}