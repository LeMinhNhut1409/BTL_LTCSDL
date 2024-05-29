using System;
using System.Collections.Generic;

namespace QuanLyKhachSan22.Models;

public partial class Thongke
{
    public int IdTk { get; set; }

    public int? IdPdp { get; set; }

    public decimal ThanhTien { get; set; }

    public DateOnly NgayThanhToan { get; set; }

    public virtual Phieudatphong? IdPdpNavigation { get; set; }
}
