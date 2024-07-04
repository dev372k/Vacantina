using Domain.Document;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserRepo _userRepo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            await _userRepo.InsertAsync(new User
            (
                "Test",
                "Test",
                "Test",
                DL.Commons.enRole.User
            ));
            return Ok();
        }
    }
}
