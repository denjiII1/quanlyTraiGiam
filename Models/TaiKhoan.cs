using System.ComponentModel.DataAnnotations;

namespace PrisonApi.Models;
public class TaiKhoan
{
    [Key]
    [MaxLength(50)]
    public string Ten { get; set; }          // username
    public string MatKhauHash { get; set; }
    public int QuyenId { get; set; }
    public Quyen Quyen { get; set; }
    public int? HoSoId { get; set; }
    public HoSo HoSo { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
