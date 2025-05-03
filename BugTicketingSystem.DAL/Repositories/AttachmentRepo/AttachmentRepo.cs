using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL.Repositories.AttachmentRepo
{
    public class AttachmentRepo : GenericRepo<Attachment>, IAttachmentRepo
    {
        private readonly BugDbContext _context;

        public AttachmentRepo(BugDbContext context) : base(context)
        {
            _context = context;
        }

        public void AddAttachmentToBugAsync(Attachment attachment, Guid bugId)
        {
            var bug = _context.Bugs.Find(bugId);
            if (bug == null)
            {
                throw new Exception("Bug not found");
            }
            attachment.BugId = bugId;
            _context.Attachments.Add(attachment);
            

        }

        public Task<List<Attachment>> GetAttachmentsByBugIdAsync(Guid bugId)
        {
            return _context.Attachments
                .Where(a => a.BugId == bugId)
                .Include(a => a.Bug)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Attachment> GetByBugAndAttachmentIdAsync(Guid bugId, Guid attachmentId)
        {
            var attachment = await _context.Attachments
                .FirstOrDefaultAsync(a => a.BugId == bugId && a.Id == attachmentId);
            if (attachment == null)
            {
                throw new Exception("Attachment not found");
            }
            return attachment;
        }

        public void RemoveAttachmentByBugIdAsync(Guid bugId)
        {
            var attachments = _context.Attachments
                .Where(a => a.BugId == bugId)
                .ToList();
            if (attachments != null && attachments.Count > 0)
            {
                _context.Attachments.RemoveRange(attachments);
            }
            else
            {
                throw new Exception("No attachments found for this bug");
            }

        }
       
    }
    
}
