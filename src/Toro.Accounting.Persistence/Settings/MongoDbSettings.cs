namespace Toro.Accounting.Persistence.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; init; }
        public string Port { get; init; }
        public string User { get; init; }
        public string Password { get; init; }
        public string ConnectionString => $"mongodb://{User}:{Password}@{Host}:{Port}";
    }
}
