using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TopCourseWorkBl.Enums;

namespace TopCourseWorkBl.BackgroundTasksService
{
    public class TasksProvider
    {
        public List<QueuedTask> TasksQueue { get; set; } = new();

        public QueuedTask RegisterNewTaskToExecute(Task task, OperationType type, long userId, string description,
            CancellationTokenSource cancellationTokenSource)
        {
            TasksQueue.Add(new QueuedTask(userId, type, task, description, cancellationTokenSource));
            return TasksQueue.Last();
        }

        public QueuedTask? CancelTask(int taskId)
        {
            var task = TasksQueue.Find(x => x.Task.Id == taskId);
            task?.CancellationTokenSource.Cancel();
            return task;
        }

        public List<QueuedTask> CancelTask(long userId)
        {
            var tasks = TasksQueue.Where(x => x.CreatedBy == userId);
            var queuedTasks = tasks.ToList();
            foreach (var task in queuedTasks)
                task.CancellationTokenSource.Cancel();
            return queuedTasks;
        }
    }
}