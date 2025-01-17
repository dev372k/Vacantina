﻿using DL.Commons;
using Domain.Document;
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
        => Ok(await _userRepo.GetUsersAsync().ToResponse(message: ResponseMessages.USER_ADDED));
}
