namespace APIIntegartion
{
    public class HttpClientFactoryService : IIntegrationService
    {
        private readonly CancellationTokenSource cancellationToken = new CancellationTokenSource();
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientFactoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        public async Task Run()
        {
            cancellationToken.CancelAfter(15000);
            await GetProductWithHttpClientFactory(cancellationToken.Token);
        }

        private async Task GetProductWithHttpClientFactory(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7033/api/products/3");
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