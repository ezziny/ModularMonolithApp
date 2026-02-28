using System;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.CQRSStuff;

namespace SharedKernel.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull, IRequest<TResponse> //IRequest not just ICommand or IQuery as we need to log all 
where TResponse : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling request {request} with response {response} with data {data}", typeof(TRequest).Name, typeof(TResponse).Name, request);
        var response = await next(cancellationToken);
        _logger.LogInformation("Handled request {request} with response {response}", typeof(TRequest).Name, typeof(TResponse).Name);
        return response;
    }
}
