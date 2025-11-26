using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Monitors.Monitor;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Monitors.MonitorSubstrate;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Monitors.Substrate;
using Hcb.Rnd.Pwn.Common.Extensions;
using System.Collections.ObjectModel;

namespace Hcb.Rnd.Pwn.Common.Helpers;

public static class MonitorSubstratesHelper
{
    public static async Task<(ObservableCollection<MonitorSubstrateDto> monitorSubstrates, ObservableCollection<IGrouping<int, MonitorSubstrateDto>> groupedMonitorSubstrates)> SetUpMonitorSubstrates(MonitorDto monitor,
                                                                                                                                                                                                       ObservableCollection<SubstrateDto> substrates,
                                                                                                                                                                                                       List<MonitorSubstrateDto> monitorSubstrates)
    {
        await Task.CompletedTask;

        var changedMonitorSubstrates = new List<MonitorSubstrateDto>();

        // Update existing substrates with matching reference from the new list
        foreach (var ms in monitorSubstrates)
        {
            var updatedSubstrate = substrates?.FirstOrDefault(s => s.Id == ms.Substrate?.Id);
            changedMonitorSubstrates.Add(ms with { Substrate = updatedSubstrate });
        }

        int position = 1; // Start position counter

        for (int row = 1; row <= monitor.Row; row++)
        {
            for (int column = 1; column <= monitor.Column; column++)
            {
                // Check if a MonitorSubstrate already exists for this row/column
                var monitorSubstrate = changedMonitorSubstrates.FirstOrDefault(ms => ms.Row == row && ms.Column == column);

                if (Guard.Against.IsNull(monitorSubstrate))
                {
                    // No existing MonitorSubstrate — create a new one with null Substrate
                    monitorSubstrate = new MonitorSubstrateDto(monitor.Id, null, monitor, 0, column, row);
                    changedMonitorSubstrates.Add(monitorSubstrate);
                }

                // If there's a substrate, assign the next available position
                if (!Guard.Against.IsNull(monitorSubstrate?.Substrate))
                {
                    changedMonitorSubstrates.Remove(monitorSubstrate);
                    changedMonitorSubstrates.Add(monitorSubstrate with { Position = position });
                    position++; // Only increment when a substrate is present
                }
            }
        }

        var orderedSubstrates = changedMonitorSubstrates.OrderBy(ms => ms.Row).ThenBy(ms => ms.Column).ToList();
        var grouped = new ObservableCollection<IGrouping<int, MonitorSubstrateDto>>(orderedSubstrates.GroupBy(ms => ms.Row));

        return (new ObservableCollection<MonitorSubstrateDto>(orderedSubstrates), grouped);
    }

}