using System;
using System.Threading;
using System.Threading.Tasks;
using TaskStatus = TopCourseWorkBl.Enums.TaskStatus;

namespace TopCourseWorkBl.BackgroundTasksService
{
    public class TaskStatusProcessor
    {
        private readonly TasksProvider _tasksProvider;

        public TaskStatusProcessor(TasksProvider tasksProvider)
            => _tasksProvider = tasksProvider;

        public async Task StartProcessing(CancellationToken cancellationToken)
        {
            await Task.Yield();

            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var runningTask in _tasksProvider.TasksQueue)
                {
                    if (runningTask.Task.IsCanceled || runningTask.Status == TaskStatus.Cancelled)
                    {
                        runningTask.Status = TaskStatus.Cancelled;
                        runningTask.CanceledAt = DateTime.Now;
                        continue;
                    }
                    if (runningTask.Task.IsCompletedSuccessfully && runningTask.Status != TaskStatus.Done)
                    {
                        runningTask.Status = TaskStatus.Done;
                        runningTask.CompletedAt = DateTime.Now;
                        continue;
                    }
                    if (runningTask.Task.IsFaulted)
                    {
                        runningTask.Status = TaskStatus.Interrupted;
                        runningTask.CanceledAt = DateTime.Now;
                        continue;
                    }
                    if (!runningTask.Task.IsCompleted && runningTask.Task.Status == System.Threading.Tasks.TaskStatus.Running)
                    {
                        runningTask.Status = TaskStatus.Running;
                    }
                }
                
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}