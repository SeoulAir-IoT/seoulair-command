namespace SeoulAir.Command.Domain.Options
{
    public class MqttNotificationsConnectionOptions : MqttConnectionOptions
    {
        public static string AppSettingsPath { get; protected set; }
            = "MqttOptions:NotificationsConnection";
    }
}
