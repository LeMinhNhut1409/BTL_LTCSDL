using System;
using System.Collections.Generic;

namespace QuanLyKhachSan22.Models;

public partial class Nhanvien
{
    public int IdNv { get; set; }

    public string HoTenNv { get; set; } = null!;

    public DateOnly NgaySinhNv { get; set; }

    public string Username { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string SdtNv { get; set; } = null!;

    public virtual ICollection<Phieudatphong> Phieudatphongs { get; set; } = new List<Phieudatphong>();
}
