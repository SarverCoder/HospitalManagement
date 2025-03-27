using Microsoft.Extensions.Options;

namespace HospitalManagement.Middlewares
{
    public class ValidationExceptionMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ValidationExceptionMiddleware(ILogger<ValidationExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (OptionsValidationException ex)
            {
                _logger.LogError($"Exception error {ex.Failures}");
            
            }
        }
    }
}
