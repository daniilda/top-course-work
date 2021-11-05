using Microsoft.Extensions.Configuration;
using static TopCourseWorkBl.EnvironmentConstants;

namespace TopCourseWorkBl.DataLayer.Extensions
{
    public static class SqlHelperExtension
    {
        public static string GetPostgresConnectionString(this IConfiguration configuration)
        {
            var username = configuration[DatabaseUser] ?? "root";
            var password = configuration[DatabasePassword] ?? "root";
            var host = configuration[DatabaseHost] ?? "localhost";
            var port = configuration[DatabasePort] ?? "5432";
            var database = configuration[DatabaseName] ?? "database";
            var pooling = configuration[DatabasePooling] ?? "true";
            var minPoolSize = configuration[DatabaseMinPoolSize] ?? "0";
            var maxPoolSize = configuration[DatabaseMaxPoolSize] ?? "100";
            var connectionLifetime = configuration[DatabaseConnectionLifetime] ?? "0";
            
            return $"UserID={username};Password={password};Host={host};Port={port};" +
                   $"Database={database};Pooling={pooling};MinPoolSize={minPoolSize};" +
                   $"MaxPoolSize={maxPoolSize};ConnectionLifetime={connectionLifetime}";
        }
    }
}