using Lending.Models;
using Microsoft.EntityFrameworkCore;

namespace Lending.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LoanAdmin> LoanAdmins { get; set; }
        public DbSet<LoanOfficer> LoanOfficers { get; set; }
        public DbSet<LoanScheme> LoanSchemes { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Repayment> Repayments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map abstract base class User to table Users
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<LoanAdmin>().ToTable("LoanAdmins");
            modelBuilder.Entity<LoanOfficer>().ToTable("LoanOfficers");

            // CUSTOMER → LOANAPPLICATION (Cascade allowed)
            modelBuilder.Entity<LoanApplication>()
                .HasOne(l => l.Customer)
                .WithMany(c => c.LoanApplications)
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // CUSTOMER → DOCUMENTS (Cascade allowed)
            modelBuilder.Entity<Document>()
                .HasOne(d => d.Customer)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // LOANAPPLICATION → DOCUMENTS (Restrict to avoid multiple cascade paths)
            modelBuilder.Entity<Document>()
                .HasOne(d => d.LoanApplication)
                .WithMany(l => l.Documents)
                .HasForeignKey(d => d.LoanApplicationId)
                .OnDelete(DeleteBehavior.Restrict);

            // LOANAPPLICATION → REPAYMENTS (Cascade allowed)
            modelBuilder.Entity<Repayment>()
                .HasOne(r => r.LoanApplication)
                .WithMany(l => l.Repayments)
                .HasForeignKey(r => r.LoanApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // LOAN → CUSTOMER (Cascade allowed)
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Customer)
                .WithMany()
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // LOAN → LOANAPPLICATION (Restrict to avoid double cascade)
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.LoanApplication)
                .WithMany()
                .HasForeignKey(l => l.LoanApplicationId)
                .OnDelete(DeleteBehavior.Restrict);

            // NOTIFICATION → CUSTOMER (Cascade allowed)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Customer)
                .WithMany()
                .HasForeignKey(n => n.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // NOTIFICATION → LOANAPPLICATION (Restrict)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.LoanApplication)
                .WithMany()
                .HasForeignKey(n => n.LoanApplicationId)
                .OnDelete(DeleteBehavior.Restrict);

            // REPORT → LOANADMIN (Restrict to prevent admin delete cascade)
            modelBuilder.Entity<Report>()
                .HasOne(r => r.GeneratedBy)
                .WithMany()
                .HasForeignKey(r => r.GeneratedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed default LoanAdmin
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
