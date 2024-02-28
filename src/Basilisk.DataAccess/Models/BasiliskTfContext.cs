using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Basilisk.DataAccess.Models;

public partial class BasiliskTfContext : DbContext
{
    public BasiliskTfContext()
    {
    }

    public BasiliskTfContext(DbContextOptions<BasiliskTfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CompleteProduct> CompleteProducts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Salesman> Salesmen { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<Config> Configs { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => e.Name);
            entity.Property(e => e.Description)
            .HasMaxLength(1000)
            .IsUnicode(false);
            entity.Property(e => e.Name)
            .HasMaxLength(100)
            .IsUnicode(false);
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasConversion(
                    v => v.ToString(), // Convert enum to string for storage
                    v => (RoleEnum)Enum.Parse(typeof(RoleEnum), v) // Convert string back to enum when reading from the database
                );
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.Id, "UQ_Categories_CategoryName").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CompleteProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CompleteProduct");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SupplierCompanyName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DeleteDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.InvoiceNumber);

            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DeliveryCost).HasColumnType("money");
            entity.Property(e => e.DestinationAddress)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DestinationCity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DestinationPostalCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.SalesEmployeeNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ShippedDate).HasColumnType("date");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.Delivery).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeliveryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Deliveries");

            entity.HasOne(d => d.SalesEmployeeNumberNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SalesEmployeeNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Salesmen");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceNumber, e.ProductId }).HasName("PK_OrderDetails_1");

            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Discount).HasColumnType("money");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.InvoiceNumberNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.InvoiceNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_Products_Suppliers");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remark)
                .HasMaxLength(2000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Salesman>(entity =>
        {
            entity.HasKey(e => e.EmployeeNumber);

            entity.Property(e => e.EmployeeNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HiredDate).HasColumnType("date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Level)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SuperiorEmployeeNumber)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.SuperiorEmployeeNumberNavigation).WithMany(p => p.InverseSuperiorEmployeeNumberNavigation)
                .HasForeignKey(d => d.SuperiorEmployeeNumber)
                .HasConstraintName("FK_Salesmen_Salesmen");

            entity.HasMany(d => d.Regions).WithMany(p => p.SalesmanEmployeeNumbers)
                .UsingEntity<Dictionary<string, object>>(
                    "SalesmenRegion",
                    r => r.HasOne<Region>().WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SalesmenRegions_Regions"),
                    l => l.HasOne<Salesman>().WithMany()
                        .HasForeignKey("SalesmanEmployeeNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SalesmenRegions_Salesmen"),
                    j =>
                    {
                        j.HasKey("SalesmanEmployeeNumber", "RegionId");
                        j.ToTable("SalesmenRegions");
                        j.IndexerProperty<string>("SalesmanEmployeeNumber")
                            .HasMaxLength(20)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DeleteDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JobTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
