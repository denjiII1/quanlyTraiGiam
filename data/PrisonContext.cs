using Microsoft.EntityFrameworkCore;
using PrisonApi.Models;

namespace PrisonApi.Data;
public class PrisonContext : DbContext
{
    public PrisonContext(DbContextOptions<PrisonContext> options) : base(options) { }

    public DbSet<Quyen> Quyens { get; set; }
    public DbSet<HoSo> HoSos { get; set; }
    public DbSet<TaiKhoan> TaiKhoans { get; set; }
    public DbSet<LichSuTruyCap> LichSuTruyCaps { get; set; }
    public DbSet<PhongGiam> PhongGiams { get; set; }
    public DbSet<PhamNhan> PhamNhans { get; set; }
    public DbSet<CanBo> CanBos { get; set; }
    public DbSet<KyLuat> KyLuats { get; set; }
    public DbSet<KhenThuong> KhenThuongs { get; set; }
    public DbSet<LichSinhHoat> LichSinhHoats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // seed roles
        modelBuilder.Entity<Quyen>().HasData(
            new Quyen { Id = 1, Ten = "Admin", MoTa = "Quản trị hệ thống" },
            new Quyen { Id = 2, Ten = "QuanLy", MoTa = "Cán bộ quản lý" },
            new Quyen { Id = 3, Ten = "NhanVien", MoTa = "Nhân viên trại" }
        );

        // configure relations optional
    }
}
