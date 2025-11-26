using Hcb.Rnd.Pwn.Domain.Entities.Additives;
using Hcb.Rnd.Pwn.Domain.Entities.Base;

namespace Hcb.Rnd.Pwn.Domain.Entities.Experiments;

public class ProductSetupAdditive : BaseEntity
{
    public long ProductSetupId { get; set; }
    public ProductSetup ProductSetup { get; set; }

    public long AdditiveTypeId { get; set; }
    public AdditiveType AdditiveType { get; set; }


    public decimal Amount { get; set; }
    public bool IsProductDependent { get; set; }
}
