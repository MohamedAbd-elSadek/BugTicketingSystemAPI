namespace BugTicketingSystem.DAL.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new HashSet<User>();
        public ICollection<Bug> Bugs { get; set; } = new HashSet<Bug>();
        
    }
}
