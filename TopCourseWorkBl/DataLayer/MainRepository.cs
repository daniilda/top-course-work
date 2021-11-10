using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TopCourseWorkBl.DataLayer.Cmds.Main;
using TopCourseWorkBl.Dtos;

namespace TopCourseWorkBl.DataLayer
{
    public class MainRepository
    {
        private readonly DbExecuteWrapper _dbExecuteWrapper;

        public MainRepository(DbExecuteWrapper dbExecuteWrapper)
            => _dbExecuteWrapper = dbExecuteWrapper;

        public async Task InsertTransactionsBulkAsync(InsertTransactionsCmd insertCmd,
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

        public async Task InsertMccCodesBulkAsync(InsertMccCodesCmd insertCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"INSERT INTO {SqlConstants.MccCodesTable}
                            (code, description)
                            VALUES(@MccCodeId, @Description)";
            
            await _dbExecuteWrapper.ExecuteMultipliedQueryAsync(
                query, insertCmd.MccCodes.Select(x =>
                    new
                    {
                        x.MccCodeId,
                        x.Description
                    })
                , cancellationToken);
        }

        public async Task InsertTypesBulkAsync(InsertTypesCmd insertCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"INSERT INTO {SqlConstants.TypesTable}
                            (type, description)
                            VALUES(@TypeId, @Description)";

            await _dbExecuteWrapper.ExecuteMultipliedQueryAsync(
                query, insertCmd.Types.Select(x =>
                    new
                    {
                        x.TypeId,
                        x.Description
                    })
                , cancellationToken);
        }

        public async Task InsertCustomersBulkAsync(InsertCustomersCmd insertCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"INSERT INTO {SqlConstants.CustomersTable}
                            (id, gender)
                            VALUES(@CustomerId, @Gender)";

            await _dbExecuteWrapper.ExecuteMultipliedQueryAsync(
                query, insertCmd.Customers.Select(x =>
                    new
                    {
                        x.CustomerId,
                        x.Gender
                    })
                , cancellationToken);
        }
        
        public async Task DeleteDataFromTransactionsAsync(CancellationToken cancellationToken)
        {
            var query = $@"DELETE FROM {SqlConstants.TransactionsTable};";

            await _dbExecuteWrapper.ExecuteQueryAsync(query,
                cancellationToken);
        }

        public async Task DeleteDataFromCustomersAsync(CancellationToken cancellationToken)
        {
            var query = $@"DELETE FROM {SqlConstants.CustomersTable};";

            await _dbExecuteWrapper.ExecuteQueryAsync(query,
                cancellationToken);
        }
        
        public async Task DeleteDataFromMccCodesAsync(CancellationToken cancellationToken)
        {
            var query = $@"DELETE FROM {SqlConstants.MccCodesTable};";

            await _dbExecuteWrapper.ExecuteQueryAsync(query,
                cancellationToken);
        }
        
        public async Task DeleteDataFromTypesAsync(CancellationToken cancellationToken)
        {
            var query = $@"DELETE FROM {SqlConstants.TypesTable};";

            await _dbExecuteWrapper.ExecuteQueryAsync(query,
                cancellationToken);
        }
        

        public async Task<List<GroupedTransactionByDay>> GetGroupedTransactionsByDay(
            CancellationToken cancellationToken)
        {
            var query = $@"SELECT DATE(date) DateTime, 
                            mcc_code MccCode,
                            AVG(amount) AverageAmount                    
                            FROM {SqlConstants.TransactionsTable}
                            GROUP BY DATE(date), mcc_code
                            HAVING (SELECT COUNT(mcc_code) FROM {SqlConstants.TransactionsTable}) > 60000
                            ORDER BY DateTime;";
            var result =
                await _dbExecuteWrapper.MultipleExecuteQueryAsync<GroupedTransactionByDay>(query, cancellationToken);
            return result.ToList();
        }
        
        public async Task<GetGroupedTransactionsByDayCmdResponse> GetGroupedTransactionsByDay(GetGroupedTransactionsByDayCmd getCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"SELECT DATE(date) DateTime, 
                            mcc_code MccCode,
                            AVG(amount) AverageAmount                    
                            FROM {SqlConstants.TransactionsTable}
                            GROUP BY DATE(date), mcc_code
                            HAVING (SELECT COUNT(mcc_code) FROM {SqlConstants.TransactionsTable}) > 60000
                            ORDER BY DateTime DESC
                            LIMIT @Count
                            OFFSET @Offset;";
            var countQuery = $@"SELECT COUNT(*) FROM {SqlConstants.TransactionsTable}";
            
            var result =
                await _dbExecuteWrapper.MultipleExecuteQueryAsync<GroupedTransactionByDay>(query,
                    new
                    {
                        getCmd.Pagination.Count,
                        getCmd.Pagination.Offset
                    },
                    cancellationToken);
            var countResult =
                await _dbExecuteWrapper.ExecuteQueryAsync<int>(countQuery,
                    cancellationToken);
            return new GetGroupedTransactionsByDayCmdResponse(result.ToList(), new PaginationResponse(countResult));
        }
    }
}