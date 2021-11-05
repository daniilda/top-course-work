using TopCourseWorkBl.DataLayer.Dto;

namespace TopCourseWorkBl.DataLayer.Cmds.Main
{
    public record InsertTransactionsCmd(Transaction[]? Transactions);
}