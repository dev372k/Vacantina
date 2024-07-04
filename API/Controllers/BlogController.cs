using DL.Commons;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.BlogDTOs;
using Shared.DTOs.UserDTOs;
using Shared.Extensions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController(IBlogRepo _blogRepo) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(AddBlogDTO request)
            => Ok(await _blogRepo.InsertAsync(request).ToResponse(message: ResponseMessages.USER_ADDED));

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _blogRepo.GetUsersAsync().ToResponse());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
            => Ok(await _blogRepo.GetUserAsync(id).ToResponse());
    }
}
