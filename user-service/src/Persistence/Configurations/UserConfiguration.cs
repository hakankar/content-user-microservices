using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.ToTable("users");
            builder.HasKey(t => t.Id);

            builder.Property(t=> t.FullName)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Email)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(t => t.Password)
                .IsRequired();

            builder.Property(t => t.IsDeleted)
               .HasDefaultValue(false);
            builder.Property(t => t.CreatedDate)
               .HasDefaultValueSql("now()");

            builder.HasIndex(t=> t.Email).IsUnique();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
