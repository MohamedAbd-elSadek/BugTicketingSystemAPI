using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BugTicketingSystem.DAL.Data
{
    public static class SeedData
    {
        public static async Task Initialize(BugDbContext context, UserManager<User> userManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            // Seed Users with Identity
            var users = new List<User>
                {
                    new User
                    {
                        UserName = "ahmed",
                        Email = "ahmed@gmail.com",
                        Name = "Ahmed Abdo",
                        Role = UserRole.Developer
                    },
                    new User
                    {
                        UserName = "ali",
                        Email = "ali@gmail.com",
                        Name = "Ali Mohamed",
                        Role = UserRole.Manager
                    },
                    new User
                    {
                        UserName = "mohamed",
                        Email = "mohamed@gmail.com",
                        Name = "Mohamed Loai",
                        Role = UserRole.Tester
                    }
                };

            // Create users with passwords
            foreach (var user in users)
            {
                var result = await userManager.CreateAsync(user, "Password@123");
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                if (!result.Succeeded)
                throw new Exception($"Failed to create user {user.Email}: {errors}");
            }
            
            var ali = await userManager.FindByEmailAsync("ali@gmail.com");
            if (ali == null) throw new Exception("Ali not found!");
            var ahmed = await userManager.FindByEmailAsync("ahmed@gmail.com");
            if (ahmed == null) throw new Exception("Ahmed not found!");
            var mohamed = await userManager.FindByEmailAsync("mohamed@gmail.com");
            if (mohamed == null) throw new Exception("Mohamed not found!");

            // Seed Projects
            var projects = new List<Project>
                {
                    new Project
                    {
                        Id = Guid.NewGuid(),
                        Name = "Project Alpha",
                        Description = "First project",

                        
                    },
                    new Project
                    {
                        Id = Guid.NewGuid(),
                        Name = "Project Beta",
                        Description = "Second project",
                    },
                    new Project
                    {
                        Id = Guid.NewGuid(),
                        Name = "Project Gamma",
                        Description = "Third project",
                    }
                };

            await context.Projects.AddRangeAsync(projects);
            await context.SaveChangesAsync();

            // Seed Bugs
            var bugs = new List<Bug>
                {
                    new Bug
                    {
                        Id = Guid.NewGuid(),
                        Title = "Login Failure",
                        Description = "Users cannot log in after update",
                        Status = BugStatus.Open,
                        ProjectId = projects[0].Id, // Corrected reference to project ID
                    },
                    new Bug
                    {
                        Id = Guid.NewGuid(),
                        Title = "UI Misalignment",
                        Description = "Buttons overlap on mobile view",
                        Status = BugStatus.Open,
                        ProjectId = projects[1].Id // Corrected reference to project ID
                    },
                    new Bug
                    {
                        Id = Guid.NewGuid(),
                        Title = "Performance Issue",
                        Description = "Slow response time on dashboard",
                        Status = BugStatus.Open,
                        ProjectId = projects[2].Id // Corrected reference to project ID
                    }
                };

            await context.Bugs.AddRangeAsync(bugs);
            await context.SaveChangesAsync();

            // Seed Bug-User Assignments
            var bugUsers = new List<BugUser>
                {
                    new BugUser { BugId = bugs[0].Id, UserId = ahmed.Id },
                    new BugUser { BugId = bugs[1].Id, UserId = ali.Id },
                    new BugUser { BugId = bugs[2].Id, UserId = mohamed.Id }
                };

            await context.BugUsers.AddRangeAsync(bugUsers);
            await context.SaveChangesAsync();

            // Seed Attachments
            var attachments = new List<Attachment>
                {
                    new Attachment
                    {
                        Id = Guid.NewGuid(),
                        FileName = "screenshot.png",
                        FilePath = "/attachments/screenshot1.png",
                        BugId = bugs[0].Id
                    },
                    new Attachment
                    {
                        Id = Guid.NewGuid(),
                        FileName = "error.log",
                        FilePath = "/attachments/error.log",
                        BugId = bugs[1].Id
                    },
                    new Attachment
                    {
                        Id = Guid.NewGuid(),
                        FileName = "report.pdf",
                        FilePath = "/attachments/report.pdf",
                        BugId = bugs[2].Id
                    }
                };

            await context.Attachments.AddRangeAsync(attachments);
            await context.SaveChangesAsync();
        }
    }
    
}