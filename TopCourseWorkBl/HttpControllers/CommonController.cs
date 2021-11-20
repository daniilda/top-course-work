using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TopCourseWorkBl.AuthenticationLayer.Exceptions;
using TopCourseWorkBl.BusinessLayer.Handlers.Common.GetGroupedAverage;
using TopCourseWorkBl.BusinessLayer.Handlers.Common.GetGroupedAverageCsv;
using TopCourseWorkBl.BusinessLayer.Handlers.Common.UploadDataset;
using TopCourseWorkBl.Dtos;

namespace TopCourseWorkBl.HttpControllers
{
    [ApiController]
    [Route("/v1/common")]
    public class CommonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommonController(IMediator mediator)
            => _mediator = mediator;

      //  [Authorize]
        [Produces("application/json")]
        [HttpGet("get/grouped-average/{count:int}/{offset:int}")]
        public async Task<ActionResult<GroupedTransactionByDay>> GetAverageGroup([FromRoute]int count, int offset)
            => Ok(await _mediator.Send(new GetGroupedAverageCommand(new PaginationRequest(count,offset))));
        
      //  [Authorize]
        [Produces("application/csv")]
        [HttpGet("get/grouped-average/csv")]
        public async Task<IActionResult> GetAverageGroupCsv()
            => await _mediator.Send(new GetGroupedAverageCsvCommand());


       // [Authorize]
        [DisableRequestSizeLimit] //TODO: All logic in this handle with a huge memory issue!!!!
        [HttpPost("upload/dataset-bulk")]
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