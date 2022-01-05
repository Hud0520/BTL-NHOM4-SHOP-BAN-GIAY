using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BTL_NHOM4.Models
{
    public partial class DBQLBanGiay : DbContext
    {
        public DBQLBanGiay()
            : base("name=DBQLBanGiay")
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHang { get; set; }
        public virtual DbSet<DonHang> DonHang { get; set; }
        public virtual DbSet<Giay> Giay { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<TinTuc> TinTuc { get; set; }
        public virtual DbSet<TheLoai> TheLoai { get; set; }
        public virtual DbSet<Slide> Slide { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.Gia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Giay>()
                .Property(e => e.KichThuoc)
                .IsUnicode(false);

            modelBuilder.Entity<Giay>()
                .Property(e => e.Anh)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TinTuc>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<TinTuc>()
                .Property(e => e.AnhTieuDe)
                .IsUnicode(false);
        }
    }
}
