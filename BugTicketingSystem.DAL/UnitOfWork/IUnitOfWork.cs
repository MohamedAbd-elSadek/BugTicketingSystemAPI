using BugTicketingSystem.DAL.Repositories.AttachmentRepo;
using BugTicketingSystem.DAL.Repositories.BugRepo;
using BugTicketingSystem.DAL.Repositories.BugUserRepo;
using BugTicketingSystem.DAL.Repositories.ProjectRepo;
using BugTicketingSystem.DAL.Repositories.UserRepo;

namespace BugTicketingSystem.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IUserRepo UserRepo { get; }
        public IBugRepo BugRepo { get; }
        public IAttachmentRepo AttachmentRepo { get; }
        public IProjectRepo ProjectRepo { get; }
        public IBugUserRepo BugUserRepo { get; }
        Task<int> SaveChangesAsync();


    }
}