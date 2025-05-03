using BugTicketingSystem.DAL.Models;

namespace BugTicketingSystem.BL.Dtos.AuthDtos
{
    public class RegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Developer; 
    }
}