using Hcb.Rnd.Pwn.Domain.Entities.AzureGroups;
using Hcb.Rnd.Pwn.Domain.Entities.Base;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Domain.Entities.Products;

namespace Hcb.Rnd.Pwn.Domain.Entities.Orders;

public class Order : BaseEntity
{
    public string Description { get; set; }
    public string HugoProjectId { get; set; }
    public string InternalId { get; set; }
    public long ProductDeveloperAadGroupId { get; set; }
    public ProductDeveloperAadGroup ProductDeveloperAadGroup { get; set; }
    public ICollection<Product> Products { get; set; } = [];
    public ICollection<Experiment> Experiments { get; set; } = [];
}
