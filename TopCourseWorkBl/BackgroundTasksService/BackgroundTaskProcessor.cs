using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskStatus = TopCourseWorkBl.Enums.TaskStatus;

namespace TopCourseWorkBl.BackgroundTasksService
{
    public class BackgroundTaskProcessor
    {
        private readonly TasksProvider _tasksProvider;

        public BackgroundTaskProcessor(TasksProvider tasksProvider)
            => _tasksProvider = tasksProvider;

        public async Task StartProcessing(CancellationToken cancellationToken)
        {
            await Task.Yield();

            while (!cancellationToken.IsCancellationRequested)
            {
                var tasksList = _tasksProvider.TasksQueue;
                var taskToRun = tasksList.SingleOrDefault(x => x.Status == TaskStatus.Created);
                if (taskToRun != null)
                    await taskToRun.Task;
                await Task.Delay(5000, cancellationToken);
            }
        }
    }
}