
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL.EntitiesConfigrations
{
    public class AttachmentConfigration: IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FileName).IsRequired().HasMaxLength(200);
            builder.Property(a => a.FilePath).IsRequired().HasMaxLength(500);
            builder.HasOne(a => a.Bug)
                .WithMany(b => b.Attachments)
                .HasForeignKey(a => a.BugId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    
}
