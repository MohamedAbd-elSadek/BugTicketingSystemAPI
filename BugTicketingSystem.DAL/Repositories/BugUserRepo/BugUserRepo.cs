
using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL.Repositories.BugUserRepo
{
    public class BugUserRepo : GenericRepo<BugUser>, IBugUserRepo
    {
        private readonly BugDbContext _context;
        public BugUserRepo(BugDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<BugUser?> GetByBugIdAndUserIdAsync(Guid bugId, Guid userId)
        {
            return _context.BugUsers
                .FirstOrDefaultAsync(bu => bu.BugId == bugId && bu.UserId == userId);

        }
    }
    
}
