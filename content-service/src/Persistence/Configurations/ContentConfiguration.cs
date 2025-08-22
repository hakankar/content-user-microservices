using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder) {
            builder.ToTable("contents");
            builder.HasKey(t => t.Id);

            builder.Property(t=> t.Title)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Body)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasIndex(t => t.UserId);

            builder.Property(t => t.IsDeleted)
               .HasDefaultValue(false);
            builder.Property(t => t.CreatedDate)
               .HasDefaultValueSql("now()");

            builder.HasIndex(t=> t.Title).IsUnique();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
