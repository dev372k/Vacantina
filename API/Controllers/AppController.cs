using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Commons;
using Shared.DTOs.AppDTOs;
using Shared.Extensions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppController(IAppRepo _appRepo) : ControllerBase
{
    [HttpPost("contact")]
    public async Task<IActionResult> Contact(ContactDTO request) 
        => Ok(await _appRepo.Contact(request).ToResponse(message: ResponseMessages.CONTACT_ADDED));

    [HttpGet("Admim"), Authorize]
    [IsAuthorized(["Admin"])]
    public IActionResult PingAdmin() 
        => Ok("Hello I'm admin");

    [HttpGet("User"), Authorize]
    [IsAuthorized(["User"])]
    public IActionResult PingUser() 
        => Ok("Hello I'm user");
}
