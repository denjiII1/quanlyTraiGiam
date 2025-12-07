namespace PrisonApi.DTOs;

public class PhamNhanCreateDto
{
    public string HoTen { get; set; }
    public DateTime? NgaySinh { get; set; }
    public bool GioiTinh { get; set; }      // true = Nam
    public string CCCD { get; set; }
    public string TomTatToiPham { get; set; }
    public int MucAn { get; set; }          // số năm
    public DateTime? NgayVaoTrai { get; set; }
    public int? PhongGiamId { get; set; }
    public string TrangThai { get; set; }
}

public class PhamNhanUpdateDto
{
    public string HoTen { get; set; }
    public DateTime? NgaySinh { get; set; }
    public bool GioiTinh { get; set; }
    public string CCCD { get; set; }
    public string TomTatToiPham { get; set; }
    public int MucAn { get; set; }
    public DateTime? NgayVaoTrai { get; set; }
    public int? PhongGiamId { get; set; }
    public string TrangThai { get; set; }
}

public class PhamNhanDetailDto
{
    public int Id { get; set; }
    public string HoTen { get; set; }
    public DateTime? NgaySinh { get; set; }
    public bool GioiTinh { get; set; }
    public string CCCD { get; set; }
    public string TomTatToiPham { get; set; }
    public int MucAn { get; set; }
    public DateTime? NgayVaoTrai { get; set; }
    public string TrangThai { get; set; }

    // Thông tin phòng giam
    public int? PhongGiamId { get; set; }
    public string TenPhongGiam { get; set; }
    public string KhuGiam { get; set; }
}

public class PhamNhanListDto
{
    public int Id { get; set; }
    public string HoTen { get; set; }
    public string CCCD { get; set; }
    public int MucAn { get; set; }
    public string TenPhongGiam { get; set; }
    public string TrangThai { get; set; }
}
