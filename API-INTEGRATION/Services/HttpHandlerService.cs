namespace APIIntegartion
{
    public class HttpHandlerService : IIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private CancellationTokenSource _cancelationTokenSource = new CancellationTokenSource();
         public HttpHandlerService(IHttpClientFactory httpClientFactory, ProductClient client)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            
        }
        public async Task Run()
        {
            await GetProductWithRetryPolicy(_cancelationTokenSource.Token);
        }

        private async Task GetProductWithRetryPolicy(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("API-INTEGRATION");
            var request = new HttpRequestMessage(HttpMethod.Get, "api/products/666");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("The requested product cannot be found.");
                        return;
                    }
                    response.EnsureSuccessStatusCode();
                }

                var product = stream.ReadAndDeserializeFromJson<IntegrationProduct>();
            }
        }
    }
}