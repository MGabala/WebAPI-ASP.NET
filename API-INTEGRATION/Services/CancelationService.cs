namespace APIIntegartion
{
    public class CancelationService : IIntegrationService
    {
        private static HttpClient _httpClient = new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip
        });
        public CancelationService()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7033");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task Run()
        {
            await GetResourceAndCancel();
        }
        private async Task GetResourceAndCancel()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/products/3");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            var cancelationTokenSource = new CancellationTokenSource();
            cancelationTokenSource.CancelAfter(1000);
            using (var response = await _httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead, cancelationTokenSource.Token))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                var product = stream.ReadAndDeserializeFromJson<IntegrationProduct>();
            }
        }
    }
}