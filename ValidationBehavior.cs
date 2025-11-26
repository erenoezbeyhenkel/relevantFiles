using ErrorOr;
using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Extensions;
using Hcb.Rnd.Pwn.Application.Interfaces.Authentication;
using MediatR;
using Microsoft.Extensions.Logging;
using Hcb.Rnd.Pwn.Domain.Errors;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Common;

namespace Hcb.Rnd.Pwn.Application.Common.Behaviors;

/// <summary>
/// https://www.youtube.com/watch?v=FXP3PQ03fa0&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=8&ab_channel=AmichaiMantinband
/// Once Api sends the request to Application layer, first this validation behavior runs. If everything is valid then command or query handlers will be invoked.
/// If any validation error exist then this returns the validation errors with the explanations.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public sealed class ValidationBehavior<TRequest, TResponse>(ILogger<ValidationBehavior<TRequest, TResponse>> logger,
                                                            IDateTimeProvider dateTimeProvider,
                                                            IPwnHttpContextAccessor pwnHttpContextAccessor,
                                                            IValidator<TRequest> validator = null) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr

{
    public async Task<TResponse> Handle(TRequest request,
                                        RequestHandlerDelegate<TResponse> next,
                                        CancellationToken cancellationToken)
    {
        try
        {
            //Checks if there is any Validator for the respective request(for commands or queries).
            if (Guard.Against.IsNull(validator))
            {
                return await next(cancellationToken);
            }

            //Invokes the Fluent validation of the respective request(command or query)
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid)
            {
                return await next(cancellationToken);
            }

            //If there are errors the return them with the correct format of ErrorOr.
            var errors = validationResult.Errors.ConvertAll(validationFailure => Error.Validation(validationFailure.PropertyName,
                                                                                                  validationFailure.ErrorMessage));

            //Log block starts
            logger.LogInformation("------------------------------------------------------------------{@RequestName} START --------------------------------------------------------", typeof(TRequest).Name);

            //Set user email to trace
            logger.LogInformation("REQUESTOR USER EMAIL => {@UserEmail}", pwnHttpContextAccessor.UserEmail);
            var requestType = request.IsQuery() ? "QUERY" : "COMMAND";
            logger.LogWarning("COMPLETED REQUEST WITH VALIDATION ERRORS ({@RequestType}) => {@RequestName} =>, {@RequestTime}", requestType, typeof(TRequest).Name, dateTimeProvider.UtcNow);
            logger.LogWarning("VALIDATION ERRORS CONTENT => {@Errors}", errors);

            //Log block finish
            logger.LogInformation("------------------------------------------------------------------{@RequestName} FINISH --------------------------------------------------------", typeof(TRequest).Name);

            return (dynamic)errors;
        }
        catch (Exception ex)
        {
            return (dynamic)Errors.Infrastructure.InfrastructureError(ex?.InnerException?.Message ?? ex?.Message);
        }
    }
}
