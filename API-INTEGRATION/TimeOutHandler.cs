namespace APIIntegartion
{
    public class TimeOutHandler : DelegatingHandler
    {
        private readonly TimeSpan _timeOut = TimeSpan.FromSeconds(100);
        public TimeOutHandler(TimeSpan timeout) : base()
        {
            _timeOut = timeout;
        }
        public TimeOutHandler(HttpMessageHandler innerHandler, TimeSpan timeOut) : base(innerHandler)
        {
            _timeOut = timeOut;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
           using(var _timeOutToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                _timeOutToken.CancelAfter(_timeOut);
                try
                {
                    return await base.SendAsync(request, _timeOutToken.Token);
                }
                catch (OperationCanceledException exception)
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        throw new TimeoutException("The request timed out.", exception);
                    }
                    throw;
                }
            }
        }
    }
}