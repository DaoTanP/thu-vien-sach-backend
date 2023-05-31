using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLyThuVien.Models
{
    public partial class ThuVien : DbContext
    {
        public ThuVien()
            : base("name=ThuVien")
        {
        }

        public virtual DbSet<DanhSachYeuThich> DanhSachYeuThiches { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TacGia> TacGias { get; set; }
        public virtual DbSet<TheLoai> TheLoais { get; set; }
        public virtual DbSet<ThongTinMuonSach> ThongTinMuonSaches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DanhSachYeuThich>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<DanhSachYeuThich>()
                .Property(e => e.NguoiDung_Id)
                .IsUnicode(false);

            modelBuilder.Entity<DanhSachYeuThich>()
                .Property(e => e.Sach_Id)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Email)
                .IsUnicode(false);
            
            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);
            
            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.AnhDaiDien)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.SoThe)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MatKhauThe)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.DanhSachYeuThiches)
                .WithRequired(e => e.NguoiDung)
                .HasForeignKey(e => e.NguoiDung_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.ThongTinMuonSaches)
                .WithRequired(e => e.NguoiDung)
                .HasForeignKey(e => e.NguoiMuon_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaXuatBan>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<NhaXuatBan>()
                .HasMany(e => e.Saches)
                .WithRequired(e => e.NhaXuatBan)
                .HasForeignKey(e => e.NhaXuatBan_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.TheLoai_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.TacGia_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.NhaXuatBan_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.AnhBia)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.DanhSachYeuThiches)
                .WithRequired(e => e.Sach)
                .HasForeignKey(e => e.Sach_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.ThongTinMuonSaches)
                .WithRequired(e => e.Sach)
                .HasForeignKey(e => e.Sach_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TacGia>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<TacGia>()
                .HasMany(e => e.Saches)
                .WithRequired(e => e.TacGia)
                .HasForeignKey(e => e.TacGia_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TheLoai>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<TheLoai>()
                .HasMany(e => e.Saches)
                .WithRequired(e => e.TheLoai)
                .HasForeignKey(e => e.TheLoai_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThongTinMuonSach>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinMuonSach>()
                .Property(e => e.NguoiMuon_Id)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinMuonSach>()
                .Property(e => e.Sach_Id)
                .IsUnicode(false);
        }
    }
}
