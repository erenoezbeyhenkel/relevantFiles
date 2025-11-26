using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Common;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Monitors.SubstrateFabricComposition;
using Hcb.Rnd.Pwn.Common.Extensions;
using Microsoft.Graph.Models;

namespace Hcb.Rnd.Pwn.Application.Common.Helpers;

public static class GeneralHelper
{
    public static async Task<string> GenerateInternalId(IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, CancellationToken cancellationToken)
    {
        var currentMonth = dateTimeProvider.Now.ToString("yyMM");
        var ordersCountOfTheMonth = await unitOfWork.Orders.CountAsync(o => o.InternalId.Contains(currentMonth), cancellationToken: cancellationToken);
        ordersCountOfTheMonth++;
        return $"W-{currentMonth}-{ordersCountOfTheMonth:D4}";
    }

    public static async Task<bool> ValidatePercentage(List<SubstrateFabricCompositionDto> fabricCompositionBasicDataDtos)
    {
        await Task.CompletedTask;

        if (!Guard.Against.IsAnyOrNotEmpty(fabricCompositionBasicDataDtos))
            return true;

        return fabricCompositionBasicDataDtos.Sum(item => item.Percentage) == 100m;
    }

    public static List<DirectoryObject> GetDevTeamMembers()
    {
        return
            [
            new User
            {
                UserPrincipalName = "enes.oeztuerk@henkel.com",
                DisplayName = "Enes ÖZTÜRK"
            },
            new User
            {
                UserPrincipalName = "eren.oezbey@henkel.com",
                DisplayName = "Eren ÖZBEY"
            }
            ];
    }

    public static List<DirectoryObject> GetWashOperatorDevTeamMembers()
    {
        return
            [
            new User
            {
                UserPrincipalName = "enes.oeztuerk@henkel.com",
                DisplayName = "Enes ÖZTÜRK"
            },
            new User
            {
                UserPrincipalName = "eren.oezbey@henkel.com",
                DisplayName = "Eren ÖZBEY"
            },
            new User
            {
                UserPrincipalName = "simon.hartwig@henkel.com",
                DisplayName = "Simon HARTWIG"
            }
            ];
    }
}
