using Domain.Document;
using Domain.Repositories.Base;

namespace Domain.Repositories;

public interface IUserRepo : IBaseRepo<User>
{
    Task<IEnumerable<User>> GetUsersAsync();

    Task<User> GetUserAsync(string id);
}
