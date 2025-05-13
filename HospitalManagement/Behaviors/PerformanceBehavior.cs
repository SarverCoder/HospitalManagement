using System.Diagnostics;
using MediatR;

namespace HospitalManagement.Behaviors;

public class PerformanceBehavior<TRequest, TResponse> 
    (ILogger<PerformanceBehavior<TRequest,TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var timer = new Stopwatch();

        timer.Start();

        var response = await next(cancellationToken);

        timer.Stop();

        TimeSpan timeTaken = timer.Elapsed;

        string foo = "Time taken: " + timeTaken.Milliseconds.ToString();

        logger.LogInformation("Executed time {request}, executing time: {time} ms", typeof(TRequest).Name, foo);


        return response;

    }
}
