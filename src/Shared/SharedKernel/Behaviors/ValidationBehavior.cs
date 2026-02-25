using System;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SharedKernel.CQRSStuff;

namespace SharedKernel.Behaviors;

public class ValidationBehavior<TRequest, TResponse> 
: IPipelineBehavior<TRequest, TResponse>
where TRequest : ICommand<TResponse> //where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationContext = new ValidationContext<TRequest>(request);
        var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(validationContext, cancellationToken)));
        var errors = results.Where(r => r.Errors.Any())
        .SelectMany(r => r.Errors)
        .ToList();
        if (errors.Any()) throw new ValidationException(errors);
        return await next(cancellationToken);
    }
}
