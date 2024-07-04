using Domain.Document.Base;
using Domain.Repositories.Base;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Application.Implementations.Base;

public class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity
{
    private const string _database = "Vacantina";
    private readonly IMongoClient _mongoClient;
    private readonly IClientSessionHandle _clientSessionHandle;
    private readonly string _collection;

    public BaseRepo(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, string collection)
    {
        (_mongoClient, _clientSessionHandle, _collection) = (mongoClient, clientSessionHandle, collection);

        if (!_mongoClient.GetDatabase(_database).ListCollectionNames().ToList().Contains(collection))
            _mongoClient.GetDatabase(_database).CreateCollection(collection);
    }

    protected virtual IMongoCollection<T> Collection =>
        _mongoClient.GetDatabase(_database).GetCollection<T>(_collection);

    public async Task InsertAsync(T obj) =>
        await Collection.InsertOneAsync(_clientSessionHandle, obj);

    public async Task UpdateAsync(T obj)
    {
        Expression<Func<T, string>> func = f => f.Id;
        var value = (string)obj.GetType().GetProperty(func.Body.ToString().Split(".")[1]).GetValue(obj, null);
        var filter = Builders<T>.Filter.Eq(func, value);

        if (obj != null)
            await Collection.ReplaceOneAsync(_clientSessionHandle, filter, obj);
    }

    public async Task DeleteAsync(string id) =>
        await Collection.DeleteOneAsync(_clientSessionHandle, f => f.Id == id);
}
