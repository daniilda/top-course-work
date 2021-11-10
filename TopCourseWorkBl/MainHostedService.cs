using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TopCourseWorkBl.BackgroundTasksService;

namespace TopCourseWorkBl
{
    public class MainHostedService : BackgroundService
    {
        private readonly TaskStatusProcessor _taskStatusProcessor;
        private readonly BackgroundTaskProcessor _backgroundTaskProcessor;
        
        public MainHostedService(TaskStatusProcessor taskStatusProcessor, BackgroundTaskProcessor backgroundTaskProcessor)
        {
            _backgroundTaskProcessor = backgroundTaskProcessor;
            _taskStatusProcessor = taskStatusProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var backgroundTask = _backgroundTaskProcessor.StartProcessing(cancellationToken);
            var taskStatusTask = _taskStatusProcessor.StartProcessing(cancellationToken);

            await Task.WhenAll(backgroundTask, taskStatusTask);
        }

        
    }
}