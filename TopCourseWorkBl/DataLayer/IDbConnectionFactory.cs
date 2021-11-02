using System.Threading;
using System.Threading.Tasks;

namespace TopCourseWorkBl.DataLayer
{
    public interface IDbConnectionFactory
    {
        DatabaseWrapper CreateDatabase(CancellationToken? cancellationToken = default);
    }
}