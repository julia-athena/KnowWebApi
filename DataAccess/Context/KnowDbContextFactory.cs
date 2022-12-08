using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context
{
    public class KnowDbContextFactory : IDesignTimeDbContextFactory<KnowDbContext>
    {
        public KnowDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<KnowDbContext>()
                .Build();
            var connectionString = configuration["ConnectionStrings:Default"];
            var optionsBuilder = new DbContextOptionsBuilder<KnowDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new KnowDbContext(optionsBuilder.Options);
        }
    }
}