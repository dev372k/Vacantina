using Domain.Document;
using Domain.Repositories;
using Infrastructure.Implementations.Base;
using MongoDB.Driver;

namespace Infrastructure.Implementations;

public class UserRepo : BaseRepo<User>, IUserRepo
{
    public UserRepo(
        IMongoClient mongoClient,
        IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "user")
    {
    }

    public async Task<User> GetUserAsync(string id)
    {
        var filter = Builders<User>.Filter.Eq(f => f.Id, id);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetUsersAsync() =>
        await Collection.AsQueryable().ToListAsync();
}
