﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BudgetBackend.Entities
{
    public partial class BudgetappContext : DbContext
    {
        public BudgetappContext()
        {
        }

        public BudgetappContext(DbContextOptions<BudgetappContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MonthlyBill> MonthlyBills { get; set; }
        public virtual DbSet<MonthlyIncome> MonthlyIncomes { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<WeeklyBudget> WeeklyBudgets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MonthlyBill>(entity =>
            {
                entity.ToTable("MonthlyBill");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

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

                entity.Property(e => e.LastPayDay).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("(current_timestamp)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("timestamp");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.BudgetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Budget");
            });

            modelBuilder.Entity<WeeklyBudget>(entity =>
            {
                entity.ToTable("WeeklyBudget");

                entity.HasIndex(e => e.Id, "IX_Budget");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MonthlyIncome)
                    .WithMany(p => p.WeeklyBudgets)
                    .HasForeignKey(d => d.MonthlyIncomeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Budget_MonthlyIncome");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}