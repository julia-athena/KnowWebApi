using System.IO;
using DataAccess.Extensions;
using DataAccess.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class KnowDbContext : IdentityDbContext<KnowUser>
    {
        public KnowDbContext(DbContextOptions<KnowDbContext> options)
            : base(options){}
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Creator> Creators { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (Database.IsNpgsql())
            {
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(KnowDbContext).Assembly);
            }
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}