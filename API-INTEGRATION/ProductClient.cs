namespace APIIntegartion
{
    public class ProductClient
    {
        public ProductClient(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }
        public HttpClient _HttpClient { get; }
    }
}