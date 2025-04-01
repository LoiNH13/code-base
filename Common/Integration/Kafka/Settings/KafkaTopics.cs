namespace Integration.Kafka.Settings;

public static class KafkaTopics
{
    public static class MotService
    {
        public const string MoMessage = "mo-message";

        public const string IotHubOtpSms = "IOTHub_OTP_SMS";
    }

    public static class OdooWorker
    {
        public const string KafkaCortexSmsBanking = "Cortex_Sms_Banking";
    }
}