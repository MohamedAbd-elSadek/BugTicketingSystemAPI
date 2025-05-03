namespace BugTicketingSystem.BL.Dtos.AttachmentDtos
{
    public class AttachmentReadDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;

    }
}
