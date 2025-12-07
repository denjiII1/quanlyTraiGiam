namespace PrisonApi.Models;
public class KhenThuong
{
    public int Id { get; set; }
    public int PhamNhanId { get; set; }
    public PhamNhan PhamNhan { get; set; }
    public DateTime NgayKhenThuong { get; set; } = DateTime.UtcNow;
    public string LyDo { get; set; }
    public string MoTa { get; set; }
}
