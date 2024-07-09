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
            => Ok(await _blogRepo.InsertAsync(request).ToResponse(message: ResponseMessages.BLOG_ADDED));

        [HttpPut, Authorize]
        [IsAuthorized(["Admin"])]
        public async Task<IActionResult> Put(UpdateBlogDTO request)
            => Ok(await _blogRepo.UpdateAsync(request).ToResponse(message: ResponseMessages.BLOG_UPDATED));

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _blogRepo.GetUsersAsync().ToResponse());

        [HttpGet("{id}"), Authorize]
        [IsAuthorized(["Admin"])]
        public async Task<IActionResult> Get(string id)
            => Ok(await _blogRepo.GetUserAsync(id).ToResponse());
        
        [HttpDelete("{id}"), Authorize]
        [IsAuthorized(["Admin"])]
        public async Task<IActionResult> Delete(string id)
            => Ok(await _blogRepo.DeleteAsync(id).ToResponse(message: ResponseMessages.BLOG_DELETED));
    }
}
