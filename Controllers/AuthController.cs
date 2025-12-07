using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrisonApi.DTOs;
using PrisonApi.Services;

namespace PrisonApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;
    public AuthController(AuthService auth) { _auth = auth; }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var r = await _auth.RegisterAsync(dto);
        if (r == null) return BadRequest(new { message = "Username already exists" });
        return CreatedAtAction(nameof(Register), new { username = r.Ten }, r);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        dto.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? dto.IpAddress;
        var token = await _auth.LoginAsync(dto);
        if (token == null) return Unauthorized(new { message = "Invalid credentials" });
        return Ok(new { token });
    }

    [HttpPost("changepassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        var username = User.Identity.Name;
        if (!await _auth.ChangePasswordAsync(username, dto)) return BadRequest(new { message = "Change password failed" });
        return Ok(new { message = "Password changed" });
    }
}
