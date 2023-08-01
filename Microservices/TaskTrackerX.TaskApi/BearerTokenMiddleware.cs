namespace TaskTrackerX.TaskApi
{
    public class BearerTokenMiddleware
    {
        private readonly RequestDelegate next;

        public BearerTokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                var token = authorizationHeader.FirstOrDefault()?.Split(" ").LastOrDefault();
                if (!string.IsNullOrEmpty(token))
                {
                    context.Items["BearerToken"] = token;
                }
            }

            await next(context);
        }
    }
}
