using CrediBill_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CrediBill_Web.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurations des relations entre les modèles
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Invoices)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Payments)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId);

            // Configurer la suppression logique
            modelBuilder.Entity<Customer>()
                .HasQueryFilter(c => c.Deleted == DateTime.MaxValue);
            modelBuilder.Entity<Invoice>()
                .HasQueryFilter(i => i.Deleted == DateTime.MaxValue);
            modelBuilder.Entity<Payment>()
                .HasQueryFilter(p => p.Deleted == DateTime.MaxValue);

            // Configuration des décimaux
            modelBuilder.Entity<Invoice>()
                .Property(i => i.Amount)
                .HasPrecision(18, 2); // Précision totale: 18, après la virgule: 2

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);
        }
    }
}