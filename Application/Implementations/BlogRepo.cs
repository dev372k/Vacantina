using Application.Implementations.Base;
using Domain.Documents;
using Domain.Repositories;
using Domain.Repositories.Services;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using Shared.DTOs.BlogDTOs;
using Shared.Exceptions.Messages;
using Shared.Exceptions;
using Stripe;
using System.Net;
using Shared.Helpers;
using System.Drawing.Printing;
using Shared.Extensions;

namespace Application.Implementations;

public class BlogRepo : BaseRepo<Blog>, IBlogRepo
{
    private IFileService _fileService;

    public BlogRepo(
    IMongoClient mongoClient,
    IClientSessionHandle clientSessionHandle,
    IFileService fileService) : base(mongoClient, clientSessionHandle, "blog")
    {
        _fileService = fileService;
    }

    public async Task InsertAsync(AddBlogDTO dto)
    {
        var imageURL = await _fileService.SaveAsync(dto.Image);
        await InsertAsync(new Blog
        (
            dto.Title,
            dto.Content,
            imageURL,
            dto.IsFeatured,
            dto.Tags
        ));
    }

    public async Task<GetBlogDTO> GetUserAsync(string id)
    {
        var filter = Builders<Blog>.Filter.Eq(f => f.Id, id);
        var blog = await Collection.Find(filter).FirstOrDefaultAsync();

        if (blog == null)
            throw new CustomException(HttpStatusCode.OK, ExceptionMessages.BLOG_DOESNOT_EXIST);

        return new GetBlogDTO
        {
            Id = blog.Id,
            Title = blog.Title,
            ImageURL = SecurityHelper.GenerateFileUrl(blog.ImageURL, DateTime.UtcNow.AddMinutes(20)),
            Content = blog.Content,
            IsFeatured = blog.IsFeatured,
            Tags = blog.Tags
        };
    }

    public List<GetBlogDTO> GetUsersAsync(int pageNumber = 1, int pageSize = 10)
    {
        return Collection.AsQueryable().Paginate(pageNumber, pageSize).Select(blog => new GetBlogDTO
        {
            Id = blog.Id,
            Title = blog.Title,
            Content = blog.Content,
            ImageURL = SecurityHelper.GenerateFileUrl(blog.ImageURL, DateTime.UtcNow.AddMinutes(20)),
            IsFeatured = blog.IsFeatured,
            Tags = blog.Tags
        }).ToList();    
    }

    public async Task UpdateAsync(UpdateBlogDTO dto)
    {
        var imageURL = await _fileService.SaveAsync(dto.Image);
        var blog = new Blog
        (
            dto.Title,
            dto.Content,
            imageURL,
            dto.IsFeatured,
            dto.Tags
        );
        blog.SetId(dto.Id);
        await UpdateAsync(blog);
    }
}
