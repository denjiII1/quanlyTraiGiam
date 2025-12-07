using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrisonApi.Data;
using PrisonApi.Models;

namespace PrisonApi.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly PrisonContext _db;
    public UsersController(PrisonContext db) { _db = db; }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _db.TaiKhoans.Include(t=>t.Quyen).Include(t=>t.HoSo).ToListAsync());

    [HttpGet("{username}")]
    public async Task<IActionResult> Get(string username)
    {
        var u = await _db.TaiKhoans.Include(t=>t.Quyen).Include(t=>t.HoSo).FirstOrDefaultAsync(t=>t.Ten==username);
        if (u == null) return NotFound();
        return Ok(u);
    }

    [HttpDelete("{username}")]
    public async Task<IActionResult> Delete(string username)
    {
        var u = await _db.TaiKhoans.FindAsync(username);
        if (u == null) return NotFound();
        u.IsActive = false; // soft-delete
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
