//-----------------------------------------------------------------------------------------------
// WEB.API INTEGRATION support CRUD actions with two scenarios. Shortcuts and HttpRequestMessage|
//-----------------------------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIIntegartion
{
    internal class APIIntegration 
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            var serviceProvider = host.Services;
            try
            {
                var log = host.Services.GetRequiredService<ILogger<APIIntegration>>();
                log.LogInformation("Host created");
                await serviceProvider.GetService<IIntegrationService>().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(
                (serviceCollection) => ConfigureServices(serviceCollection));
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            throw new NotImplementedException();
        }
    }
}