using Application.Implementations.Base;
using DL.Commons;
using Domain.Document;
using Domain.Repositories;
using MongoDB.Driver;
using Shared.DTOs.UserDTOs;
using Shared.Exceptions;
using Shared.Exceptions.Messages;
using Shared.Helpers;
using System.Net;

namespace Application.Implementations;

public class UserRepo : BaseRepo<User>, IUserRepo
{
    public UserRepo(
        IMongoClient mongoClient,
        IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "user")
    {

    }
    public async Task InsertAsync(AddUserDTO dto)
    {
        if (await GetUserbyEmailAsync(dto.Email))
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_ALREADY_EXIST);
        //throw new Exception(ExceptionMessages.USER_ALREADY_EXIST);

        await InsertAsync(new User
        (
            dto.Name,
            dto.Email,
            SecurityHelper.GenerateHash(dto.PasswordHash),
            enRole.User
        ));
    }

    public async Task<User> GetUserAsync(string id)
    {
        var filter = Builders<User>.Filter.Eq(f => f.Id, id);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetUsersAsync() =>
        await Collection.AsQueryable().ToListAsync();


    #region Private
    private async Task<bool> GetUserbyEmailAsync(string email)
    {
        var filter = Builders<User>.Filter.Eq(f => f.Email, email);
        var user = await Collection.Find(filter).FirstOrDefaultAsync();
        return user != null;
    }

    #endregion
}
