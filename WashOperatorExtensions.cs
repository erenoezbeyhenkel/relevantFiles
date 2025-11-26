using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.Measurements;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions;
public static class WashOperatorExtensions
{
    public static bool IsAllSubmitted(this FrameMonitor sfm) =>
      IsAnyAssigned(sfm) && (!sfm.SubmitDate.Equals(DateTime.MinValue)) && (!Guard.Against.IsNull(sfm.SubmitDate));

    public static bool IsAnyAssigned(this FrameMonitor sfm) =>
       !(sfm.WashCycleSetup.WashCycle.Equals(0) ? Guard.Against.IsNullOrEmpty(sfm.Dmc) :
            Guard.Against.IsNullOrEmpty(sfm.WashingMachineMacAddress) || Guard.Against.IsNullOrEmpty(sfm.Dmc));
}
