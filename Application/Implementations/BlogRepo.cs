using Application.Implementations.Base;
using DL.Commons;
using Domain.Document;
using Domain.Documents;
using Domain.Repositories;
using MongoDB.Driver;
using Shared.DTOs.BlogDTOs;
using Shared.DTOs.UserDTOs;
using Shared.Exceptions.Messages;
using Shared.Exceptions;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations;

public class BlogRepo : BaseRepo<Blog>, IBlogRepo
{
    public BlogRepo(
    IMongoClient mongoClient,
    IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "blog")
    {}

    public async Task InsertAsync(AddBlogDTO dto)
    {
        await InsertAsync(new Blog
        (
            dto.Title,
            dto.Content,
            dto.ImageURL,
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
}
