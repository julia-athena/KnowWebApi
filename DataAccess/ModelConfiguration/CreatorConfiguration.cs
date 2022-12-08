using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.ModelConfiguration
{
    public class CreatorConfiguration : IEntityTypeConfiguration<Creator>
    {
        
        public void Configure(EntityTypeBuilder<Creator> builder)
        {
            builder.Property(c => c.FirstName)
                .HasMaxLength(128)
                .IsRequired();
            
            builder.Property(c => c.LastName)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasMaxLength(256)
                .IsRequired();
            builder.HasAlternateKey(c => c.Email);

            builder.Property(c => c.SoftDeleted)
                .HasDefaultValue(false)
                .IsRequired();
            builder.HasQueryFilter(c => !c.SoftDeleted);
        }
    }
}