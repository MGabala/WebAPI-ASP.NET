
namespace APIIntegartion
{
    public class CRUDService
    {
        private static HttpClient _httpClient = new HttpClient();

        public CRUDService()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7033");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Run()
        {
            await GetResource();
        }

        public async Task GetResource()
        {
            var response = await _httpClient.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<IEnumerable<IntegrationProduct>>(
                content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
        }
    }
}
