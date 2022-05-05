//-----------------------------------------------------------------------------------------------
// WEB.API INTEGRATION support CRUD actions with two scenarios. Shortcuts and HttpRequestMessage|
//-----------------------------------------------------------------------------------------------
namespace APIIntegartion
{
    internal class APIIntegration : CRUDService
    {
        static async Task Main(string[] args)
        {
            //var CRUDinstance = new CRUDService();
            //await CRUDinstance.Run();
            //var StreamInstance = new StreamService();
            //await StreamInstance.Run();
            //var PerformanceTestInstance = new PerformanceTest();
            //await PerformanceTestInstance.Run();
            var CancelationService = new CancelationService();
            await CancelationService.Run();
        }
    }
}