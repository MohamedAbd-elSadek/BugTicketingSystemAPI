namespace BugTicketingSystem.DAL.Models
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public Guid BugId { get; set; }
        public Bug Bug { get; set; } = null!;
    }
}