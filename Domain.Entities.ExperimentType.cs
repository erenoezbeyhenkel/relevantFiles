using Hcb.Rnd.Pwn.Domain.Entities.Base;

namespace Hcb.Rnd.Pwn.Domain.Entities.Experiments
{
    public class ExperimentType : BaseEntity
    {
        public string Name { get; set; }
        public string WorkInstructionPath { get; set; }
        public ICollection<Experiment> Experiments { get; set; } = [];
    }
}
