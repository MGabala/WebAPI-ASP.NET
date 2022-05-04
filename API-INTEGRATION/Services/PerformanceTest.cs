namespace APIIntegartion
{
    public class PerformanceTest : IIntegrationService
    {
        private static HttpClient _httpClient = new HttpClient();

        public PerformanceTest()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7033");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task Run()
        {
            await TESTGetProductWithStreamPerformanceTest();
        }
        public async Task GetProductWithStreamPerformanceTest()
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
        public async Task GetResourceVIAShortcutPerformanceTest()
        {
            var response = await _httpClient.GetAsync("/api/products/3");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var products = System.Text.Json.JsonSerializer.Deserialize<IntegrationProduct>(
                content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
        }
        public async Task GetProductWithStreamAndCompletionModePerformanceTest()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/products/3");
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                response.EnsureSuccessStatusCode();
                var poster = stream.ReadAndDeserializeFromJson<IntegrationProduct>();
            }
        }
        public async Task GetResourceVIAHttpRequestMessageTest()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/products/3");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var products = System.Text.Json.JsonSerializer.Deserialize<IntegrationProduct>(content,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
        }

        public async Task TESTGetProductWithStreamPerformanceTest()
        {
            var stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                await GetProductWithStreamPerformanceTest();
                
            }
            stopWatch.Stop();
            Console.WriteLine($"Time elapsed with stream: {stopWatch.ElapsedMilliseconds} ");

            stopWatch.Reset();
            stopWatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                await GetResourceVIAShortcutPerformanceTest();
               
            }
            stopWatch.Stop();
            Console.WriteLine($"Time elapsed via shortcut: {stopWatch.ElapsedMilliseconds}");
            stopWatch.Reset();
            stopWatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                await GetProductWithStreamAndCompletionModePerformanceTest();
                
            }
            stopWatch.Stop();
            Console.WriteLine($"Time elapsed with stream and completion mode: {stopWatch.ElapsedMilliseconds}");
            stopWatch.Reset();
            stopWatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                await GetResourceVIAHttpRequestMessageTest();
            }
            stopWatch.Stop();
            Console.WriteLine($"Time elapsed with HttpRequestMessage: {stopWatch.ElapsedMilliseconds}");
        }

    }
}