using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.AppConfigurations.Value;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.AppConfigurations.Value;

public sealed class AppConfigurationValueQueryValidator : BaseValidator<AppConfigurationValueQuery>
{
    public AppConfigurationValueQueryValidator()
    {
        RuleFor(x => x.Key).NotEmpty();
    }
}
