using DL.Commons;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.UserDTOs;
using Shared.Extensions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserRepo _userRepo) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(AddUserDTO request) 
        => Ok(await _userRepo.InsertAsync(request).ToResponse(message: ResponseMessages.USER_ADDED));
    
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
