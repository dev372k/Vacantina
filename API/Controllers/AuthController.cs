using API.Models;
using Infrastructure.Abstractions.Interfaces;
using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shared.Messages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRepo _userRepo;
        private readonly IRoleRepo _roleRepo;
        private readonly IEmailService _emailService;
        private readonly IStateHelper _stateHelper;
       
        public AuthController(IConfiguration config,
            IUserRepo userRepo,
            IRoleRepo roleRepo,
            IEmailService emailService,
            IStateHelper stateHelper
            )
        {
            _config = config;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _stateHelper = stateHelper;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] AddUserDTO request)
        {
            _userRepo.AddUser(request);

            return Ok(new ResponseModel { Message = ResponseMessages.USER_ADDED });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO request)
        {
            var user = _userRepo.Get(request.Email, request.Password);
            return Ok(new ResponseModel { Data = new { User = user, Token = CreateToken(user) } });
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] EmailRequestDTO emailReq)
        {
            _userRepo.ForgetPassword(emailReq.Email);
            return Ok(new ResponseModel {Message = ResponseMessages.FORGET_PASSWORD_EMAIL });
        }

        [HttpPost("change-password")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO model)
        {
            _userRepo.ChangePassword(_stateHelper.User().Email, model.OldPassword, model.NewPassword);
            return Ok(new ResponseModel { Message = ResponseMessages.PASSWORD_CHANGED });
        }

        private string CreateToken(GetUserDTO user)
        {
            var userData = new UserData
            {
                User = user,
                Role = _roleRepo.RoleDetails(user.RoleId)
            };

            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(userData)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("SecretKeys:JWT").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
