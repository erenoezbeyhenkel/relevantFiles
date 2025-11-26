namespace Hcb.Rnd.Pwn.Domain.Entities.Base;

public interface IBaseEntity
{
    long Id { get; set; }
    string CreatedById { get; set; }
    DateTime CreatedAt { get; set; }
    string UpdatedById { get; set; }
    DateTime UpdatedAt { get; set; }
}
