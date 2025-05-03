
namespace BugTicketingSystem.DAL.Models
{
    public class Bug
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public BugStatus Status { get; set; } = BugStatus.Open;
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public ICollection<BugUser> BugUsers { get; set; } = new HashSet<BugUser>();
        public ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    }
    public enum BugStatus
    {
        Open,
        InProgress,
        Closed
    }
}
