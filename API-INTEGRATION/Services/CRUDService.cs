//-----------------------------------------------------------------------------------------------
// WEB.API INTEGRATION support CRUD actions with two scenarios. Shortcuts and HttpRequestMessage|
//-----------------------------------------------------------------------------------------------
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
            //await GetResourceVIAHttpRequestMessage();
           //GetResourceVIAShortcut();
           //CreateResourceVIAHttpRequestMessage();
           //CreateResourceVIAShortcut();
           //await FullUpdateResourceVIAHttpRequestMessage();
           //FullUpdateResourceVIAShortcut();
           //await DeleteResourceVIAHttpRequestMessage();
           //DeleteResourceVIAShortcut();
           //PartialUpdateVIAHttpRequestMessage();
           //await PartialUpdateVIAShortcut();
        }
        public async Task GetResourceVIAHttpRequestMessage()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/products");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var products = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<IntegrationProduct>>(content,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
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
            var product = new
            {
                Name = "Test from created resources",
                Desc = "via httprequestmessage",
                Quantity = 16,
                Price = 65,
                Test = true
            };
            var serialize = System.Text.Json.JsonSerializer.Serialize(product);
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/products");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(serialize);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
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
            var productToUpdate = new
            {
                Name = "Full Update - Test from integration v3",
                Desc = "Full Update - Sample desc from Integration v3",
                Quantity = 16,
                Price = 65,
                Test = true
            };
            var serialize = System.Text.Json.JsonSerializer.Serialize(productToUpdate);
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/products/3");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(serialize);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
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
        }
        public async Task DeleteResourceVIAHttpRequestMessage()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/products/5");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responce = await _httpClient.SendAsync(request);
            responce.EnsureSuccessStatusCode();
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
            var patch = new JsonPatchDocument<IntegrationProduct>();
            patch.Replace(x => x.Desc, "Updated from Shortcut");
            var response = await _httpClient.PatchAsync("/api/products/4",
                new StringContent(
                    JsonConvert.SerializeObject(patch), Encoding.UTF8, "application/json-patch+json"));
            response.EnsureSuccessStatusCode();
        }
    }
}
