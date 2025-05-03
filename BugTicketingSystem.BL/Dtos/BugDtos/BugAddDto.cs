using BugTicketingSystem.DAL.Models;

namespace BugTicketingSystem.BL.Dtos.BugDtos
{
    public class BugAddDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
    }
}
