﻿using Domain.Repositories;
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
        => Ok(await _userRepo.InsertAsync(request).ToResponseAsync(message: ResponseMessages.USER_ADDED));
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO request) 
        => Ok(await _userRepo.LoginAsync(request).ToResponseAsync());
    
    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin(GoogleLoginDTO request) 
        => Ok(await _userRepo.GoogleLoginAsync(request).ToResponseAsync());
    
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _userRepo.GetUsersAsync().ToResponseAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
        => Ok(await _userRepo.GetUserAsync(id).ToResponseAsync());
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => Ok(await _userRepo.DeleteAsync(id).ToResponseAsync(message: ResponseMessages.USER_DELETED));

}
