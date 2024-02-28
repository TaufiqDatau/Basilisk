using System.Net;
using System.Text.Json;

namespace Basilisk.Presentation.API.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next) { 
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case KeyNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case NullReferenceException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = new
                {
                    status = (int)response.StatusCode,
                    title = error.Message
                };

                response.WriteAsync(JsonSerializer.Serialize(result))
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
