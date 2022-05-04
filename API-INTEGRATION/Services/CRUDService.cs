
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
            //await CreateResourceVIAShortcut();
            //await RemoveResource();
            //await UpdateResource();
            //await GetResourceVIAShortcut();
            await PartialUpdateVIAHttpRequestMessage();
        }
        public async Task GetResourceVIAHttpRequestMessage()
        {

        }
        public async Task GetResourceVIAShortcut()
        {
            var response = await _httpClient.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var products = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<IntegrationProduct>>(
                content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
        }
        public async Task CreateResourceVIAHttpRequestMessage()
        {

        }
        public async Task CreateResourceVIAShortcut()
        {
            var product = new
            {

                Name = "Test from Integration v2",
                Desc = "Sample desc from Integration",
                Quantity = 16,
                Price = 65,
                Test = true

            };
            JsonContent content = JsonContent.Create(product);
            var response = await _httpClient.PostAsync("/api/products", content);
        }
        public async Task FullUpdateResourceVIAHttpRequestMessage()
        {

        }
        public async Task FullUpdateResourceVIAShortcut()
        {
            var productToUpdate = new
            {
                Id = 3,
                Name = "Full Update - Test from integration",
                Desc = "Full Update - Sample desc from Integration",
                Quantity = 16,
                Price = 65,
                Test = true
            };
            var response = await _httpClient.PostAsync(
                "/api/products", new StringContent(System.Text.Json.JsonSerializer.Serialize(productToUpdate),
                          Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            //var content = await response.Content.ReadAsStringAsync();
            //var create = System.Text.Json.JsonSerializer.Deserialize<IntegrationProduct>(content, new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //});
        }
        public async Task DeleteResourceVIAHttpRequestMessage()
        {

        }
        public async Task DeleteResourceVIAShortcut()
        {
            var request = await _httpClient.DeleteAsync("/api/products/3");
        }
        public async Task PartialUpdateVIAHttpRequestMessage()
        {
            var patch = new JsonPatchDocument<IntegrationProduct>();
            patch.Replace(x => x.Name, "Partial Update from Integraiton");
            patch.Remove(x => x.Desc);
            var serialize = JsonConvert.SerializeObject(patch);
            var request = new HttpRequestMessage(HttpMethod.Patch, "api/products/4");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(serialize);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        public async Task PartialUpdateVIAShortcut()
        {

        }
    }
}
