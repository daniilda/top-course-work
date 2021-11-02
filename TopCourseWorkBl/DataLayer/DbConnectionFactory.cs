using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TopCourseWorkBl.DataLayer.Extensions;

namespace TopCourseWorkBl.DataLayer
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly HttpCancellationTokenAccessor _cancellationTokenAccessor;
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(
            HttpCancellationTokenAccessor cancellationTokenAccessor,
            IConfiguration configuration)
        {
            _cancellationTokenAccessor = cancellationTokenAccessor;
            _configuration = configuration;
        }

        public DatabaseWrapper CreateDatabase(CancellationToken? cancellationToken = default)
            => new(
                 new NpgsqlConnection(_configuration.GetPostgresConnectionString()),
                 cancellationToken ?? _cancellationTokenAccessor.Token);

    }
}