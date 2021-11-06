using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopCourseWorkBl.AuthenticationLayer.Exceptions;
using TopCourseWorkBl.BusinessLayer.Handlers.Common.UploadDataset;

namespace TopCourseWorkBl.HttpControllers
{
    [ApiController]
    [Route("/v1/common")]
    public class CommonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommonController(IMediator mediator)
            => _mediator = mediator;

        [Authorize]
        [HttpPost("upload-dataset")]
        public async Task<IActionResult> UploadDatasetAsync([FromQuery, Required] UploadDatasetCommand request)
        {
            try
            {
                await _mediator.Send(request);
                return Ok();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
    }
}