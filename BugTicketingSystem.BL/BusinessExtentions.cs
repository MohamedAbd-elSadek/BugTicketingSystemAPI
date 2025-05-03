using BugTicketingSystem.BL.Managers.AttachmentManager;
using BugTicketingSystem.BL.Managers.BugManager;
using BugTicketingSystem.BL.Managers.BugUserManager;
using BugTicketingSystem.BL.Managers.ProjectMananger;
using BugTicketingSystem.BL.Managers.UserManager;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketingSystem.BL
{
    public static class BusinessExtentions
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            // Register all services in the assembly
            services.AddScoped<IBugManager, BugManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IProjectManager,ProjectMananger>();
            services.AddScoped<IAttachmentManager, AttachmentManager>();
            services.AddScoped<IBugUserManager, BugUserManager>();



            // Register all validators in the assembly
            services.AddValidatorsFromAssembly(typeof(BusinessExtentions).Assembly);
        }

    }
}
