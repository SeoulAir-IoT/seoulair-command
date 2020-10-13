namespace SeoulAir.Command.Domain.Options
{
    public abstract class MqttConnectionOptions
    {
        public string BrokerAddress { get; set; }
        public int BrokerPort { get; set; }
        public string Topic { get; set; }
        public string SenderClientId { get; set; }
        public byte MaxNumOfAttemptsToSendMessage { get; set; }
    }
}
