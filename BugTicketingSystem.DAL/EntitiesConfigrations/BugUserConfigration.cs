
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL.EntitiesConfigrations
{
    public class BugUserConfigration: IEntityTypeConfiguration<BugUser>
    {
        public void Configure(EntityTypeBuilder<BugUser> builder)
        {
            builder.HasKey(bu => new { bu.BugId, bu.UserId });
            builder.HasOne(bu => bu.Bug)
                .WithMany(b => b.BugUsers)
                .HasForeignKey(bu => bu.BugId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(bu => bu.User)
                .WithMany(u => u.BugUsers)
                .HasForeignKey(bu => bu.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    
}
