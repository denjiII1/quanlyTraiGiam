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
public class KhenThuongController : ControllerBase
{
    private readonly PrisonContext _db;
    public KhenThuongController(PrisonContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _db.KhenThuongs.Include(k => k.PhamNhan).ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var k = await _db.KhenThuongs.Include(x => x.PhamNhan).FirstOrDefaultAsync(x => x.Id == id);
        if (k == null) return NotFound();

        return Ok(k);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,QuanLy")]
    public async Task<IActionResult> Create([FromBody] KhenThuongCreateDto dto)
    {
        var k = new KhenThuong
        {
            PhamNhanId = dto.PhamNhanId,
            NgayKhenThuong = dto.NgayKhenThuong ?? DateTime.UtcNow,
            LyDo = dto.LyDo,
            MoTa = dto.MoTa
        };

        _db.KhenThuongs.Add(k);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = k.Id }, k);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,QuanLy")]
    public async Task<IActionResult> Update(int id, [FromBody] KhenThuongUpdateDto dto)
    {
        var k = await _db.KhenThuongs.FindAsync(id);
        if (k == null) return NotFound();

        k.NgayKhenThuong = dto.NgayKhenThuong ?? k.NgayKhenThuong;
        k.LyDo = dto.LyDo;
        k.MoTa = dto.MoTa;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var k = await _db.KhenThuongs.FindAsync(id);
        if (k == null) return NotFound();

        _db.KhenThuongs.Remove(k);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
