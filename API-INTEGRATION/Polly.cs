
namespace APIIntegartion
{
    public class Polly : IIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RetryPolicy _retryPolicy;
        public Polly()
        {
           
        }
        
        public Task Run()
        {
            throw new NotImplementedException();
        }
    }
}