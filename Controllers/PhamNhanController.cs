using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrisonApi.Data;
using PrisonApi.DTOs;
using PrisonApi.Models;

namespace PrisonApi.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PhamNhanController : ControllerBase
{
    private readonly PrisonContext _db;
    public PhamNhanController(PrisonContext db) { _db = db; }

    [HttpGet]
    public async Task<IActionResult> Query([FromQuery] string q, int? phongId, string trangThai, int page = 1, int pageSize = 20)
    {
        var query = _db.PhamNhans.Include(p => p.PhongGiam).AsQueryable();
        if (!string.IsNullOrEmpty(q)) query = query.Where(p => p.HoTen.Contains(q) || p.CCCD.Contains(q) || p.TomTatToiPham.Contains(q));
        if (phongId.HasValue) query = query.Where(p => p.PhongGiamId == phongId.Value);
        if (!string.IsNullOrEmpty(trangThai)) query = query.Where(p => p.TrangThai == trangThai);
        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return Ok(new { total, page, pageSize, items });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
        var p = await _db.PhamNhans.Include(x=>x.PhongGiam).FirstOrDefaultAsync(x=>x.Id==id);
        if (p==null) return NotFound();
        return Ok(p);
    }

    [HttpPost]
    [Authorize(Roles="Admin,QuanLy")]
    public async Task<IActionResult> Create([FromBody] PhamNhanCreateDto dto)
    {
        var entity = new PhamNhan {
            HoTen = dto.HoTen,
            NgaySinh = dto.NgaySinh,
            GioiTinh = dto.GioiTinh,
            CCCD = dto.CCCD,
            TomTatToiPham = dto.TomTatToiPham,
            MucAn = dto.MucAn,
            NgayVaoTrai = dto.NgayVaoTrai,
            PhongGiamId = dto.PhongGiamId,
            TrangThai = dto.TrangThai
        };
        _db.PhamNhans.Add(entity);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    [Authorize(Roles="Admin,QuanLy")]
    public async Task<IActionResult> Update(int id, [FromBody] PhamNhanUpdateDto dto)
    {
        var p = await _db.PhamNhans.FindAsync(id);
        if (p==null) return NotFound();
        p.HoTen = dto.HoTen; p.NgaySinh = dto.NgaySinh; p.GioiTinh = dto.GioiTinh;
        p.CCCD = dto.CCCD; p.TomTatToiPham = dto.TomTatToiPham; p.MucAn = dto.MucAn;
        p.NgayVaoTrai = dto.NgayVaoTrai; p.PhongGiamId = dto.PhongGiamId; p.TrangThai = dto.TrangThai;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var p = await _db.PhamNhans.FindAsync(id);
        if (p==null) return NotFound();
        _db.PhamNhans.Remove(p);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id}/transfer")]
    [Authorize(Roles="Admin,QuanLy")]
    public async Task<IActionResult> Transfer(int id, [FromQuery] int toPhongId)
    {
        var p = await _db.PhamNhans.FindAsync(id);
        if (p==null) return NotFound();
        p.PhongGiamId = toPhongId;
        await _db.SaveChangesAsync();
        return Ok(p);
    }
}
