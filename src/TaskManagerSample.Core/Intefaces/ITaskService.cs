namespace TaskManagerSample.Core.Intefaces;

public interface ITaskService : IDisposable
{
    Task Add(Core.Models.Task task);

    Task Update(Core.Models.Task task);

    Task Delete(Guid id);
}