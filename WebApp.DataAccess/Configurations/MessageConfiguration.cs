using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.DataAccess.Entities;

namespace WebApp.DataAccess.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<MessagesEntity>
    {
        public void Configure(EntityTypeBuilder<MessagesEntity> builder)
        {
            builder.HasKey(x => x.id);

            builder.Property(b => b.serialnumber).IsRequired();

            builder.Property(b => b.message)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(b => b.sendtime).IsRequired();
        }
    }
}
