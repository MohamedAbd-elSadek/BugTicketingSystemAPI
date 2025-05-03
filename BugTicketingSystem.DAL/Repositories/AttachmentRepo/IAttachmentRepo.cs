using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.GenericRepo;

namespace BugTicketingSystem.DAL.Repositories.AttachmentRepo
{
    public interface IAttachmentRepo: IGenericRepo<Attachment>
    {
        void AddAttachmentToBugAsync(Attachment attachment, Guid bugId);
        Task<List<Attachment>> GetAttachmentsByBugIdAsync(Guid bugId);
        Task<Attachment> GetByBugAndAttachmentIdAsync(Guid bugId, Guid attachmentId);

        void RemoveAttachmentByBugIdAsync(Guid bugId);

    }
}