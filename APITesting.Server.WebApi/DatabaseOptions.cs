using Npgsql;

namespace APITesting;

public sealed class DatabaseOptions
{
    public const string ConfigurationKey = "Database";
    
    public string? Host { get; set; }
    public int? Port { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string?  Database { get; set; }

    public string CreateConnectionString() => new NpgsqlConnectionStringBuilder
    {
        Host = this.Host,
        Port = this.Port ?? 5432,
        Username = this.Username,
        Password = this.Password,
        Database = this.Database,
    }.ConnectionString;
}