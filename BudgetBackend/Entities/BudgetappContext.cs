using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BudgetBackend.Entities
{
    public partial class BudgetAppContext : DbContext
    {
        public BudgetAppContext()
        {
        }

        public BudgetAppContext(DbContextOptions<BudgetAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<MonthlyBill> MonthlyBills { get; set; }
        public virtual DbSet<MonthlyIncome> MonthlyIncomes { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost; Port=5432; User Id=appuser; Password=postgres; Database=BudgetApp;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget>(entity =>
            {
                entity.ToTable("Budget");

                entity.HasIndex(e => e.Id, "IX_Budget");

                entity.HasIndex(e => e.MonthlyIncomeId, "IX_WeeklyBudget_MonthlyIncomeId");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MonthlyIncome)
                    .WithMany(p => p.Budgets)
                    .HasForeignKey(d => d.MonthlyIncomeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Budget_MonthlyIncome");
            });

            modelBuilder.Entity<MonthlyBill>(entity =>
            {
                entity.ToTable("MonthlyBill");

                entity.HasIndex(e => e.MonthlyIncomeId, "IX_MonthlyBill_MonthlyIncomeId");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MonthlyIncome)
                    .WithMany(p => p.MonthlyBills)
                    .HasForeignKey(d => d.MonthlyIncomeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MonthlyBill_MonthlyIncome");
            });

            modelBuilder.Entity<MonthlyIncome>(entity =>
            {
                entity.ToTable("MonthlyIncome");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.LastPayDay).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.HasIndex(e => e.BudgetId, "IX_Transaction_BudgetId");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.TransactionDate).HasColumnType("timestamp without time zone");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.BudgetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Budget");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
