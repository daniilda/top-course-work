using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace TopCourseWorkBl.DataLayer
{
    public class DbExecuteWrapper
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbExecuteWrapper(IDbConnectionFactory connectionFactory)
            => _connectionFactory = connectionFactory;
        
        public async Task<T> ExecuteQueryAsync<T>(string query, object? parameters = null, CancellationToken? cancellationToken = default)
        {
            await using var db = await _connectionFactory.CreateDatabaseAsync(cancellationToken);
            return await db.Connection.QueryFirstOrDefaultAsync<T>(db.CreateCommand(query, parameters));
        }
        
        public async Task ExecuteQueryAsync(string query, object? parameters = null, CancellationToken? cancellationToken = default)
        {
            await using var db = await _connectionFactory.CreateDatabaseAsync(cancellationToken);
            await db.Connection.QueryAsync(query, parameters);
        }
        
        public async Task<T[]> MultipleExecuteQueryAsync<T>(string query, object? parameters = null, CancellationToken? cancellationToken = default)
        {
            await using var db = await _connectionFactory.CreateDatabaseAsync(cancellationToken);
            return (await db.Connection.QueryAsync<T>(db.CreateCommand(query, parameters))).ToArray();
        }
    }
}