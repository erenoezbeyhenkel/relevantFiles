namespace Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;

public sealed class KafkaOptions
{
    public string BootstrapServer { get; set; }
    public string KafkaSaslPassword { get; set; }
    public string KafkaSaslUsername { get; set; }
    public string Topic { get; set; }
}
