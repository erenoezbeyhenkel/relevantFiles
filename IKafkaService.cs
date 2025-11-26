namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.Kafka;

public interface IKafkaService
{
    Task ProduceAsync<T>(string topic, T message, CancellationToken cancellationToken);
}
