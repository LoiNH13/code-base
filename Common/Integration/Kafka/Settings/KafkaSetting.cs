using Confluent.Kafka;

namespace Integration.Kafka.Settings;

public class KafkaSetting
{
    public const string SettingKey = "Kafka";

    public string BootstrapServers { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Group { get; set; } = null!;

    public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Earliest;

    public SecurityProtocol SecurityProtocol { get; set; } = SecurityProtocol.SaslPlaintext;

    public SaslMechanism SaslMechanism { get; set; } = SaslMechanism.Plain;

    public ConsumerConfig BuildDefaultConfig() => new ConsumerConfig
    {
        BootstrapServers = BootstrapServers,
        GroupId = Group,
        SaslUsername = Username,
        SaslPassword = Password,
        SecurityProtocol = SecurityProtocol,
        SaslMechanism = SaslMechanism,
        AutoOffsetReset = AutoOffsetReset
    };
}