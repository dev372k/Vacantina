using Application.Implementations.Base;
using Shared.Commons;
using Domain.Document;
using Domain.Repositories;
using MongoDB.Driver;
using Shared.DTOs.UserDTOs;
using Shared.Exceptions;
using Shared.Exceptions.Messages;
using Shared.Helpers;
using System.Net;
using Google.Apis.Auth;
using MongoDB.Bson;

namespace Application.Implementations;

public class UserRepo : BaseRepo<User>, IUserRepo
{
    public UserRepo(
        IMongoClient mongoClient,
        IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "user") { }

    public async Task InsertAsync(AddUserDTO dto)
    {
        if (await IsUserExistbyEmailAsync(dto.Email))
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_ALREADY_EXIST);

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

    public async Task<string> GoogleLoginAsync(GoogleLoginDTO dto)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken);

        var user = await GetUserbyEmailAsync(payload.Email);
        if (user != null)
            return JWTHelper.CreateToken(new GetUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            });
        else
        {
            ObjectId objectId = ObjectId.GenerateNewId();
            var newUser = new User
            (
                payload.Name,
                payload.Email,
                "",
                enRole.User
            );

            newUser.SetId(objectId.ToString());
            await InsertAsync(newUser);
            return JWTHelper.CreateToken(new GetUserDTO
            {
                Id = objectId.ToString(),
                Name = payload.Name,
                Email = payload.Email,
                Role = enRole.User.ToString()
            });
        }

    }
    public async Task<string> LoginAsync(LoginDTO dto)
    {
        var user = await GetUserbyEmailAsync(dto.Email);
        if (user == null)
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.USER_DOESNOT_EXIST);

        if (!SecurityHelper.ValidateHash(dto.Password, user.PasswordHash))
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.INVALID_CREDENTIALS);

        return JWTHelper.CreateToken(new GetUserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        });
    }

    #region Private
    private async Task<bool> IsUserExistbyEmailAsync(string email)
    {
        var filter = Builders<User>.Filter.Eq(f => f.Email, email);
        var user = await Collection.Find(filter).FirstOrDefaultAsync();
        return user != null;
    }

    private async Task<User> GetUserbyEmailAsync(string email)
    {
        var filter = Builders<User>.Filter.Eq(f => f.Email, email);
        var user = await Collection.Find(filter).FirstOrDefaultAsync();
        return user;
    }

    #endregion
}
