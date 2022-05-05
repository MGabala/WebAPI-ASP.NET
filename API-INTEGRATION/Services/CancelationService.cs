namespace APIIntegartion
{
    public class CancelationService : IIntegrationService
    {
        private static HttpClient _httpClient = new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip
        });
        private CancellationTokenSource _cancelationToken = new CancellationTokenSource();
        public CancelationService()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7033");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task Run()
        {
           // _cancelationToken.CancelAfter(1000);
            //await GetResourceAndCancel(_cancelationToken.Token);
            await GetResourceAndHandleTimeOut();
        }
        private async Task GetResourceAndCancel(CancellationToken cancelationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/products/3");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            try
            {
                using (var response = await _httpClient.SendAsync(
               request, HttpCompletionOption.ResponseHeadersRead, cancelationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    response.EnsureSuccessStatusCode();
                    var product = stream.ReadAndDeserializeFromJson<IntegrationProduct>();
                }
            }
            catch(OperationCanceledException cancelException)
            {
                Console.WriteLine($"Operation was canceled with message '{cancelException.Message}'");
            }
        }
        private async Task GetResourceAndHandleTimeOut()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/products/3");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            try
            {
                using (var response = await _httpClient.SendAsync(
               request, HttpCompletionOption.ResponseHeadersRead))
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    response.EnsureSuccessStatusCode();
                    var product = stream.ReadAndDeserializeFromJson<IntegrationProduct>();
                }
            }
            catch (OperationCanceledException cancelException)
            {
                Console.WriteLine($"Operation was canceled with message '{cancelException.Message}'");
            }
        }
    }
}