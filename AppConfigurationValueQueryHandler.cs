using ErrorOr;
using Hcb.Rnd.Pwn.Application.Common.Extensions;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.AppConfigurations.Value;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Errors;
using Microsoft.Extensions.Configuration;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.AppConfigurations.Value;

public sealed class AppConfigurationValueQueryHandler(IConfiguration configuration) : IQueryHandler<AppConfigurationValueQuery, AppConfigurationValueQueryResponse>
{
    public async Task<ErrorOr<AppConfigurationValueQueryResponse>> Handle(AppConfigurationValueQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var value = configuration[request.Key] ?? configuration.GetSection(request.Key).Get<string[]>().ToJson();
        if (Guard.Against.IsNullOrEmpty(value))
            return Errors.AppConfiguration.ValueDoesNotExist(request.Key);

        return new AppConfigurationValueQueryResponse(value);
    }
}
