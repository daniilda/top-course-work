using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopCourseWorkBl.Infrastructure.Handlers.Common.UploadDataset;

namespace TopCourseWorkBl.HttpControllers
{
    [ApiController]
    [Route("v1/common")]
    public class CommonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommonController(IMediator mediator) => _mediator = mediator;

        [HttpPost("upload-dataset")]
        public async Task<IActionResult> UploadDatasetAsync([FromQuery, Required] IFormFileCollection files)
            => await _mediator.Send(new UploadDatasetCommand(files));
        
    }
}