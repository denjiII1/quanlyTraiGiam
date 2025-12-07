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
public class CanBoController : ControllerBase
{
    private readonly PrisonContext _db;
    public CanBoController(PrisonContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _db.CanBos.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var cb = await _db.CanBos.FindAsync(id);
        if (cb == null) return NotFound();

        return Ok(cb);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,QuanLy")]
    public async Task<IActionResult> Create([FromBody] CanBoCreateDto dto)
    {
        var cb = new CanBo
        {
            HoTen = dto.HoTen,
            ChucVu = dto.ChucVu,
            PhongPhuTrach = dto.PhongPhuTrach,
            SDT = dto.SDT
        };

        _db.CanBos.Add(cb);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = cb.Id }, cb);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,QuanLy")]
    public async Task<IActionResult> Update(int id, [FromBody] CanBoUpdateDto dto)
    {
        var cb = await _db.CanBos.FindAsync(id);
        if (cb == null) return NotFound();

        cb.HoTen = dto.HoTen;
        cb.ChucVu = dto.ChucVu;
        cb.PhongPhuTrach = dto.PhongPhuTrach;
        cb.SDT = dto.SDT;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var cb = await _db.CanBos.FindAsync(id);
        if (cb == null) return NotFound();

        _db.CanBos.Remove(cb);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
