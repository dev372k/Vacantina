using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Commons;
using Shared.DTOs.BlogDTOs;
using Shared.Extensions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController(IBlogRepo _blogRepo) : ControllerBase
    {
        [HttpPost, Authorize]
        [IsAuthorized(["Admin"])]
        public async Task<IActionResult> Post(AddBlogDTO request)
            => Ok(await _blogRepo.InsertAsync(request).ToResponseAsync(message: ResponseMessages.BLOG_ADDED));

        [HttpPut, Authorize]
        [IsAuthorized(["Admin"])]
        public async Task<IActionResult> Put(UpdateBlogDTO request)
            => Ok(await _blogRepo.UpdateAsync(request).ToResponseAsync(message: ResponseMessages.BLOG_UPDATED));

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
            => Ok(await _blogRepo.GetUsersAsync(pageNumber, pageSize).ToResponseAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
            => Ok(await _blogRepo.GetUserAsync(id).ToResponseAsync());

        [HttpDelete("{id}"), Authorize]
        [IsAuthorized(["Admin"])]
        public async Task<IActionResult> Delete(string id)
            => Ok(await _blogRepo.DeleteAsync(id).ToResponseAsync(message: ResponseMessages.BLOG_DELETED));
    }
}
