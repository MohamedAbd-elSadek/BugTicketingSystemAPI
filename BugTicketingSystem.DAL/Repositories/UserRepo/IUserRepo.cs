using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.GenericRepo;

namespace BugTicketingSystem.DAL.Repositories.UserRepo
{
    public interface IUserRepo: IGenericRepo<User>
    {
    }
}