using TaskManagerSample.Core.Intefaces;
using TaskManagerSample.Core.Models.Validations;
using TaskManagerSample.Core.Notifications;

namespace TaskManagerSample.Infrastructure.Service;

public class TaskService : NotificationHandler, ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository,
                       INotifier notifier) : base(notifier)
    {
        _taskRepository = taskRepository;
    }

    public async Task Add(Core.Models.Task task)
    {
        if (!ExecuteValidation(new TaskValidation(), task)) return;

        await _taskRepository.Add(task);
    }

    public async Task Update(Core.Models.Task task)
    {
        if (!ExecuteValidation(new TaskValidation(), task)) return;

        await _taskRepository.Update(task);
    }

    public async Task Delete(Guid id)
    {
        await _taskRepository.Delete(id);
    }

    public void Dispose()
    {
        _taskRepository?.Dispose();
    }
}