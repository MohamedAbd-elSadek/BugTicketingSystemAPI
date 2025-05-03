using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.GenericRepo;

namespace BugTicketingSystem.DAL.Repositories.BugRepo
{
    public class BugRepo: GenericRepo<Bug>, IBugRepo
    {
        public BugRepo(BugDbContext context) : base(context)
        {
        }
    }
    
}
