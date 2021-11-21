using System.Collections.Generic;
using JetBrains.Annotations;
using TopCourseWorkBl.Dtos;

namespace TopCourseWorkBl.BusinessLayer.Handlers.Common.GetGroupedAverage
{
    [UsedImplicitly]
    public record GetGroupedAverageResponse(List<GroupedTransactionByDay>? GroupedTransactions, PaginationResponse PaginationResponse);
}