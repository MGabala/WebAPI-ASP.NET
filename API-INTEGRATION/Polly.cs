
namespace APIIntegartion
{
    public class Polly : IIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RetryPolicy _retryPolicy;
        public Polly()
        {
            _retryPolicy = 
        }
        
        public Task Run()
        {
            throw new NotImplementedException();
        }
    }
}