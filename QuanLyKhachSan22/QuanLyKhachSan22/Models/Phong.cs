using System;
using System.Collections.Generic;

namespace QuanLyKhachSan22.Models;

public partial class Phong
{
    public int IdP { get; set; }

    public string TenP { get; set; } = null!;

    public int? IdLp { get; set; }

    public int? GiaP { get; set; }

    public string? HinhAnhPhong { get; set; }

    public string? TinhTrang { get; set; }

    public virtual Loaiphong? IdLpNavigation { get; set; }

    public virtual ICollection<Phieudatphong> Phieudatphongs { get; set; } = new List<Phieudatphong>();
}
