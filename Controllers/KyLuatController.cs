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
public class KyLuatController : ControllerBase
{
    private readonly PrisonContext _db;
    public KyLuatController(PrisonContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _db.KyLuats.Include(k => k.PhamNhan).ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var k = await _db.KyLuats.Include(x => x.PhamNhan).FirstOrDefaultAsync(x => x.Id == id);
        if (k == null) return NotFound();

        return Ok(k);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,QuanLy")]
    public async Task<IActionResult> Create([FromBody] KyLuatCreateDto dto)
    {
        var k = new KyLuat
        {
            PhamNhanId = dto.PhamNhanId,
            NgayKyLuat = dto.NgayKyLuat ?? DateTime.UtcNow,
            HinhThuc = dto.HinhThuc,
            MoTa = dto.MoTa
        };

        _db.KyLuats.Add(k);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = k.Id }, k);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,QuanLy")]
    public async Task<IActionResult> Update(int id, [FromBody] KyLuatUpdateDto dto)
    {
        var k = await _db.KyLuats.FindAsync(id);
        if (k == null) return NotFound();

        k.NgayKyLuat = dto.NgayKyLuat ?? k.NgayKyLuat;
        k.HinhThuc = dto.HinhThuc;
        k.MoTa = dto.MoTa;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var k = await _db.KyLuats.FindAsync(id);
        if (k == null) return NotFound();

        _db.KyLuats.Remove(k);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
