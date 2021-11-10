using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TopCourseWorkBl.BusinessLayer.Handlers.Common.GetGroupedAverageCsv
{
    public record GetGroupedAverageCsvCommand : IRequest<FileContentResult>;
}