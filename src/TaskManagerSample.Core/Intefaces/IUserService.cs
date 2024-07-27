namespace TaskManagerSample.Core.Intefaces;

public interface IUserService : IDisposable
{
    Task Add(Core.Models.User user);

    Task Update(Core.Models.User user);

    Task Delete(Guid id);
}