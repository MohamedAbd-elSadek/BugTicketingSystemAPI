
namespace BugTicketingSystem.DAL.Models
{
    public class BugUser
    {
        public Guid BugId { get; set; }
        public Bug Bug { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
