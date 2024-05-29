using System;
using System.Collections.Generic;

namespace QuanLyKhachSan22.Models;

public partial class Phieudatphong
{
    public int IdPdp { get; set; }

    public int? IdKh { get; set; }

    public int? IdP { get; set; }

    public int? IdNv { get; set; }

    public DateOnly NgayDatPhong { get; set; }

    public DateOnly NgayTraPhong { get; set; }

    public decimal TongTien { get; set; }

    public virtual Khachhang? IdKhNavigation { get; set; }

    public virtual Nhanvien? IdNvNavigation { get; set; }

    public virtual Phong? IdPNavigation { get; set; }

    public virtual ICollection<Thongke> Thongkes { get; set; } = new List<Thongke>();
}
