using PrisonApi.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace PrisonApi.Data;

public static class SeedData
{
    public static void SeedAdmin(PrisonContext context)
    {
        // Nếu đã có admin thì bỏ qua
        if (context.TaiKhoans.Any(t => t.Ten == "admin"))
            return;

        // Tạo hồ sơ admin
        var hoSo = new HoSo
        {
            HoTen = "Quản trị viên",
            Email = "admin@prison.local",
            SDT = "0123456789",
            DiaChi = "Hệ thống",
            GhiChu = "Tài khoản hệ thống"
        };
        context.HoSos.Add(hoSo);
        context.SaveChanges();

        // Tạo tài khoản admin
        var admin = new TaiKhoan
        {
            Ten = "admin",
            MatKhauHash = BCrypt.Net.BCrypt.HashPassword("123456"),
            QuyenId = 1,   // Quyền Admin đã seed trong OnModelCreating()
            HoSoId = hoSo.Id
        };

        context.TaiKhoans.Add(admin);
        context.SaveChanges();
    }
}

