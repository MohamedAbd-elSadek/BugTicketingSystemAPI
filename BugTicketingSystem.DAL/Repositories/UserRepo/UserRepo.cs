using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.GenericRepo;

namespace BugTicketingSystem.DAL.Repositories.UserRepo
{
    public class UserRepo: GenericRepo<User>, IUserRepo
    {
        public UserRepo(BugDbContext context) : base(context)
        {
        }
    }
    
}
