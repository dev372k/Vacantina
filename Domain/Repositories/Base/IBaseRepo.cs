using Domain.Document.Base;

namespace Domain.Repositories.Base;

public interface IBaseRepo<T> where T : BaseEntity
{
    Task InsertAsync(T obj);

    Task UpdateAsync(T obj);

    Task DeleteAsync(string id);
}
