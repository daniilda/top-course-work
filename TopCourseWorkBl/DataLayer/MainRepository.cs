using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TopCourseWorkBl.DataLayer.Dto.Cmds;

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
            if (insertCmd.Transactions == null)
                return;
            
            await _dbExecuteWrapper.ExecuteQueryAsync(
                query, insertCmd.Transactions!.Select(x=>
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
            if (insertCmd.MccCodes == null)
                return;

            await _dbExecuteWrapper.ExecuteQueryAsync(
                query, insertCmd.MccCodes!.Select(x=>
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
            if (insertCmd.Types == null)
                return;
            
            await _dbExecuteWrapper.ExecuteQueryAsync(
                query, insertCmd.Types!.Select(x=>
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

            if (insertCmd.Customers == null)
                return;
            
            await _dbExecuteWrapper.ExecuteQueryAsync(
                query, insertCmd.Customers.Select(x=>
                    new
                    {
                        x.CustomerId,
                        x.Gender
                    })
                , cancellationToken);
        } 

    }
}