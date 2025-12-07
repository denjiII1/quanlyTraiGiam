namespace PrisonApi.DTOs;
public class RegisterDto { public string Username { get; set; } public string Password { get; set; } public int QuyenId { get; set; } public int? HoSoId { get; set; } }
public class LoginDto { public string Username { get; set; } public string Password { get; set; } public string IpAddress { get; set; } }
public class ChangePasswordDto { public string OldPassword { get; set; } public string NewPassword { get; set; } }
