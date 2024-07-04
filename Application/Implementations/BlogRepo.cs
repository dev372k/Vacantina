using Application.Implementations.Base;
using Domain.Documents;
using Domain.Repositories;
using MongoDB.Driver;
using Shared.DTOs.BlogDTOs;

namespace Application.Implementations;

public class BlogRepo : BaseRepo<Blog>, IBlogRepo
{
    public BlogRepo(
    IMongoClient mongoClient,
    IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "blog")
    { }

    public async Task InsertAsync(AddBlogDTO dto)
    {
        await InsertAsync(new Blog
        (
            dto.Title,
            dto.Content,
            dto.ImageURL,
            dto.IsFeatured,
            dto.Tags
        ));
    }

    public async Task<Blog> GetUserAsync(string id)
    {
        var filter = Builders<Blog>.Filter.Eq(f => f.Id, id);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Blog>> GetUsersAsync() =>
        await Collection.AsQueryable().ToListAsync();

    public async Task UpdateAsync(UpdateBlogDTO dto)
    {
        var blog = new Blog
        (
            dto.Title,
            dto.Content,
            dto.ImageURL,
            dto.IsFeatured,
            dto.Tags
        );
        blog.SetId(dto.Id);
        await UpdateAsync(blog);
    }
}
