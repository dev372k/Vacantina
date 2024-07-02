using API.Models;
using Infrastructure.Abstractions.Interfaces;
using Infrastructure.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        public UserController(
            IUserRepo userRepo
            )
        {
            _userRepo = userRepo;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
            Ok(new ResponseModel { Data = _userRepo.GetUserById(id)! });

        [HttpGet("all")]
        public IActionResult Get()
            => Ok(new ResponseModel { Data = _userRepo.GetAllUsers() });

        [HttpGet]
        public IActionResult Get(int offset = 1, int limit = 10, string query = "") =>
            Ok(new ResponseModel { Data = _userRepo.Get(offset, limit, query) });

        [HttpPut]
        public IActionResult Update(int id, UpdateUserDTO dto) =>
            Ok(new ResponseModel { Data = _userRepo.UpdateUser(id, dto) });

        [HttpDelete]
        public IActionResult Delete(int id) =>
            Ok(new ResponseModel { Data = _userRepo.Delete(id) });
    }
}
