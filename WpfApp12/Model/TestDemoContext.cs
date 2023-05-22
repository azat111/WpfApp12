using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace WpfApp12.Model;

public partial class TestDemoContext : DbContext
{
    public static TestDemoContext _context;
	public static TestDemoContext context = new TestDemoContext();
	public TestDemoContext()
    {
    }

    public TestDemoContext(DbContextOptions<TestDemoContext> options)
        : base(options)
    {
    }
	public static TestDemoContext getcontext()
	{
        if (_context==null)
        {
            _context = new TestDemoContext();
        }
        return _context;
	}
	public virtual DbSet<Corzina> Corzinas { get; set; }

    public virtual DbSet<CorzinaOfTovari> CorzinaOfTovaris { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tovari> Tovaris { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-0QF8C26;Database=TestDemo;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Corzina>(entity =>
        {
            entity.ToTable("Corzina");
        });

        modelBuilder.Entity<CorzinaOfTovari>(entity =>
        {
            entity.HasKey(e => new { e.IdCorzina, e.IdTovari });

            entity.ToTable("CorzinaOfTovari");

            entity.Property(e => e.IdCorzina).HasColumnName("idCorzina");
            entity.Property(e => e.IdTovari).HasColumnName("idTovari");

            entity.HasOne(d => d.IdCorzinaNavigation).WithMany(p => p.CorzinaOfTovaris)
                .HasForeignKey(d => d.IdCorzina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CorzinaOfTovari_Corzina");

            entity.HasOne(d => d.IdTovariNavigation).WithMany(p => p.CorzinaOfTovaris)
                .HasForeignKey(d => d.IdTovari)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CorzinaOfTovari_Tovari");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Tovari>(entity =>
        {
            entity.ToTable("Tovari");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");

            entity.HasOne(d => d.Corzina).WithMany(p => p.Users)
                .HasForeignKey(d => d.CorzinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Corzina");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
