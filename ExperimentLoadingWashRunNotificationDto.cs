namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Notifications.ExperimentLoadingWashRun;

public sealed record ExperimentLoadingWashRunNotificationDto(long ExperimentId,
                                                             long FrameMonitorId,
                                                             string WashRun);