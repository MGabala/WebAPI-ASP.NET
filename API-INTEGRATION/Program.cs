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
           Console.ReadKey();
           await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(
                (serviceCollection) => ConfigureServices(serviceCollection));
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging(config => config.AddDebug().AddConsole());
            serviceCollection.AddHttpClient("API-INTEGRATION", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7033");
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();
            }).ConfigurePrimaryHttpMessageHandler(handler =>
                new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            serviceCollection.AddHttpClient<APIIntegration>().ConfigurePrimaryHttpMessageHandler(
                handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            serviceCollection.AddScoped<IIntegrationService, StreamService>();
        }
    }
}