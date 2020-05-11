using Microsoft.EntityFrameworkCore;
using Uploader.Domain.Entities;
using Uploader.Infrastructure.EntityConfigurations;

namespace Uploader.Infrastructure
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TransactionEntityConfiguration());
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}