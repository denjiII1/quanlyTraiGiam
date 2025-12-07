namespace PrisonApi.DTOs;

public class CanBoCreateDto
{
    public string HoTen { get; set; }
    public string ChucVu { get; set; }
    public string PhongPhuTrach { get; set; }
    public string SDT { get; set; }
}

public class CanBoUpdateDto
{
    public string HoTen { get; set; }
    public string ChucVu { get; set; }
    public string PhongPhuTrach { get; set; }
    public string SDT { get; set; }
}

public class CanBoDetailDto
{
    public int Id { get; set; }
    public string HoTen { get; set; }
    public string ChucVu { get; set; }
    public string PhongPhuTrach { get; set; }
    public string SDT { get; set; }
}

public class CanBoListDto
{
    public int Id { get; set; }
    public string HoTen { get; set; }
    public string ChucVu { get; set; }
}
