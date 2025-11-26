using Hcb.Rnd.Pwn.Domain.Entities.Base;


namespace Hcb.Rnd.Pwn.Domain.Entities.Experiments;

public class ExperimentStatus : BaseEntity
{   
    public string Name { get; set; }

    public ICollection<Experiment> Experiments { get; set; } = [];
}
