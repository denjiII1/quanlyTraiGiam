using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrisonApi.Data;
using PrisonApi.Models;

namespace PrisonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LichSinhHoatController : ControllerBase
{
    private readonly PrisonContext _db;
    public LichSinhHoatController(PrisonContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _db.LichSinhHoats.Include(l => l.PhamNhan).ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var l = await _db.LichSinhHoats.Include(x => x.PhamNhan).FirstOrDefaultAsync(x => x.Id == id);
        if (l == null) return NotFound();

        return Ok(l);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,QuanLy")]
    public async Task<IActionResult> Create([FromBody] LichSinhHoat dto)
    {
        var l = new LichSinhHoat
        {
            PhamNhanId = dto.PhamNhanId,
            Ngay = dto.Ngay,
            HoatDong = dto.HoatDong,
            GhiChu = dto.GhiChu
        };

        _db.LichSinhHoats.Add(l);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = l.Id }, l);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,QuanLy")]
    public async Task<IActionResult> Update(int id, [FromBody] LichSinhHoat dto)
    {
        var l = await _db.LichSinhHoats.FindAsync(id);
        if (l == null) return NotFound();

        l.Ngay = dto.Ngay;
        l.HoatDong = dto.HoatDong;
        l.GhiChu = dto.GhiChu;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var l = await _db.LichSinhHoats.FindAsync(id);
        if (l == null) return NotFound();

        _db.LichSinhHoats.Remove(l);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
