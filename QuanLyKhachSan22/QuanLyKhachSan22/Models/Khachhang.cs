using System;
using System.Collections.Generic;

namespace QuanLyKhachSan22.Models;

public partial class Khachhang
{
    public int IdKh { get; set; }

    public string HoTenKh { get; set; } = null!;

    public DateOnly NgaySinhKh { get; set; }

    public string SdtKh { get; set; } = null!;

    public string EmailKh { get; set; } = null!;

    public string DiaChiKh { get; set; } = null!;

    public virtual ICollection<Phieudatphong> Phieudatphongs { get; set; } = new List<Phieudatphong>();
}
