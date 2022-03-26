using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SalesTransaction.Models;

namespace SalesTransaction.Data
{
    public partial class TestDBContext : DbContext
    {
        public TestDBContext()
        {
        }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<TestTable> TestTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsBilled).HasColumnName("isBilled");
            });

            modelBuilder.Entity<TestTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TestTable");

                entity.Property(e => e.Address)
                    .HasMaxLength(30)
                    .HasColumnName("address");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
