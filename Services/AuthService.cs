using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using PrisonApi.Data;
using PrisonApi.Models;
using PrisonApi.DTOs;

namespace PrisonApi.Services;
public class AuthService
{
    private readonly PrisonContext _db;
    private readonly IConfiguration _cfg;

    public AuthService(PrisonContext db, IConfiguration cfg) { _db = db; _cfg = cfg; }

    public async Task<TaiKhoan> RegisterAsync(RegisterDto dto)
    {
        if (await _db.TaiKhoans.AnyAsync(t => t.Ten == dto.Username)) return null;
        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new TaiKhoan { Ten = dto.Username, MatKhauHash = hash, QuyenId = dto.QuyenId, HoSoId = dto.HoSoId };
        _db.TaiKhoans.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _db.TaiKhoans.Include(t => t.Quyen).FirstOrDefaultAsync(t => t.Ten == dto.Username);
        if (user == null) return null;
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.MatKhauHash)) return null;

        // create jwt
        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.Ten),
            new Claim(ClaimTypes.Role, user.Quyen?.Ten ?? "User")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _cfg["Jwt:Issuer"],
            audience: _cfg["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_cfg["Jwt:ExpireMinutes"])),
            signingCredentials: creds
        );
        // save login history
        _db.LichSuTruyCaps.Add(new LichSuTruyCap { TaiKhoanTen = user.Ten, DiaChiIP = dto.IpAddress, MoTa = "Login" });
        await _db.SaveChangesAsync();

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<bool> ChangePasswordAsync(string username, ChangePasswordDto dto)
    {
        var user = await _db.TaiKhoans.FindAsync(username);
        if (user == null) return false;
        if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.MatKhauHash)) return false;
        user.MatKhauHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        await _db.SaveChangesAsync();
        return true;
    }
}
