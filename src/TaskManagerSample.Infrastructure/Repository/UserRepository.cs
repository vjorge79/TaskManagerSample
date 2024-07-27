using TaskManagerSample.Core.Intefaces;
using TaskManagerSample.Infrastructure.Context;

namespace TaskManagerSample.Infrastructure.Repository;

public class UserRepository : Repository<Core.Models.User>, IUserRepository
{
    public UserRepository(TaskManagerDbContext context) : base(context)
    {
    }
}