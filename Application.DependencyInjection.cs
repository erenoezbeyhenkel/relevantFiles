using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Hcb.Rnd.Pwn.Application.DI;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //MediatR injection.
        services.AddMediatR(configuration =>
        {
            //This extension scans all the assembly and find out the Commands, Queries and Handles etc.
            //No need to inject all our newly created Handlers etc. This like takes care of the injection.
            configuration.RegisterServicesFromAssemblies(AssemblyReference.Assembly);
        });

        //We are defining here our behaviors.
        //Order of the injection defines the order of behavior execution.
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));


        //It scans all the Abstract Validators from Assembly and inject them. This means we do not need to inject our validators manually.
        //We are using base validator and it extends Abstract Validator. This means that all validator also extends Abstract Validator. 
        //BaseValidator<TValidationObject> : AbstractValidator<TValidationObject> 
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

        return services;
    }
}
