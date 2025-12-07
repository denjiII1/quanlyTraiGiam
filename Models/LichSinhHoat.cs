namespace PrisonApi.Models;
public class LichSinhHoat
{
    public int Id { get; set; }
    public int PhamNhanId { get; set; }
    public PhamNhan PhamNhan { get; set; }
    public DateTime Ngay { get; set; }
    public string HoatDong { get; set; }
    public string GhiChu { get; set; }
}
