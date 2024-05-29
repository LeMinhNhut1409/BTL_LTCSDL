using System;
using System.Collections.Generic;

namespace QuanLyKhachSan22.Models;

public partial class Loaiphong
{
    public int IdLp { get; set; }

    public string TenLp { get; set; } = null!;

    public string MoTaLp { get; set; } = null!;

    public virtual ICollection<Phong> Phongs { get; set; } = new List<Phong>();
}
