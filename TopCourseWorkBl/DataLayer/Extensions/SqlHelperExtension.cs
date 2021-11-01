using Microsoft.Extensions.Configuration;

namespace TopCourseWorkBl.DataLayer
{
    public static class SqlHelperExtension
    {
        public static string GetPostgresConnectionString(this IConfiguration configuration)
        {
            var username = configuration["DB_USER"] ?? "root";
            var password = configuration["DB_PASSWORD"] ?? "root";
            var host = configuration["DB_HOST"] ?? "localhost";
            var port = configuration["DB_PORT"] ?? "5432";
            var database = configuration["DB_NAME"] ?? "database";
            var pooling = configuration["DB_POOLING"] ?? "true";
            var minPoolSize = configuration["DB_MIN_POOL_SIZE"] ?? "0";
            var maxPoolSize = configuration["DB_MAX_POOL_SIZE"] ?? "100";
            var connectionLifetime = configuration["DB_CONNECTION_LIFETIME"] ?? "0";
            
            return $"UserID={username};Password={password};Host={host};Port={port};" +
                   $"Database={database};Pooling={pooling};MinPoolSize={minPoolSize};" +
                   $"MaxPoolSize={maxPoolSize};ConnectionLifetime={connectionLifetime};";
        }
    }
}