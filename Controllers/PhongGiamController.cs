using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrisonApi.Data;
using PrisonApi.Models;

namespace PrisonApi.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PhongGiamController : ControllerBase
{
    private readonly PrisonContext _db;
    public PhongGiamController(PrisonContext db) { _db = db; }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _db.PhongGiams.Include(p=>p.PhamNhans).ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
        var p = await _db.PhongGiams.Include(x=>x.PhamNhans).FirstOrDefaultAsync(x=>x.Id==id);
        if (p==null) return NotFound();
        return Ok(p);
    }

    [HttpPost]
    [Authorize(Roles="Admin,QuanLy")]
    public async Task<IActionResult> Create([FromBody] PhongGiam model){
        _db.PhongGiams.Add(model);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    [Authorize(Roles="Admin,QuanLy")]
    public async Task<IActionResult> Update(int id, [FromBody] PhongGiam update){
        var p = await _db.PhongGiams.FindAsync(id);
        if (p==null) return NotFound();
        p.TenPhong = update.TenPhong;
        p.LoaiPhong = update.LoaiPhong;
        p.SucChua = update.SucChua;
        p.KhuGiam = update.KhuGiam;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Delete(int id){
        var p = await _db.PhongGiams.FindAsync(id);
        if (p==null) return NotFound();
        _db.PhongGiams.Remove(p);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("stats")]
    public async Task<IActionResult> Stats(){
        var data = await _db.PhongGiams
            .Select(pg => new {
                pg.Id, pg.TenPhong, pg.SucChua, SoPhamNhan = pg.PhamNhans.Count()
            }).ToListAsync();
        return Ok(data);
    }
}
