namespace WebAPI_ASP.NET6.Services
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}
//