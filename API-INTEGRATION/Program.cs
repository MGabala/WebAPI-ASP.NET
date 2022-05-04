//-----------------------------------------------------------------------------------------------
// WEB.API INTEGRATION support CRUD actions with two scenarios. Shortcuts and HttpRequestMessage|
//-----------------------------------------------------------------------------------------------
namespace APIIntegartion
{
    internal class APIIntegration : CRUDService
    {
        static async Task Main(string[] args)
        {
            var instance = new CRUDService();
            await instance.Run();
        }
    }
}