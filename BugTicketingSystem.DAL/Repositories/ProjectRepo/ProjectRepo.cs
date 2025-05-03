using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Repositories.GenericRepo;

namespace BugTicketingSystem.DAL.Repositories.ProjectRepo
{
    public class ProjectRepo : GenericRepo<Project>, IProjectRepo
    {
        public ProjectRepo(BugDbContext context) : base(context)
        {
        }
    }
    
}
