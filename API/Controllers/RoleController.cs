using API.Models;
using Infrastructure.Abstractions.Interfaces;
using Infrastructure.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.Messages;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private IRoleRepo _roleRepo;

        public RoleController(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        [HttpGet("{id}"), Authorize]
        [HasPermission(PermissionConstants.ROLE)]
        public IActionResult Get(int id) => Ok(new ResponseModel { Data = _roleRepo.RoleDetails(id) });

        [HttpGet("all")]
        public IActionResult Get() => Ok(new ResponseModel { Data = _roleRepo.Get() });

        [HttpPost, Authorize]
        [HasPermission(PermissionConstants.ROLE)]
        public IActionResult Post([FromBody] AddRoleDTO dto) =>
             Ok(new ResponseModel { Data = _roleRepo.Insert(dto), Message = ResponseMessages.ROLE_ADDED });
        
        [HttpPut, Authorize]
        [HasPermission(PermissionConstants.ROLE)]
        public IActionResult Update(int id, UpdateRoleDTO dto)=>
            Ok(new ResponseModel { Data = _roleRepo.Update(id, dto), Message = ResponseMessages.ROLE_UPDATED });

        [HttpDelete, Authorize]
        [HasPermission(PermissionConstants.ROLE)]
        public IActionResult Delete(int id) =>
            Ok(new ResponseModel { Data = _roleRepo.Delete(id), Message = ResponseMessages.ROLE_DELETED });
    }
}
