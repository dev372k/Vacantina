using Domain.Document;
using Domain.Repositories.Base;
using Shared.DTOs.UserDTOs;

namespace Domain.Repositories;

public interface IUserRepo : IBaseRepo<User>
{
    Task InsertAsync(AddUserDTO dto);
    Task UpdateAsync(UpdateUserDTO dto);
    Task DeleteAsync();
    Task<string> LoginAsync(LoginDTO dto);
    Task<string> GoogleLoginAsync(GoogleLoginDTO dto);
    Task<IEnumerable<User>> GetUsersAsync();

    Task<User> GetUserAsync(string id);
}
