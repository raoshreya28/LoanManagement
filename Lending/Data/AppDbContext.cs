using BCrypt.Net;
using Lending.Models;
using Microsoft.EntityFrameworkCore;

namespace Lending.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanOfficer> LoanOfficers { get; set; }
        public DbSet<LoanAdmin> LoanAdmins { get; set; }
        public DbSet<LoanScheme> LoanSchemes { get; set; }
        public DbSet<Repayment> Repayments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Example: configure table names if needed
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<LoanApplication>().ToTable("LoanApplications");
            modelBuilder.Entity<LoanOfficer>().ToTable("LoanOfficers");
            modelBuilder.Entity<LoanAdmin>().ToTable("LoanAdmins");
            modelBuilder.Entity<Loan>().ToTable("Loans");
            modelBuilder.Entity<LoanScheme>().ToTable("LoanSchemes");
            modelBuilder.Entity<Repayment>().ToTable("Repayments");
            modelBuilder.Entity<Document>().ToTable("Documents");
            modelBuilder.Entity<Notification>().ToTable("Notifications");
            modelBuilder.Entity<Report>().ToTable("Reports");
            base.OnModelCreating(modelBuilder);

            // -------------------------
            // USER / CUSTOMER TABLES
            // -------------------------
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<LoanAdmin>().ToTable("LoanAdmins");
            modelBuilder.Entity<LoanOfficer>().ToTable("LoanOfficers");

            // Customer → LoanApplications: cascade allowed
            modelBuilder.Entity<LoanApplication>()
                .HasOne(l => l.Customer)
                .WithMany(c => c.LoanApplications)
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Customer → Documents: cascade allowed
            modelBuilder.Entity<Document>()
                .HasOne(d => d.Customer)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // LoanApplication → Documents: **do not cascade**
            modelBuilder.Entity<Document>()
                .HasOne(d => d.LoanApplication)
                .WithMany(l => l.Documents)
                .HasForeignKey(d => d.LoanApplicationId)
                .OnDelete(DeleteBehavior.Restrict);  // or SetNull if nullable


            // -------------------------
            // Seed default LoanAdmin user
            // -------------------------
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("Admin@123");
            modelBuilder.Entity<LoanAdmin>().HasData(new LoanAdmin
            {
                UserId = 1,
                AdminDepartment = "Finance",
                Role = UserRole.LOANADMIN,
                UserName = "Default Admin",
                UserEmail = "admin@lending.com",
                PasswordHash = hashedPassword,
                UserPhone = "123-456-7890",
                Address = "Head Office",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            });
        }
    }
}
