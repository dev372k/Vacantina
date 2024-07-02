using Infrastructure.DTOs;

namespace Infrastructure.Abstractions.Interfaces
{
    public interface IRoleRepo
    {
        List<RolePermissions> RoleDetails(int id);
        List<GetRoleDTO> Get();
        GetRoleDTO Insert(AddRoleDTO dto);
        GetRoleDTO Update(int id, UpdateRoleDTO dto);
        GetRoleDTO Delete(int id);
    }
}
