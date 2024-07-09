using Domain.Document;
using Domain.Documents;
using Domain.Repositories.Base;
using Shared.DTOs.BlogDTOs;

namespace Domain.Repositories;

public interface IBlogRepo : IBaseRepo<Blog>
{
    Task InsertAsync(AddBlogDTO dto);
    Task UpdateAsync(UpdateBlogDTO dto);
    List<GetBlogDTO> GetUsersAsync(int pageNumber = 1, int pageSize = 10);
    Task<GetBlogDTO> GetUserAsync(string id);
}
