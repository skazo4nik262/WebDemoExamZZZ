using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace WebApplication4;

public partial class User06Context : DbContext
{
    public User06Context()
    {
    }

    public User06Context(DbContextOptions<User06Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Floor> Floors { get; set; }

    public virtual DbSet<Fond> Fonds { get; set; }

    public virtual DbSet<FondStatus> FondStatuses { get; set; }

    public virtual DbSet<Postoialci> Postoialcis { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=192.168.200.35;user=user06;password=45358;database=user06", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.27-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Floor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Floor");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.FloorName).HasMaxLength(255);
        });

        modelBuilder.Entity<Fond>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Fond");

            entity.HasIndex(e => e.CategoryId, "FK_Fond_Category_Id");

            entity.HasIndex(e => e.FloorId, "FK_Fond_Floor_Id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.CategoryId).HasColumnType("int(11)");
            entity.Property(e => e.FloorId)
                .HasColumnType("int(11)")
                .HasColumnName("FloorID");
            entity.Property(e => e.Number).HasColumnType("int(11)");

            entity.HasOne(d => d.Category).WithMany(p => p.Fonds)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Fond_Category_Id");

            entity.HasOne(d => d.Floor).WithMany(p => p.Fonds)
                .HasForeignKey(d => d.FloorId)
                .HasConstraintName("FK_Fond_Floor_Id");
        });

        modelBuilder.Entity<FondStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("FondStatus");

            entity.HasIndex(e => e.CategotyId, "FK_FondStatus_Category_Id");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CategotyId).HasColumnType("int(11)");
            entity.Property(e => e.Number).HasColumnType("int(11)");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Categoty).WithMany(p => p.FondStatuses)
                .HasForeignKey(d => d.CategotyId)
                .HasConstraintName("FK_FondStatus_Category_Id");
        });

        modelBuilder.Entity<Postoialci>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Postoialci");

            entity.HasIndex(e => e.CategoryId, "FK_Postoialci_Category_Id");

            entity.HasIndex(e => e.FloorId, "FK_Postoilci_Floor_Id");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CategoryId).HasColumnType("int(11)");
            entity.Property(e => e.Client).HasMaxLength(50);
            entity.Property(e => e.DataIn).HasColumnName("Data In");
            entity.Property(e => e.DateOut).HasColumnName("Date Out");
            entity.Property(e => e.FloorId).HasColumnType("int(11)");
            entity.Property(e => e.Number).HasColumnType("int(11)");

            entity.HasOne(d => d.Category).WithMany(p => p.Postoialcis)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Postoialci_Category_Id");

            entity.HasOne(d => d.Floor).WithMany(p => p.Postoialcis)
                .HasForeignKey(d => d.FloorId)
                .HasConstraintName("FK_Postoilci_Floor_Id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.RoleId, "FK_User_Role_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasDefaultValueSql("''");
            entity.Property(e => e.RoleId).HasColumnType("int(11)");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
