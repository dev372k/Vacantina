using Infrastructure.Abstractions.Interfaces;
using Domain;
using Domain.Entities;
using Domain.Repositories;
using Omu.ValueInjecter;
using Infrastructure.DTOs;

namespace Infrastructure.Abstractions.Implementations
{
    public class RoleRepo : IRoleRepo
    {
        private ApplicationDBContext _context;
        private readonly IGenericRepository<Role> _roleRepo;
        private readonly IGenericRepository<RolePermission> _rolePermissionRepo;

        public RoleRepo(ApplicationDBContext context, IGenericRepository<Role> roleRepo, IGenericRepository<RolePermission> rolePermissionRepo)
        {
            _context = context;
            _roleRepo = roleRepo;
            _rolePermissionRepo = rolePermissionRepo;
        }

        public GetRoleDTO Insert(AddRoleDTO dto)
        {
            var role = new Role()
            {
                Name = dto.Name,
            };

            _roleRepo.Insert(role);

            var rolePermissions = dto.RolePermission.Select(_ => new RolePermission
            {
                RoleId = _.RoleId,
                PermissionId = _.PermissionId,
                IsAllowed = _.IsAllowed
            }).ToList();

            _rolePermissionRepo.BulkInsert(rolePermissions);


            return Mapper.Map<GetRoleDTO>(role);
        }

        public GetRoleDTO Update(int id, UpdateRoleDTO dto)
        {
            var existingRole = _roleRepo.Get(id);
            existingRole.Name = dto.Name;
            if (existingRole.RolePermissions == null)
            {
                existingRole.RolePermissions = new List<RolePermission>();
            }
            else
            {
                // Clear existing RolePermissions
                existingRole.RolePermissions.Clear();
            }

            var rolePermissions = dto.RolePermission.Select(_ => new RolePermission
            {
                RoleId = _.RoleId,
                PermissionId = _.PermissionId,
                IsAllowed = _.IsAllowed
            }).ToList();


            existingRole.RolePermissions.AddRange(rolePermissions);

            _rolePermissionRepo.BulkInsert(rolePermissions);
            _roleRepo.Update(existingRole);

            return Mapper.Map<GetRoleDTO>(existingRole);
        }

        public List<RolePermissions> RoleDetails(int id)
        {
            var role = (from r in _context.Roles
                        join rp in _context.RolePermission
                        on r.Id equals rp.RoleId
                        join p in _context.Permissions
                        on rp.PermissionId equals p.Id
                        select new RolePermissions
                        {
                            Role = r.Name,
                            Permission = p.Name,
                            IsAllowed = rp.IsAllowed
                        }).ToList();
            return role;
        }

        public List<GetRoleDTO> Get()
        {
            List<GetRoleDTO> roles = new List<GetRoleDTO>();
            var data = _roleRepo.GetAll().ToList();

            foreach (var category in data)
            {
                roles.Add(Mapper.Map<GetRoleDTO>(roles));
            }

            return roles;
        }
        public GetRoleDTO Delete(int id)
        {
            var deletedrole = _roleRepo.Get(id);
            deletedrole.IsDeleted = false;
            var categoryId = _roleRepo.Delete(deletedrole);
            return Mapper.Map<GetRoleDTO>(deletedrole);
        }
    }
}
