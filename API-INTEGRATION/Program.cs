using System;

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