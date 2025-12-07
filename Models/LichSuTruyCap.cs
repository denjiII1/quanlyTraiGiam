namespace PrisonApi.Models;
public class LichSuTruyCap
{
    public int Id { get; set; }
    public string TaiKhoanTen { get; set; }
    public TaiKhoan TaiKhoan { get; set; }
    public DateTime ThoiGian { get; set; } = DateTime.UtcNow;
    public string DiaChiIP { get; set; }
    public string MoTa { get; set; }
}
