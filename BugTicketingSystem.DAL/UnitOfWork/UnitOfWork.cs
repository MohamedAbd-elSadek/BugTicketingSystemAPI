using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Repositories.AttachmentRepo;
using BugTicketingSystem.DAL.Repositories.BugRepo;
using BugTicketingSystem.DAL.Repositories.BugUserRepo;
using BugTicketingSystem.DAL.Repositories.ProjectRepo;
using BugTicketingSystem.DAL.Repositories.UserRepo;

namespace BugTicketingSystem.DAL.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BugDbContext _context;
        public UnitOfWork(
         BugDbContext context,
         IUserRepo userRepo,
         IBugRepo bugRepo, 
         IAttachmentRepo attachmentRepo, 
         IProjectRepo projectRepo,
         IBugUserRepo bugUserRepo)
        {
            _context = context;
            UserRepo = userRepo;
            BugRepo = bugRepo;
            AttachmentRepo = attachmentRepo;
            ProjectRepo = projectRepo;
            BugUserRepo = bugUserRepo;

        }

        public IUserRepo UserRepo { get; }
        public IBugRepo BugRepo { get; }
        public IAttachmentRepo AttachmentRepo { get; }
        public IProjectRepo ProjectRepo { get; }
        public IBugUserRepo BugUserRepo { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
    
}
