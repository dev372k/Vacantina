using Infrastructure.DTOs;
using Domain.Entities;
using Shared.Helpers;

namespace Infrastructure.Abstractions.Interfaces
{
    public interface IUserRepo
    {
        IQueryable<GetUserDTO> GetAllUsers();
        GetUserDTO? GetUserById(int id);
        GetUserDTO Get(string email, string password);
        GetUserDTO? GetUserByEmail(string email);
        void AddUser(AddUserDTO user);
        void UpdateUserPassword(string email, string newPassword);
        void ForgetPassword(string email);
        void ChangePassword(string email, string oldPassword, string newPassword);
        GetUserDTO UpdateUser(int id, UpdateUserDTO dto);
        GetUserDTO Delete(int id);
        PagedResult<User> Get(int offset, int limit, string query);
    }
}
