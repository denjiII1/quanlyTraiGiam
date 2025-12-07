namespace PrisonApi.Models;
public class PhongGiam
{
    public int Id { get; set; }
    public string TenPhong { get; set; }
    public string LoaiPhong { get; set; }    // Biệt giam, thường, y tế...
    public int SucChua { get; set; }
    public string KhuGiam { get; set; }
    public ICollection<PhamNhan> PhamNhans { get; set; }
}

