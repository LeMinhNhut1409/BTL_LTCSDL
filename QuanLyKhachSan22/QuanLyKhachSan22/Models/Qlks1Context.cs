using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan22.Models;

public partial class Qlks1Context : DbContext
{
    public Qlks1Context()
    {
    }

    public Qlks1Context(DbContextOptions<Qlks1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Loaiphong> Loaiphongs { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Phieudatphong> Phieudatphongs { get; set; }

    public virtual DbSet<Phong> Phongs { get; set; }

    public virtual DbSet<Thongke> Thongkes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI\\Mi123;Initial Catalog=QLKS1;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.IdKh).HasName("PK__KHACHHAN__8B62EC895392AB2D");

            entity.ToTable("KHACHHANG");

            entity.Property(e => e.IdKh)
                .ValueGeneratedNever()
                .HasColumnName("ID_KH");
            entity.Property(e => e.DiaChiKh)
                .HasMaxLength(50)
                .HasColumnName("DiaChiKH");
            entity.Property(e => e.EmailKh)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Email_KH");
            entity.Property(e => e.HoTenKh)
                .HasMaxLength(100)
                .HasColumnName("HoTen_KH");
            entity.Property(e => e.NgaySinhKh).HasColumnName("NgaySinh_KH");
            entity.Property(e => e.SdtKh)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("Sdt_KH");
        });

        modelBuilder.Entity<Loaiphong>(entity =>
        {
            entity.HasKey(e => e.IdLp).HasName("PK__LOAIPHON__8B62F4B097EF21E3");

            entity.ToTable("LOAIPHONG");

            entity.Property(e => e.IdLp)
                .ValueGeneratedNever()
                .HasColumnName("ID_LP");
            entity.Property(e => e.MoTaLp)
                .HasMaxLength(100)
                .HasColumnName("MoTa_LP");
            entity.Property(e => e.TenLp)
                .HasMaxLength(100)
                .HasColumnName("Ten_LP");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.IdNv).HasName("PK__NHANVIEN__8B63E0638BE66C1A");

            entity.ToTable("NHANVIEN");

            entity.Property(e => e.IdNv).HasColumnName("ID_NV");
            entity.Property(e => e.HoTenNv)
                .HasMaxLength(100)
                .HasColumnName("HoTen_NV");
            entity.Property(e => e.NgaySinhNv).HasColumnName("NgaySinh_NV");
            entity.Property(e => e.Pass)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SdtNv)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("Sdt_NV");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Phieudatphong>(entity =>
        {
            entity.HasKey(e => e.IdPdp).HasName("PK__PHIEUDAT__20AF0B2A0593E987");

            entity.ToTable("PHIEUDATPHONG");

            entity.Property(e => e.IdPdp)
                .ValueGeneratedNever()
                .HasColumnName("ID_PDP");
            entity.Property(e => e.IdKh).HasColumnName("ID_KH");
            entity.Property(e => e.IdNv).HasColumnName("ID_NV");
            entity.Property(e => e.IdP).HasColumnName("ID_P");
            entity.Property(e => e.TongTien).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdKhNavigation).WithMany(p => p.Phieudatphongs)
                .HasForeignKey(d => d.IdKh)
                .HasConstraintName("FK__PHIEUDATP__ID_KH__403A8C7D");

            entity.HasOne(d => d.IdNvNavigation).WithMany(p => p.Phieudatphongs)
                .HasForeignKey(d => d.IdNv)
                .HasConstraintName("FK__PHIEUDATP__ID_NV__4222D4EF");

            entity.HasOne(d => d.IdPNavigation).WithMany(p => p.Phieudatphongs)
                .HasForeignKey(d => d.IdP)
                .HasConstraintName("FK__PHIEUDATPH__ID_P__412EB0B6");
        });

        modelBuilder.Entity<Phong>(entity =>
        {
            entity.HasKey(e => e.IdP).HasName("PK__PHONG__B87EA51C4E848FC6");

            entity.ToTable("PHONG");

            entity.Property(e => e.IdP)
                .ValueGeneratedNever()
                .HasColumnName("ID_P");
            entity.Property(e => e.GiaP).HasColumnName("Gia_P");
            entity.Property(e => e.HinhAnhPhong).HasMaxLength(100);
            entity.Property(e => e.IdLp).HasColumnName("ID_LP");
            entity.Property(e => e.TenP)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Ten_P");
            entity.Property(e => e.TinhTrang).HasMaxLength(100);

            entity.HasOne(d => d.IdLpNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.IdLp)
                .HasConstraintName("FK__PHONG__ID_LP__3D5E1FD2");
        });

        modelBuilder.Entity<Thongke>(entity =>
        {
            entity.HasKey(e => e.IdTk).HasName("PK__THONGKE__8B63B1A95C9DF23C");

            entity.ToTable("THONGKE");

            entity.Property(e => e.IdTk)
                .ValueGeneratedNever()
                .HasColumnName("ID_TK");
            entity.Property(e => e.IdPdp).HasColumnName("ID_PDP");
            entity.Property(e => e.ThanhTien).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPdpNavigation).WithMany(p => p.Thongkes)
                .HasForeignKey(d => d.IdPdp)
                .HasConstraintName("FK__THONGKE__ID_PDP__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
