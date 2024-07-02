
using Infrastructure.DTOs;

namespace API.Models
{
    public class UserData
    {
        public GetUserDTO User { get; set; }
        public List<RolePermissions> Role { get; set; }
    }
}
