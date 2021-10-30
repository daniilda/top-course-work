using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TopCourseWorkBl.Infrastructure.Handlers.Common.UploadDataset
{
    public record UploadDatasetCommand(IFormFileCollection Files) : IRequest<IActionResult>;
}