using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Threading;

namespace LootTrade.IntegrationTests.TestFixtures
{
    public class MySqlDatabaseFixture : IDisposable
    {
        public string ConnectionString { get; private set; }
        private readonly string databaseName = "loottrade_test";

        public MySqlDatabaseFixture()
        {
            string host = GetEnv("MYSQL_HOST");
            string port = GetEnv("MYSQL_PORT");
            string user = GetEnv("MYSQL_USER");
            string password = GetEnv("MYSQL_PASSWORD");

            var serverConnection = new MySqlConnectionStringBuilder
            {
                Server = host,
                Port = uint.Parse(port),
                UserID = user,
                Password = password
            };

            // Wait until MySQL is ready (retry loop)
            bool connected = false;
            int retries = 10;
            while (!connected && retries > 0)
            {
                try
                {
                    using var conn = new MySqlConnection(serverConnection.ToString());
                    conn.Open();
                    connected = true;
                }
                catch
                {
                    retries--;
                    Thread.Sleep(3000);
                }
            }

            if (!connected)
                throw new InvalidOperationException("Could not connect to MySQL test container.");

            //using (var conn = new MySqlConnection(serverConnection.ToString()))
            //{
            //    conn.Open();
            //    new MySqlCommand($"DROP DATABASE IF EXISTS {databaseName};", conn)
            //        .ExecuteNonQuery();
            //    new MySqlCommand($"CREATE DATABASE {databaseName};", conn)
            //        .ExecuteNonQuery();
            //}

            ConnectionString = $"Server={host};Port={port};Database={databaseName};Uid={user};Pwd={password};";

            //RunSqlScript(Path.Combine(
            //    AppDomain.CurrentDomain.BaseDirectory,
            //    "Database",
            //    "schema.sql"
            //));

            SeedData();
        }

        private static string GetEnv(string name)
        {
            var value = Environment.GetEnvironmentVariable(name);
            if (string.IsNullOrWhiteSpace(value))
            {
                return name switch
                {
                    "MYSQL_HOST" => "127.0.0.1",
                    "MYSQL_PORT" => "3308",
                    "MYSQL_USER" => "loottrade_test",
                    "MYSQL_PASSWORD" => "testpassword",
                    _ => throw new InvalidOperationException($"Environment variable '{name}' is not set")
                };
            }
            return value;
        }

        private void RunSqlScript(string path)
        {
            string sql = File.ReadAllText(path);

            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            new MySqlCommand(sql, conn).ExecuteNonQuery();
        }

        private void SeedData()
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(@"
                INSERT INTO game (title) VALUES ('Game A'), ('Game B');
            ", conn);

            cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            string host = GetEnv("MYSQL_HOST");
            string port = GetEnv("MYSQL_PORT");
            string user = GetEnv("MYSQL_USER");
            string password = GetEnv("MYSQL_PASSWORD");

            using var conn = new MySqlConnection($"Server={host};Port={port};Uid={user};Pwd={password};");
            conn.Open();
            new MySqlCommand($"DROP DATABASE IF EXISTS {databaseName};", conn).ExecuteNonQuery();
        }
    }
}
