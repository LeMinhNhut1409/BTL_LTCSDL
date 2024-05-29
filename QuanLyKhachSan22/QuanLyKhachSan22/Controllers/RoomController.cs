using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using QuanLyKhachSan22.Models;
using QuanLyKhachSan22.ViewModels;
using X.PagedList;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuanLyKhachSan22.Controllers
{
    public class RoomController : Controller
    {
        private Qlks1Context db;
        public RoomController(Qlks1Context context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(int? Loai, int? page)
        {
            var phongs = db.Phongs.AsQueryable();
            if (Loai.HasValue)
            {
                phongs = phongs.Where(p => p.IdP == Loai.Value);
            }

            int pageSize = 6; // Số lượng phòng trên mỗi trang
            int pageNumber = (page ?? 1); // Số trang hiện tại (mặc định là trang 1 nếu không có giá trị)

            var result = await phongs.Select(p => new roomVM
            {
                MaPhong = p.IdP,
                TenPhong = p.TenP,
                Hinh = p.HinhAnhPhong ?? "",
                GiaPhong = p.GiaP ?? 0,
                TinhTrang = p.TinhTrang,
            }).ToPagedListAsync(pageNumber, pageSize);

            return View(result);
        }
    }
}
