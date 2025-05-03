using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.GenericRepo;

namespace BugTicketingSystem.DAL.Repositories.BugUserRepo
{
    public interface IBugUserRepo : IGenericRepo<BugUser>
    {
        Task<BugUser?> GetByBugIdAndUserIdAsync(Guid bugId, Guid userId);
        //Task<List<BugUser>> GetByBugIdAsync(Guid bugId);
        //Task<List<BugUser>> GetByUserIdAsync(Guid userId);

    }
   
}