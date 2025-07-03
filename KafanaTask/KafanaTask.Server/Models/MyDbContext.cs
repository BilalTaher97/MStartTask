using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KafanaTask.Server.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-L41TH4PM;Database=MStartDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC27EA4F03A5");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D10534786C3640").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateOfBirth).HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DateTimeUtc)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("DateTime_UTC");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.GenderAr)
                .HasMaxLength(10)
                .HasColumnName("Gender_AR");
            entity.Property(e => e.GenderEn)
                .HasMaxLength(10)
                .HasColumnName("Gender_EN");
            entity.Property(e => e.LastLoginDateTimeUtc).HasColumnName("Last_Login_DateTime_UTC");
            entity.Property(e => e.NameAr)
                .HasMaxLength(255)
                .HasColumnName("Name_AR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("Name_EN");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(512)
                .HasColumnName("Password_Hash");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.ServerDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Server_DateTime");
            entity.Property(e => e.StatusAr)
                .HasMaxLength(20)
                .HasDefaultValue("نشط")
                .HasColumnName("Status_AR");
            entity.Property(e => e.StatusEn)
                .HasMaxLength(20)
                .HasDefaultValue("Active")
                .HasColumnName("Status_EN");
            entity.Property(e => e.UpdateDateTimeUtc).HasColumnName("Update_DateTime_UTC");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC275BC5E339");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.DateTimeUtc)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("DateTime_UTC");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.ServerDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Server_DateTime");
            entity.Property(e => e.StatusAr)
                .HasMaxLength(20)
                .HasDefaultValue("نشط")
                .HasColumnName("Status_AR");
            entity.Property(e => e.StatusEn)
                .HasMaxLength(20)
                .HasDefaultValue("Active")
                .HasColumnName("Status_EN");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Total_Amount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Customer");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC2708805CE3");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD");
            entity.Property(e => e.DateTimeUtc)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("DateTime_UTC");
            entity.Property(e => e.DescriptionAr).HasColumnName("Description_AR");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_EN");
            entity.Property(e => e.NameAr)
                .HasMaxLength(255)
                .HasColumnName("Name_AR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("Name_EN");
            entity.Property(e => e.ServerDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Server_DateTime");
            entity.Property(e => e.StatusAr)
                .HasMaxLength(20)
                .HasDefaultValue("نشط")
                .HasColumnName("Status_AR");
            entity.Property(e => e.StatusEn)
                .HasMaxLength(20)
                .HasDefaultValue("Active")
                .HasColumnName("Status_EN");
            entity.Property(e => e.UpdateDateTimeUtc).HasColumnName("Update_DateTime_UTC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
