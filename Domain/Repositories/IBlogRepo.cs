using Domain.Document;
using Domain.Documents;
using Domain.Repositories.Base;
using Shared.DTOs.BlogDTOs;

namespace Domain.Repositories;

public interface IBlogRepo : IBaseRepo<Blog>
{
    Task InsertAsync(AddBlogDTO dto);
    Task UpdateAsync(UpdateBlogDTO dto);
    Task<IEnumerable<Blog>> GetUsersAsync();
    Task<Blog> GetUserAsync(string id);
}
