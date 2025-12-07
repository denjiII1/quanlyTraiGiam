namespace PrisonApi.DTOs;

public class KhenThuongCreateDto
{
    public int PhamNhanId { get; set; }
    public DateTime? NgayKhenThuong { get; set; } = DateTime.UtcNow;
    public string LyDo { get; set; }
    public string MoTa { get; set; }
}

public class KhenThuongUpdateDto
{
    public DateTime? NgayKhenThuong { get; set; }
    public string LyDo { get; set; }
    public string MoTa { get; set; }
}

public class KhenThuongDetailDto
{
    public int Id { get; set; }
    public int PhamNhanId { get; set; }
    public string TenPhamNhan { get; set; }
    public DateTime NgayKhenThuong { get; set; }
    public string LyDo { get; set; }
    public string MoTa { get; set; }
}

public class KhenThuongListDto
{
    public int Id { get; set; }
    public string LyDo { get; set; }
    public DateTime NgayKhenThuong { get; set; }
}
