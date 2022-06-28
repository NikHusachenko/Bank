using Bank.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bank.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<CardEntity> Cards { get; set; }
        public DbSet<BankAccountEntity> Accounts { get; set; }

        public ApplicationDbContext() : base()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=BankProject;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientEntity>().Property(client => client.Name).HasMaxLength(15);

            modelBuilder.Entity<ClientEntity>()
                .HasOne<BankAccountEntity>(client => client.Account)
                .WithOne(account => account.Client)
                .HasForeignKey<BankAccountEntity>(account => account.ClientFK)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BankAccountEntity>()
                .HasMany<CardEntity>(account => account.Cards)
                .WithOne(card => card.Account)
                .HasForeignKey(card => card.AccountFK);
        }
    }
}