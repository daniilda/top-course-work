using TopCourseWorkBl.Dtos;

namespace TopCourseWorkBl.DataLayer.Cmds.Main
{
    public record GetGroupedTransactionsByDayCmd(PaginationRequest Pagination);
}