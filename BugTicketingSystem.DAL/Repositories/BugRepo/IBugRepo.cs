using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.GenericRepo;

namespace BugTicketingSystem.DAL.Repositories.BugRepo
{
    public interface IBugRepo : IGenericRepo<Bug>
    {
    }
}