using Microsoft.EntityFrameworkCore;
using RentalQuotationApp.Models;

namespace RentalQuotationApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure correct configuration
            modelBuilder.Entity<Quotation>().ToTable("Quotations");
            modelBuilder.Entity<Quotation>().Property(q => q.ApprovalStatus).IsRequired(); // If it's required
        }
        public DbSet<CostComponent> CostComponents { get; set; }
    }
}
