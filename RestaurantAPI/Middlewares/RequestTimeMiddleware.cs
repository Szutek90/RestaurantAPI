using System.Diagnostics;

namespace RestaurantAPI.Middlewares
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private ILogger<RequestTimeMiddleware> _logger;
        private Stopwatch _stopwatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            var elapsedTime = _stopwatch.Elapsed.Seconds;
            if (elapsedTime > 4)
            {
                var message = $"Methode [{context.Request.Method}] for path [{context.Request.Path}] took [{elapsedTime}] seconds";
                _logger.LogInformation(message);
            }
        }
    }
}
