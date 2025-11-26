using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using System.Text;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions;

public static class SubstrateExtensions
{
    public static string GetSubstrateIdentifier(this Substrate substrate)
    {
        var identifierBuilder = new StringBuilder();
        identifierBuilder.Append(substrate.Label);
        identifierBuilder.Append(" --- ");

        foreach (var substrateFabricComposition in substrate.SubstrateFabricCompositions)
        {
            identifierBuilder.Append(substrateFabricComposition.FabricComposition.Name.ToUpper());
            identifierBuilder.Append(" %: ");
            identifierBuilder.Append(substrateFabricComposition.Percentage);
        }

        return identifierBuilder.ToString();
    }
}
