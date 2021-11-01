using System.Threading;
using System.Threading.Tasks;

namespace TopCourseWorkBl.DataLayer
{
    public interface IDbConnectionFactory
    {
        Task<DatabaseWrapper> CreateDatabaseAsync(CancellationToken? cancellationToken = default);
    }
}