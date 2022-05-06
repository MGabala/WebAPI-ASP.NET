namespace APIIntegartion
{
    public class HandlingErrorsAndFaults : IIntegrationService
    {
        private readonly CancellationTokenSource cancellationToken = new CancellationTokenSource();
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ProductClient _productClient;

        public HandlingErrorsAndFaults(IHttpClientFactory httpClientFactory, ProductClient productClient)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _productClient = productClient ?? throw new ArgumentNullException(nameof(productClient));
        }
        public async Task Run()
        {
            cancellationToken.CancelAfter(15000);
            await GetProductWithNamedHttpClientFactory(cancellationToken.Token);

        }

        private async Task GetProductWithNamedHttpClientFactory(CancellationToken cancellationToken)
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
                    if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("The requested product cannot be found.");
                        return;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        Console.WriteLine("The request cannot be authorized");
                        return;
                    }
                    response.EnsureSuccessStatusCode();
                }

                var product = stream.ReadAndDeserializeFromJson<IntegrationProduct>();
            }
        }
    }
}