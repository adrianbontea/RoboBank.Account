using System.Data.Entity;

namespace RoboBank.Account.Application.Adapters
{
    public class UnitOfWork : DbContext
    {
        public DbSet<Domain.Account> Accounts { get; set; }

        public DbSet<Domain.Card> Cards { get; set; }
    }
}
