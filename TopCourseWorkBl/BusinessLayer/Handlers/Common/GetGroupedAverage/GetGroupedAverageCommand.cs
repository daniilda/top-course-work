using MediatR;
using TopCourseWorkBl.Dtos;

namespace TopCourseWorkBl.BusinessLayer.Handlers.Common.GetGroupedAverage
{
    public record GetGroupedAverageCommand(PaginationRequest PaginationRequest) : IRequest<GetGroupedAverageResponse>;
}