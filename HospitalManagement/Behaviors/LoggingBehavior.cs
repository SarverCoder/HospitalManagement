using MediatR;

namespace HospitalManagement.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest,TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Executing request {Request}, {Time}" , typeof(TRequest).Name, DateTime.UtcNow);

        var response = await next(cancellationToken);

        logger.LogInformation("Executing request {Request}, {Time}", typeof(TRequest).Name, DateTime.UtcNow);

        return response;
    }
}
