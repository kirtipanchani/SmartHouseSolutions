using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartHouseSolutionsAPI.Models;

public partial class SmartHouseSolutionsDbContext : DbContext
{
    public SmartHouseSolutionsDbContext()
    {
    }

    public SmartHouseSolutionsDbContext(DbContextOptions<SmartHouseSolutionsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerDeviceRelation> CustomerDeviceRelations { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<DeviceType> DeviceTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PANCHANI;Database=SmartHouseSolutionsDB;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D87B882917");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.CustomerEmail, "UQ__Customer__3A0CE74C06E6A97A").IsUnique();

            entity.Property(e => e.CustomerEmail).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
        });

        modelBuilder.Entity<CustomerDeviceRelation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC074CB40724");

            entity.ToTable("CustomerDeviceRelation");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerDeviceRelations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerDeviceRelation_Customer");

            entity.HasOne(d => d.Device).WithMany(p => p.CustomerDeviceRelations)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerDeviceRelation_Device");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.DeviceId).HasName("PK__Device__49E1231182E74B3C");

            entity.ToTable("Device");

            entity.Property(e => e.DeviceName).HasMaxLength(255);

            entity.HasOne(d => d.DeviceType).WithMany(p => p.Devices)
                .HasForeignKey(d => d.DeviceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Device_DeviceType");
        });

        modelBuilder.Entity<DeviceType>(entity =>
        {
            entity.HasKey(e => e.DeviceTypeId).HasName("PK__DeviceTy__07A6C7F6B1B50D2C");

            entity.ToTable("DeviceType");

            entity.HasIndex(e => e.TypeName, "UQ__DeviceTy__D4E7DFA8E75196B1").IsUnique();

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
