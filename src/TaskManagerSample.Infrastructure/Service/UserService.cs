using TaskManagerSample.Core.Intefaces;
using TaskManagerSample.Core.Models.Validations;
using TaskManagerSample.Core.Notifications;

namespace TaskManagerSample.Infrastructure.Service;

public class UserService : NotificationHandler, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository,
                       INotifier notifier) : base(notifier)
    {
        _userRepository = userRepository;
    }

    public async Task Add(Core.Models.User user)
    {
        if (!ExecuteValidation(new UserValidation(), user)) return;

        await _userRepository.Add(user);
    }

    public async Task Update(Core.Models.User user)
    {
        if (!ExecuteValidation(new UserValidation(), user)) return;

        await _userRepository.Update(user);
    }

    public async Task Delete(Guid id)
    {
        await _userRepository.Delete(id);
    }

    public void Dispose()
    {
        _userRepository?.Dispose();
    }
}