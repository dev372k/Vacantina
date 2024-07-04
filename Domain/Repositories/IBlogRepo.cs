using Domain.Document;
using Domain.Documents;
using Shared.DTOs.BlogDTOs;

namespace Domain.Repositories;

public interface IBlogRepo
{
    Task InsertAsync(AddBlogDTO dto);

    Task<IEnumerable<Blog>> GetUsersAsync();

    Task<Blog> GetUserAsync(string id);
}
