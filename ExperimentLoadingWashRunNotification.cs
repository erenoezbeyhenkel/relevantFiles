using MediatR;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Notifications.ExperimentLoadingWashRun;

public sealed record ExperimentLoadingWashRunNotification(List<ExperimentLoadingWashRunNotificationDto> ExperimentLoadingWashRunNotificationDtos) : INotification;