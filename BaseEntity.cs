namespace Hcb.Rnd.Pwn.Domain.Entities.Base;

/// <summary>
/// Our base entity which stores the base properties for each tables.
/// </summary>
public class BaseEntity : IBaseEntity
{
    public long Id { get; set; }
    public string CreatedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UpdatedById { get; set; }
    public DateTime UpdatedAt { get; set; }
}
