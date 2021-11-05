using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TopCourseWorkBl.BusinessLayer.Handlers.Common.UploadDataset
{
    public record UploadDatasetCommand : IRequest<EmptyResult>
    {
        public IFormFileCollection Files { get; set; } = null!;
    }
        
}