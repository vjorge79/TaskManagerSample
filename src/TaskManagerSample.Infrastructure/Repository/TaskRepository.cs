using TaskManagerSample.Core.Intefaces;
using TaskManagerSample.Infrastructure.Context;

namespace TaskManagerSample.Infrastructure.Repository;

public class TaskRepository : Repository<Core.Models.Task>, ITaskRepository
{
    public TaskRepository(TaskManagerDbContext context) : base(context)
    {
    }
}