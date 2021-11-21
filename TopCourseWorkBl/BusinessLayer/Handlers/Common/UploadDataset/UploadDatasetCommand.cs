using MediatR;
using Microsoft.AspNetCore.Http;

namespace TopCourseWorkBl.BusinessLayer.Handlers.Common.UploadDataset
{
    public record UploadDatasetCommand : IRequest
    {
        public IFormFileCollection Files { get; set; } = null!;
        
    }
        
}