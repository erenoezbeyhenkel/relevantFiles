using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Hcb.Rnd.Pwn.Application.Interfaces.Authentication;
using Hcb.Rnd.Pwn.Domain.Entities.Base;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Common;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.Interceptors;

/// <summary>
/// Once we add or update any entity, we should define our common actions here.
/// 
/// https://www.milanjovanovic.tech/blog/how-to-use-ef-core-interceptors
/// 
/// https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/interceptors
/// </summary>
/// <param name="pwnHttpContextAccessor"></param>
/// <param name="dateTimeProvider"></param>
internal sealed class UpdateBaseEntityInterceptor(IPwnHttpContextAccessor pwnHttpContextAccessor,
                                                  IDateTimeProvider dateTimeProvider) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
                                                                          InterceptionResult<int> result,
                                                                          CancellationToken cancellationToken = default)
    {
        if (!Guard.Against.IsNull(eventData.Context))
            UpdateBaseEntity(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    //Update rules stay here.
    private void UpdateBaseEntity(DbContext context)
    {
        var entities = context.ChangeTracker.Entries<IBaseEntity>().ToList();

        foreach (EntityEntry<IBaseEntity> entry in entities)
        {
            if (entry.State == EntityState.Added)
            {
                if (!Guard.Against.IsNullOrEmpty(pwnHttpContextAccessor.UserId))
                {
                    SetCurrentPropertyValue(entry, nameof(IBaseEntity.CreatedById), pwnHttpContextAccessor.UserId);
                    SetCurrentPropertyValue(entry, nameof(IBaseEntity.UpdatedById), pwnHttpContextAccessor.UserId);
                }

                SetCurrentPropertyValue(entry, nameof(IBaseEntity.CreatedAt), dateTimeProvider.UtcNow);
                SetCurrentPropertyValue(entry, nameof(IBaseEntity.UpdatedAt), dateTimeProvider.UtcNow);
            }

            if (entry.State == EntityState.Modified)
            {
                SetCurrentPropertyValue(entry, nameof(IBaseEntity.UpdatedById), pwnHttpContextAccessor.UserId);
                SetCurrentPropertyValue(entry, nameof(IBaseEntity.UpdatedAt), dateTimeProvider.UtcNow);


                //Todo: Once we want to update nested objects, base properties of the nested objects are setting null. We should ignore that mapping.
                //This is the workaround to set that null values. Need to handle this case like first lever objects ignorence.
                if (entry.Entity.CreatedAt == default)
                    SetCurrentPropertyValue(entry, nameof(IBaseEntity.CreatedAt), dateTimeProvider.UtcNow);

                if (entry.Entity.CreatedById == default)
                    SetCurrentPropertyValue(entry, nameof(IBaseEntity.CreatedById), pwnHttpContextAccessor.UserId);

            }
        }

        static void SetCurrentPropertyValue<T>(EntityEntry entry,
                                               string propertyName,
                                               T utcNow) => entry.Property(propertyName).CurrentValue = utcNow;
    }
}
