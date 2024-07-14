namespace Shared.DTOs.UserDTOs;

public class UpdateUserDTO
{
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Phone { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public DateTime DoB { get; set; }
}
