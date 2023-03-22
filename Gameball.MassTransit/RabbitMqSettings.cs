namespace Gameball.MassTransit
{
    public class RabbitMqSettings
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Protocol { get; set; }
        public string Port { get; set; }
        public string EngineAppType { get; set; }
        public string NotificationAppType { get; set; }
    }
}
