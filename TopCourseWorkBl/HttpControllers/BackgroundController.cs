using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopCourseWorkBl.BackgroundTasksService;
using TopCourseWorkBl.BusinessLayer.Extensions;

namespace TopCourseWorkBl.HttpControllers
{
    [ApiController]
    [Route("/v1/background-operations")]
    public class BackgroundController : ControllerBase
    {
        private readonly TasksProvider _tasks;

        public BackgroundController(TasksProvider tasks)
        {
            _tasks = tasks;
        }

        [Authorize]
        [HttpGet("get/tasks/{userId:long}")]
        public IActionResult GetTasks([FromQuery] long userId)
        {
            var tasks = _tasks.TasksQueue;
            return Ok(tasks.Where(x => x.CreatedBy == userId));
        }

        [Authorize]
        [HttpGet("get/tasks")]
        public IActionResult GetTasks()
        {
            var tasks = _tasks.TasksQueue;
            return Ok(tasks.Where(x => x.CreatedBy == Request.HttpContext.GetUserIdFromHttpContext()));
        }
        
        [Authorize]
        [HttpGet("get/all-tasks")]
        public IActionResult GetAllTasks()
        {
            var tasks = _tasks.TasksQueue;
            return Ok(tasks);
        }

        [Authorize]
        [HttpDelete("cancel/task/{taskId:int}")]
        public IActionResult CancelTask([FromRoute] int taskId)
        {
            return Ok(_tasks.CancelTask(taskId));
        }
        
        [Authorize]
        [HttpDelete("cancel/task/{userId:long}")]
        public IActionResult CancelTask([FromRoute] long userId)
        {
            return Ok(_tasks.CancelTask(userId));
        }
    }
}