using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TopCourseWorkBl.DataLayer.Cmds.Main;

namespace TopCourseWorkBl.DataLayer
{
    public class MainRepository
    {
        private readonly DbExecuteWrapper _dbExecuteWrapper;

        public MainRepository(DbExecuteWrapper dbExecuteWrapper)
            => _dbExecuteWrapper = dbExecuteWrapper;

        public async Task InsertTransactionsAsync(InsertTransactionsCmd insertCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"INSERT INTO {SqlConstants.TransactionsTable}
                            (customer_id, date, mcc_code, type, amount, terminal_id)
                            VALUES(@CustomerId, @DateTime, @TransactionMccCode, @TransactionType, @Amount, @TerminalId)";

            await _dbExecuteWrapper.ExecuteMultipliedQueryAsync(
                query, insertCmd.Transactions.Select(x=>
                        new
                        {
                            x.CustomerId,
                            x.DateTime,
                            x.TransactionMccCode,
                            x.TransactionType,
                            x.Amount,
                            x.TerminalId
                        })
                , cancellationToken);
        } 
        
        public async Task InsertMccCodesAsync(InsertMccCodesCmd insertCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"INSERT INTO {SqlConstants.MccCodesTable}
                            (code, description)
                            VALUES(@MccCodeId, @Description)";

            await _dbExecuteWrapper.ExecuteMultipliedQueryAsync(
                query, insertCmd.MccCodes.Select(x=>
                    new
                    {
                        x.MccCodeId,
                        x.Description
                    })
                , cancellationToken);
        } 
        
        public async Task InsertTypesAsync(InsertTypesCmd insertCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"INSERT INTO {SqlConstants.TypesTable}
                            (type, description)
                            VALUES(@TypeId, @Description)";

            await _dbExecuteWrapper.ExecuteMultipliedQueryAsync(
                query, insertCmd.Types.Select(x=>
                    new
                    {
                        x.TypeId,
                        x.Description
                    })
                , cancellationToken);
        } 
        
        public async Task InsertCustomersAsync(InsertCustomersCmd insertCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"INSERT INTO {SqlConstants.CustomersTable}
                            (id, gender)
                            VALUES(@CustomerId, @Gender)";

            await _dbExecuteWrapper.ExecuteMultipliedQueryAsync(
                query, insertCmd.Customers.Select(x=>
                    new
                    {
                        x.CustomerId,
                        x.Gender
                    })
                , cancellationToken);
        }

        public async Task CleanTablesWithDataAsync(CancellationToken cancellationToken)
        {
            var query = $@"DELETE FROM {SqlConstants.TransactionsTable};
                                DELETE FROM {SqlConstants.CustomersTable};
                                DELETE FROM {SqlConstants.TypesTable};
                                DELETE FROM {SqlConstants.MccCodesTable}";

            await _dbExecuteWrapper.ExecuteQueryAsync(query,
                cancellationToken);
        }
    }
}