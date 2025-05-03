using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Repositories.AttachmentRepo;
using BugTicketingSystem.DAL.Repositories.BugRepo;
using BugTicketingSystem.DAL.Repositories.BugUserRepo;
using BugTicketingSystem.DAL.Repositories.ProjectRepo;
using BugTicketingSystem.DAL.Repositories.UserRepo;
using BugTicketingSystem.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketingSystem.DAL
{
    public static class DataAccessExtention
    {
        public static void AddDataAccessServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<BugDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("default")));

            // Register repositories and other data access services here
            // Example: services.AddScoped<IBugRepository, BugRepository>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IBugRepo, BugRepo>();
            services.AddScoped<IAttachmentRepo, AttachmentRepo>();
            services.AddScoped<IProjectRepo, ProjectRepo>();
            services.AddScoped<IBugUserRepo, BugUserRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }


    }
}
