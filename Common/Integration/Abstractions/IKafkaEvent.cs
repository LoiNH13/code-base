namespace Integration.Abstractions;

public interface IKafkaEvent
{
    long TimeStamp { get; }

    HashSet<string> GetTopics();

    string? GetKey();
}