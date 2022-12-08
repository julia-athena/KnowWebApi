using System;
using DataAccess.Extensions;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.ModelConfiguration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Created)
                .HasDefaultValueSql("now()"); 
            
            builder.Property(p => p.Title)
                .HasMaxLength(128)
                .IsRequired();
            
            //converting enum to varchar in db and vice versa
            builder.Property(p => p.State)
                .HasDefaultValue(PostState.Draft)
                .HasConversion(
                    p => p.GetDescription(),
                    p => p.GetEnumValueFromDescription<PostState>())
                .HasMaxLength(128);
           
            //add shadow property
            builder.Property<long>("FirstCreatorId");
            
            //use shadow property as foreign key
            builder
                .HasOne<Creator>(p => p.FirstCreator)
                .WithMany(c => c.CreatedPosts)
                .HasForeignKey("FirstCreatorId")
                .IsRequired();
            
            builder.Property(p => p.SoftDeleted)
                .HasDefaultValue(false)
                .IsRequired();
            
            builder.HasQueryFilter(p => !p.SoftDeleted);
        }  
    }
}