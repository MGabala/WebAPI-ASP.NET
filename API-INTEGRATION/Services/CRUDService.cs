﻿namespace APIIntegartion
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
            //await GetResource();
            //await CreateResource();
            //await FullUpdateResource();
            await DeleteResource();
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
        public async Task CreateResource()
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
        public async Task FullUpdateResource()
        {
            var productToUpdate = new
            {
                Name = "Full Update - Test from integration",
                Desc = "Full Update - Sample desc from Integration",
                Quantity = 16,
                Price = 65,
                Test = true
            };
            var serialize = JsonSerializer.Serialize(productToUpdate);

            var request = new HttpRequestMessage(HttpMethod.Put, "api/products/3");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(serialize);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var update = JsonSerializer.Deserialize<IntegrationProduct>(content,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

        }
        public async Task DeleteResource()
        {
            var request = await _httpClient.DeleteAsync("/api/products/3");
        }
    }
}
