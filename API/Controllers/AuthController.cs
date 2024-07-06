using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.Commons;
using Shared.DTOs.UserDTOs;
using Shared.Extensions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserRepo _userRepo) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(AddUserDTO request) 
        => Ok(await _userRepo.InsertAsync(request).ToResponse(message: ResponseMessages.USER_ADDED));
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO request) 
        => Ok(await _userRepo.LoginAsync(request).ToResponse());
    
    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin(GoogleLoginDTO request) 
        => Ok(await _userRepo.GoogleLoginAsync(request).ToResponse());
    
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _userRepo.GetUsersAsync().ToResponse());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
        => Ok(await _userRepo.GetUserAsync(id).ToResponse());
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => Ok(await _userRepo.DeleteAsync(id).ToResponse(message: ResponseMessages.USER_DELETED));

}
