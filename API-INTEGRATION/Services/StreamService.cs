namespace APIIntegartion
{
    public class StreamService : IIntegrationService
    {
        private static HttpClient _httpClient = new HttpClient();

        public StreamService()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7033");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Run()
        {
            //await GetProductWithStream();
            await GetProductWithStreamAndCompletionMode();
        }

        public async Task GetProductWithStream()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/products/3");
            var response = await _httpClient.SendAsync(request);
            using (var stream = await response.Content.ReadAsStreamAsync()) 
            {
                response.EnsureSuccessStatusCode();
                    using (var streamReader = new StreamReader(stream))
                    {
                        using (var jsonReader = new JsonTextReader(streamReader))
                        {
                            var serializer = new Newtonsoft.Json.JsonSerializer();
                            var poster = serializer.Deserialize<IntegrationProduct>(jsonReader);
                        }
                    }
            }
        }
        public async Task GetProductWithStreamAndCompletionMode()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/products/3");
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                response.EnsureSuccessStatusCode();
                var poster = stream.ReadAndDeserializeFromJson<IntegrationProduct>();
            }
        }
    }
}