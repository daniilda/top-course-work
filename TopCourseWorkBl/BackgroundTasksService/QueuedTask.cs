using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using TopCourseWorkBl.Enums;
using TaskStatus = TopCourseWorkBl.Enums.TaskStatus;

namespace TopCourseWorkBl.BackgroundTasksService
{
    public record QueuedTask(long CreatedBy, OperationType Type, Task Task, string? Description,
        CancellationTokenSource CancellationTokenSource)
    {
        public long Id => Task.Id;
        [JsonIgnore]
        public Task Task { get; set; } = Task;
        public TaskStatus Status { get; set; } = TaskStatus.Created;
        public OperationType Type { get; set; } = Type;
        public long CreatedBy { get; set; } = CreatedBy;
        public string? Description { get; set; } = Description;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? CompletedAt { get; set; }
        public DateTime? CanceledAt { get; set; }
        public string? ErrorMessage => Task.Exception?.Message;
        public bool IsSaved { get; set; } = false;
        [JsonIgnore]
        public CancellationTokenSource CancellationTokenSource { get; set; } = CancellationTokenSource;
    }
}