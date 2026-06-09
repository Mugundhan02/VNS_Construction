using BuildManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Contexts
{
    public class BuildManagerDbContext : DbContext
    {
        public BuildManagerDbContext(DbContextOptions<BuildManagerDbContext> options)
            : base(options)
        {
        }

        // ── Masters ──────────────────────────────────────────────────────────

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<CompanyBank> CompanyBanks { get; set; }
        public DbSet<OfficeExpense> OfficeExpenses { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Whom> Whoms { get; set; }
        public DbSet<InstallmentTerm> InstallmentTerms { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SubContractor> SubContractors { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<JobWork> JobWorks { get; set; }

        // ── Transactions ─────────────────────────────────────────────────────

        public DbSet<ClientTransaction> ClientTransactions { get; set; }
        public DbSet<SupplierTransaction> SupplierTransactions { get; set; }
        public DbSet<SubContractorTransaction> SubContractorTransactions { get; set; }
        public DbSet<CompanyExpenseTransaction> CompanyExpenseTransactions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        // ── Model Configuration ───────────────────────────────────────────────

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Company ──
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyId);
                entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(200);

                entity.OwnsOne(e => e.Address, address =>
                {
                    address.Property(a => a.PinCode).HasMaxLength(10);
                });

                entity.OwnsOne(e => e.ContactInfo, contact =>
                {
                    contact.Property(c => c.PhoneNumber).HasMaxLength(20);
                    contact.Property(c => c.MobileNumber).HasMaxLength(20);
                    contact.Property(c => c.EmailId).HasMaxLength(200);
                });

                entity.OwnsOne(e => e.IdentityDetails, identity =>
                {
                    identity.Property(i => i.PanCardNumber).HasMaxLength(20);
                    identity.Property(i => i.TinNumber).HasMaxLength(30);
                    identity.Property(i => i.CstNumber).HasMaxLength(30);
                });
            });

            // ── RefreshToken ──
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.RefreshTokenId);
                entity.Property(e => e.Token).IsRequired();
                entity.HasIndex(e => e.Token).IsUnique();

                entity.HasOne(e => e.CompanyUser)
                      .WithMany(u => u.RefreshTokens)
                      .HasForeignKey(e => e.CompanyUserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── CompanyUser ──
            modelBuilder.Entity<CompanyUser>(entity =>
            {
                entity.HasKey(e => e.CompanyUserId);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.PasswordSalt).IsRequired();
                entity.Property(e => e.UserType).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.UserName).IsUnique();

                entity.HasOne(e => e.Company)
                      .WithMany(c => c.CompanyUsers)
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── CompanyBank ──
            modelBuilder.Entity<CompanyBank>(entity =>
            {
                entity.HasKey(e => e.CompanyBankId);
                entity.Property(e => e.BankName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.IfscCode).HasMaxLength(20);

                entity.HasOne(e => e.Company)
                      .WithMany(c => c.CompanyBanks)
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── OfficeExpense ──
            modelBuilder.Entity<OfficeExpense>(entity =>
            {
                entity.HasKey(e => e.OfficeExpenseId);
                entity.Property(e => e.ExpenseName).IsRequired().HasMaxLength(200);
                entity.HasIndex(e => e.ExpenseName).IsUnique();
            });

            // ── PaymentType ──
            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.HasKey(e => e.PaymentTypeId);
                entity.Property(e => e.PaymentTypeName).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.PaymentTypeName).IsUnique();
            });

            // ── Whom ──
            modelBuilder.Entity<Whom>(entity =>
            {
                entity.HasKey(e => e.WhomId);
                entity.Property(e => e.WhomName).IsRequired().HasMaxLength(200);
                entity.HasIndex(e => e.WhomName).IsUnique();
            });

            // ── InstallmentTerm ──
            modelBuilder.Entity<InstallmentTerm>(entity =>
            {
                entity.HasKey(e => e.InstallmentTermId);
                entity.Property(e => e.TermName).IsRequired().HasMaxLength(200);
            });

            // ── Client ──
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientId);
                entity.Property(e => e.ClientName).IsRequired().HasMaxLength(200);

                entity.OwnsOne(e => e.Address, address =>
                {
                    address.Property(a => a.PinCode).HasMaxLength(10);
                });

                entity.OwnsOne(e => e.ContactInfo, contact =>
                {
                    contact.Property(c => c.MobileNumber).HasMaxLength(20);
                    contact.Property(c => c.EmailId).HasMaxLength(200);
                });

                entity.OwnsOne(e => e.IdentityDetails, identity =>
                {
                    identity.Property(i => i.PanCardNumber).HasMaxLength(20);
                });

                entity.Property(e => e.EstimateUnit).HasPrecision(18, 4);
                entity.Property(e => e.EstimateRate).HasPrecision(18, 4);
                entity.Property(e => e.EstimateAmount).HasPrecision(18, 2);
            });

            // ── Supplier ──
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId);
                entity.Property(e => e.SupplierName).IsRequired().HasMaxLength(200);

                entity.OwnsOne(e => e.Address, address =>
                {
                    address.Property(a => a.PinCode).HasMaxLength(10);
                });

                entity.OwnsOne(e => e.ContactInfo, contact =>
                {
                    contact.Property(c => c.MobileNumber).HasMaxLength(20);
                    contact.Property(c => c.EmailId).HasMaxLength(200);
                });
            });

            // ── SubContractor ──
            modelBuilder.Entity<SubContractor>(entity =>
            {
                entity.HasKey(e => e.SubContractorId);
                entity.Property(e => e.SubContractorName).IsRequired().HasMaxLength(200);

                entity.OwnsOne(e => e.Address, address =>
                {
                    address.Property(a => a.PinCode).HasMaxLength(10);
                });

                entity.OwnsOne(e => e.ContactInfo, contact =>
                {
                    contact.Property(c => c.MobileNumber).HasMaxLength(20);
                });

                entity.OwnsOne(e => e.WorkDetails, work =>
                {
                    work.Property(w => w.Rate).HasPrecision(18, 4);
                });
            });

            // ── Material ──
            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.MaterialId);
                entity.Property(e => e.MaterialName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Unit).HasMaxLength(50);
                entity.Property(e => e.Rate).HasPrecision(18, 4);
                entity.HasIndex(e => e.MaterialName).IsUnique();
            });

            // ── JobWork ──
            modelBuilder.Entity<JobWork>(entity =>
            {
                entity.HasKey(e => e.JobWorkId);
                entity.Property(e => e.JobWorkName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Unit).HasMaxLength(50);
                entity.Property(e => e.Rate).HasPrecision(18, 4);
                entity.HasIndex(e => e.JobWorkName).IsUnique();
            });

            // ── ClientTransaction ──
            modelBuilder.Entity<ClientTransaction>(entity =>
            {
                entity.HasKey(e => e.ClientTransactionId);
                entity.Property(e => e.CreditAmount).HasPrecision(18, 2);
                entity.Property(e => e.DebitAmount).HasPrecision(18, 2);

                entity.HasOne(e => e.Client)
                      .WithMany(c => c.ClientTransactions)
                      .HasForeignKey(e => e.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.PaymentType)
                      .WithMany()
                      .HasForeignKey(e => e.PaymentTypeId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.ByWhom)
                      .WithMany()
                      .HasForeignKey(e => e.ByWhomId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // ── SupplierTransaction ──
            modelBuilder.Entity<SupplierTransaction>(entity =>
            {
                entity.HasKey(e => e.SupplierTransactionId);
                entity.Property(e => e.Quantity).HasPrecision(18, 4);
                entity.Property(e => e.Rate).HasPrecision(18, 4);
                entity.Property(e => e.Amount).HasPrecision(18, 2);
                entity.Property(e => e.PaidAmount).HasPrecision(18, 2);
                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.HasOne(e => e.Client)
                      .WithMany(c => c.SupplierTransactions)
                      .HasForeignKey(e => e.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Supplier)
                      .WithMany(s => s.SupplierTransactions)
                      .HasForeignKey(e => e.SupplierId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Material)
                      .WithMany(m => m.SupplierTransactions)
                      .HasForeignKey(e => e.MaterialId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.PaymentType)
                      .WithMany()
                      .HasForeignKey(e => e.PaymentTypeId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.ToWhom)
                      .WithMany()
                      .HasForeignKey(e => e.ToWhomId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // ── SubContractorTransaction ──
            modelBuilder.Entity<SubContractorTransaction>(entity =>
            {
                entity.HasKey(e => e.SubContractorTransactionId);
                entity.Property(e => e.Quantity).HasPrecision(18, 4);
                entity.Property(e => e.Rate).HasPrecision(18, 4);
                entity.Property(e => e.Amount).HasPrecision(18, 2);
                entity.Property(e => e.PaidAmount).HasPrecision(18, 2);
                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.HasOne(e => e.Client)
                      .WithMany(c => c.SubContractorTransactions)
                      .HasForeignKey(e => e.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SubContractor)
                      .WithMany(sc => sc.SubContractorTransactions)
                      .HasForeignKey(e => e.SubContractorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.JobWork)
                      .WithMany(j => j.SubContractorTransactions)
                      .HasForeignKey(e => e.JobWorkId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.PaymentType)
                      .WithMany()
                      .HasForeignKey(e => e.PaymentTypeId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.ToWhom)
                      .WithMany()
                      .HasForeignKey(e => e.ToWhomId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // ── CompanyExpenseTransaction ──
            modelBuilder.Entity<CompanyExpenseTransaction>(entity =>
            {
                entity.HasKey(e => e.CompanyExpenseTransactionId);
                entity.Property(e => e.Amount).HasPrecision(18, 2);
                entity.Property(e => e.ReceivedAmount).HasPrecision(18, 2);
                entity.Property(e => e.PaidAmount).HasPrecision(18, 2);

                entity.HasOne(e => e.Client)
                      .WithMany(c => c.CompanyExpenseTransactions)
                      .HasForeignKey(e => e.ClientId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Company)
                      .WithMany()
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.OfficeExpense)
                      .WithMany(o => o.CompanyExpenseTransactions)
                      .HasForeignKey(e => e.OfficeExpenseId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.PaymentType)
                      .WithMany()
                      .HasForeignKey(e => e.PaymentTypeId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.ToWhom)
                      .WithMany()
                      .HasForeignKey(e => e.ToWhomId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // ── AuditLog ──
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(e => e.AuditLogId);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Action).IsRequired().HasMaxLength(100);
                entity.Property(e => e.EntityType).IsRequired().HasMaxLength(100);
            });
        }
    }
}