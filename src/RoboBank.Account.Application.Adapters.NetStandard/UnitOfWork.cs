using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RoboBank.Account.Application.Adapters.NetStandard
{
    public class UnitOfWork : DbContext
    {
        public DbSet<Domain.Account> Accounts { get; set; }

        public DbSet<Domain.Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("UnitOfWork"));
        }
    }
}
