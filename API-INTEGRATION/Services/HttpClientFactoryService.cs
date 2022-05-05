namespace APIIntegartion
{
    public class HttpClientFactoryService : IIntegrationService
    {
        private readonly CancellationTokenSource cancellationToken = new CancellationTokenSource();
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ProductClient _productClient;

        public HttpClientFactoryService(IHttpClientFactory httpClientFactory, ProductClient productClient)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _productClient = productClient ?? throw new ArgumentNullException(nameof(productClient));
        }
        public async Task Run()
        {
            //cancellationToken.CancelAfter(15000);
            //await GetProductWithNamedHttpClientFactory(cancellationToken.Token);
            cancellationToken.CancelAfter(15000);
            await GetProductWithGenericHttpClientFactory(cancellationToken.Token);
        }

        private async Task GetProductWithNamedHttpClientFactory(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("API-INTEGRATION");
            var request = new HttpRequestMessage(HttpMethod.Get, "api/products/3");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                var product = stream.ReadAndDeserializeFromJson<IntegrationProduct>();
            }
        }
        private async Task GetProductWithGenericHttpClientFactory(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("Generic Use of API-INTEGRATION");
            var request = new HttpRequestMessage(HttpMethod.Get, "api/products/3");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                var product = stream.ReadAndDeserializeFromJson<IntegrationProduct>();
            }
        }
    }
}