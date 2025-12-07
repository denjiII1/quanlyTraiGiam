namespace PrisonApi.DTOs;

public class PhongGiamCreateDto
{
    public string TenPhong { get; set; }
    public string LoaiPhong { get; set; }
    public int SucChua { get; set; }
    public string KhuGiam { get; set; }
}

public class PhongGiamUpdateDto
{
    public string TenPhong { get; set; }
    public string LoaiPhong { get; set; }
    public int SucChua { get; set; }
    public string KhuGiam { get; set; }
}

public class PhongGiamDetailDto
{
    public int Id { get; set; }
    public string TenPhong { get; set; }
    public string LoaiPhong { get; set; }
    public int SucChua { get; set; }
    public string KhuGiám { get; set; }
    public int SoPhamNhan { get; set; }
}

public class PhongGiamListDto
{
    public int Id { get; set; }
    public string TenPhong { get; set; }
    public int SoPhamNhan { get; set; }
}

