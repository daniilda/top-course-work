using System.Collections.Generic;
using TopCourseWorkBl.Dtos;

namespace TopCourseWorkBl.DataLayer.Cmds.Main
{
    public record GetGroupedTransactionsByDayCmdResponse(List<GroupedTransactionByDay> Transactions, PaginationResponse PaginationResponse);
}