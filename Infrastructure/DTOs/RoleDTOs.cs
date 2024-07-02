using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.DTOs
{
    public class RolePermissions
    {
        public string Role { get; set; }
        public string Permission { get; set; }
        public bool IsAllowed { get; set; }
    }

    public class GetRoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public List<RolePermissionsDTO> RolePermission { get; set; }
    }
    public class RolePermissionsDTO
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public bool IsAllowed { get; set; }
    }


    public class AddRoleDTO
    {
        public string Name { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<RolePermissionsDTO> RolePermission { get; set; }
    }
    public class UpdateRoleDTO
    {
        public string Name { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<RolePermissionsDTO> RolePermission { get; set; }
    }
}
