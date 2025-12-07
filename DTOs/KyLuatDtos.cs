namespace PrisonApi.DTOs;

public class KyLuatCreateDto
{
    public int PhamNhanId { get; set; }
    public DateTime? NgayKyLuat { get; set; } = DateTime.UtcNow;
    public string HinhThuc { get; set; }
    public string MoTa { get; set; }
}

public class KyLuatUpdateDto
{
    public DateTime? NgayKyLuat { get; set; }
    public string HinhThuc { get; set; }
    public string MoTa { get; set; }
}

public class KyLuatDetailDto
{
    public int Id { get; set; }
    public int PhamNhanId { get; set; }
    public string TenPhamNhan { get; set; }
    public DateTime NgayKyLuat { get; set; }
    public string HinhThuc { get; set; }
    public string MoTa { get; set; }
}

public class KyLuatListDto
{
    public int Id { get; set; }
    public string HinhThuc { get; set; }
    public DateTime NgayKyLuat { get; set; }
}
