namespace PrisonApi.Models;
public class KyLuat
{
    public int Id { get; set; }
    public int PhamNhanId { get; set; }
    public PhamNhan PhamNhan { get; set; }
    public DateTime NgayKyLuat { get; set; } = DateTime.UtcNow;
    public string HinhThuc { get; set; }
    public string MoTa { get; set; }
}
