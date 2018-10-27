using System;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using SQLite.CodeFirst;
using SstRegistrationTestHarness.Core.EntityFramework;
using SstRegistrationTestHarness.Core.EntityFramework.Persistence;

namespace SstRegistrationTestHarness.Core.Test
{
    public class TestFluentConfigurer : IFluentConfigurer
    {
        public Action<string> LogAction { get; }
        public Type ContextType => typeof(SstRegistrationTestHarnessSystemContext);
        
        public TestFluentConfigurer()
        {
            LogAction = message =>
            {
                var logFile = File.AppendText("EFLogs.txt");
                logFile.WriteLine($"{DateTime.UtcNow:yyyy/MM/dd hh:mm:ss} {message}");
                logFile.Close();
            };
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = new SQLiteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SQLiteConnection(connectionString.ToString());
            connection.Open();
            return connection;
        }

        public IDatabaseInitializer<TDbContext> CreateInitializer<TDbContext>(DbModelBuilder modelBuilder) where TDbContext : DbContext
        {
            return new SqliteDropCreateDatabaseAlways<TDbContext>(modelBuilder);
        }
    }
}