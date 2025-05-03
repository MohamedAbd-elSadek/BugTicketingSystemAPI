using Microsoft.AspNetCore.Identity;

namespace BugTicketingSystem.DAL.Models
{
    public class User : IdentityUser<Guid>
    {
        //public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //public string Email { get; set; } = string.Empty;
        //public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Developer;
        public ICollection<BugUser> BugUsers { get; set; } = new HashSet<BugUser>();
        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
    public enum UserRole
    {
        Manager,
        Developer,
        Tester
    }
}

