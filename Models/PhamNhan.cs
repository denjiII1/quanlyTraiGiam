namespace PrisonApi.Models;
public class PhamNhan
{
    public int Id { get; set; }
    public string HoTen { get; set; }
    public DateTime? NgaySinh { get; set; }
    public bool GioiTinh { get; set; }      // true: Nam
    public string CCCD { get; set; }
    public string TomTatToiPham { get; set; }
    public int MucAn { get; set; }          // số năm
    public DateTime? NgayVaoTrai { get; set; }
    public int? PhongGiamId { get; set; }
    public PhongGiam PhongGiam { get; set; }
    public string TrangThai { get; set; }
    public ICollection<KyLuat> KyLuats { get; set; }
    public ICollection<KhenThuong> KhenThuongs { get; set; }
}
