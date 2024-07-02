using Infrastructure.Abstractions.Interfaces;
using Infrastructure.DTOs;
using Infrastructure.Services;
using Domain.Entities;
using Shared.Helpers;
using Domain.Repositories;
using Omu.ValueInjecter;
using Shared.Messages;

namespace Infrastructure.Abstractions.Implementations
{
    public class UserRepo : IUserRepo
    {
        private IGenericRepository<User> _userRepo;
        private readonly IEmailService _emailService;

        public UserRepo(IGenericRepository<User> userrepo, IEmailService emailService)
        {
            _userRepo = userrepo;
            _emailService = emailService;
        }

        public void AddUser(AddUserDTO user)
        {
            if (UserExists(user.Email))
                throw new Exception(ExceptionMessages.USER_ALREADY_EXIST);

            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                RoleId = user.Role,
                PasswordHash = SecurityHelper.GenerateHash(user.Password)
            };

            _userRepo.Insert(newUser);
        }

        public PagedResult<User> Get(int offset, int limit, string query)
        {
            var data = _userRepo.GetAll()   
                .Where(_ => (!string.IsNullOrEmpty(query) ? _.Name.ToLower().Contains(query.ToLower()) : true))
                .ToPageResult(offset, limit);
            return data;
        }

        public IQueryable<GetUserDTO> GetAllUsers()
        {
            return _userRepo.GetAll().Select(_ => new GetUserDTO
            {
                Id = _.Id,
                Name = _.Name,
                Email = _.Email,
                RoleId = _.RoleId,
                Password = _.PasswordHash
            }).AsQueryable();
        }

        public GetUserDTO Get(string email, string password)
        {
            var user = GetUserByEmail(email);
            if (user == null)
                throw new Exception(ExceptionMessages.USER_DOESNOT_EXIST);

            if (SecurityHelper.ValidateHash(password, user.Password))
                return Mapper.Map<GetUserDTO>(user);
            throw new Exception(ExceptionMessages.INVALID_CREDENTIALS);

        }

        public GetUserDTO? GetUserByEmail(string email) => GetAllUsers().FirstOrDefault(_ => _.Email == email);
        public GetUserDTO? GetUserById(int id) => GetAllUsers().FirstOrDefault(_ => _.Id == id);
        private bool UserExists(string email) => GetAllUsers().Any(_ => _.Email == email);

        public void UpdateUserPassword(string email, string Password)
        {
            var user = _userRepo.GetAll().FirstOrDefault(u => u.Email == email);
            if (user == null)
                throw new Exception(ExceptionMessages.USER_DOESNOT_EXIST);

            user.PasswordHash = SecurityHelper.GenerateHash(Password);
            _userRepo.Update(user);
        }

        public void ForgetPassword(string email)
        {
            var newPassword = SecurityHelper.GenerateRandomPassword();
            UpdateUserPassword(email, newPassword);
            string emailBody = string.Format(EmailBody.FORGET_PASSWORD, newPassword);
            _emailService.SendEmailAsync(email, EmailSubject.FORGET_PASSWORD, emailBody).GetAwaiter().GetResult();
        }

        public void ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = GetUserByEmail(email);
            if (!SecurityHelper.ValidateHash(oldPassword, user.Password))
                throw new Exception(ExceptionMessages.INVALID_CREDENTIALS);

            UpdateUserPassword(email, newPassword);
        }

        public GetUserDTO UpdateUser(int id, UpdateUserDTO dto)
        {
            var updateuser = _userRepo.Get(id);
            updateuser.Name = dto.Name;
            updateuser.RoleId = dto.RoleId;
            _userRepo.Update(updateuser);

            return Mapper.Map<GetUserDTO>(updateuser);
        }

        public GetUserDTO Delete(int id)
        {
            var userrole = _userRepo.Get(id);
            userrole.IsDeleted = false;
            _userRepo.Delete(userrole);
            return Mapper.Map<GetUserDTO>(userrole);
        }

    }
}

