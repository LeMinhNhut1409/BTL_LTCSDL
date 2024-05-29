using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuanLyKhachSan22.Models;

namespace QuanLyKhachSan22.Controllers
{
    public class AdminController : Controller
    {
        Qlks1Context db = new Qlks1Context();
        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(userRole))
            {
                return RedirectToAction("Login", "Access");
            }
            return View();
        }
        public async Task<IActionResult> ListLP()
        {
            var userRole = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(userRole))
            {
                return RedirectToAction("Login", "Access");
            }
            return View(await db.Loaiphongs.ToListAsync());
        }
        // GET: LoaiPhong/Create
        public IActionResult Create()
        {
            var userRole = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(userRole))
            {
                return RedirectToAction("Login", "Access");
            }
            return View();
        }

        // POST: LoaiPhong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLp,TenLp,MoTaLp")] Loaiphong loaiPhong)
        {
            if (ModelState.IsValid)
            {
                db.Add(loaiPhong);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiPhong);
        }

        [HttpGet]
        [Route("QLPhong")]
        public IActionResult QLPhong(string searchInput)
        {
            var lstphong = db.Phongs.ToList();

            // Kiểm tra dữ liệu
            if (!string.IsNullOrEmpty(searchInput))
            {
                //kiếm tra dữ liệu có phải giá không?
                if (int.TryParse(searchInput, out int gia))
                {
                    lstphong = lstphong.Where(x => x.GiaP == gia).ToList();
                }
                else
                {
                    lstphong = lstphong.Where(x => x.TinhTrang.Contains(searchInput)).ToList();
                }
            }

            return View(lstphong);

        }
        //Them Phong
        [Route("ThemPhong")]
        public ActionResult ThemPhong()
        {
            return View();
        }
        [HttpPost]

        public ActionResult ThemPhong(Phong P)
        {
            db.Phongs.Add(P);
            db.SaveChangesAsync();
            return RedirectToAction("QlPhong");
        }

        // Sửa phòng

        [Route("SuaPhong")]
        [HttpGet]
        public ActionResult SuaPhong(int id)
        {
            Phong p = db.Phongs.FirstOrDefault(x => x.IdP == id);
            if (p == null)
            {
                return NotFound();
            }

            return View(p);
        }
        [Route("SuaPhong")]
        [HttpPost]

        public ActionResult SuaPhong(Phong p)
        {
            Phong up = db.Phongs.FirstOrDefault(x => x.IdP == p.IdP);
            if (up != null)
            {
                up.TenP = p.TenP;
                up.GiaP = p.GiaP;
                up.TinhTrang = p.TinhTrang;
                db.SaveChanges();
            }

            return RedirectToAction("QlPhong");
        }

        //Xoa phong
        [Route("XoaPhong")]
        [HttpGet]
        public IActionResult XoaPhong(int IdP)
        {
            db.Remove(db.Phongs.Find(IdP));
            db.SaveChanges();
            TempData["Message"] = "phòng đã được xóa!!!";
            return RedirectToAction("QLPhong");

        }

        //Quản Lý loại phòng
        [HttpGet]
        [Route("QLloaiphong")]
        public IActionResult QLloaiphong()
        {
            var lstlp = db.Loaiphongs.ToList();
            return View(lstlp);
        }

        //thêm loại phòng
        [Route("ThemLP")]
        public ActionResult ThemLP(Loaiphong lp)
        {
            if (ModelState.IsValid)
            {
                db.Loaiphongs.Add(lp);
                db.SaveChangesAsync();
                return RedirectToAction("QLloaiphong");
            }
            return View(lp);
        }

        //Xoa loai phong
        [Route("XoaLP")]
        [HttpGet]
        public IActionResult XoaLP(int IdLp)
        {
            db.Remove(db.Loaiphongs.Find(IdLp));
            db.SaveChangesAsync();
            TempData["Message"] = "Loại phòng đã được xóa!!!";
            return RedirectToAction("QLloaiphong");

        }

        //sửa loại phòng
        [Route("SuaLP")]
        [HttpGet]
        public ActionResult SuaLP(int id)
        {
            Loaiphong lp = db.Loaiphongs.FirstOrDefault(x => x.IdLp == id);
            if (lp == null)
            {
                return NotFound();
            }

            return View(lp);
        }

        [Route("SuaLP")]
        [HttpPost]

        public ActionResult SuaLP(Loaiphong editLP)
        {
            Loaiphong up = db.Loaiphongs.FirstOrDefault(x => x.IdLp == editLP.IdLp);
            if (up != null)
            {
                up.TenLp = editLP.TenLp;
                up.MoTaLp = editLP.MoTaLp;
                db.SaveChangesAsync();
            }

            return RedirectToAction("QLloaiphong");
        }

        //QLNhanVien
        [HttpGet]
        [Route("QLNhanVien")]
        public ActionResult QLNhanVien(Nhanvien nv)
        {
            var lstnv = db.Nhanviens.ToList();
            return View(lstnv);

        }
        [HttpGet]
        [Route("Xoanv")]
        public IActionResult Xoanv(int IdNv)
        {
            db.Remove(db.Nhanviens.Find(IdNv));
            db.SaveChanges();
            TempData["Message"] = "Xóa thành công!!!";
            return RedirectToAction("QLNhanVien");

        }

        //QLKHACHHANG

        [HttpGet]
        [Route("QLKhachHang")]
        public IActionResult QLKhachHang()
        {
            var lstkh = db.Khachhangs.ToList();
            return View(lstkh);

        }
        [Route("ThemKH")]
        public ActionResult ThemKH(Khachhang kh)
        {
            if (ModelState.IsValid)
            {
                db.Khachhangs.Add(kh);
                db.SaveChangesAsync();
                return RedirectToAction("QLKhachHang");
            }
            return View(kh);
        }
        //sửa KH
        [Route("SuaKH")]
        [HttpGet]
        public ActionResult SuaKH(int id)
        {
            Khachhang kh = db.Khachhangs.FirstOrDefault(x => x.IdKh == id);
            if (kh == null)
            {
                return NotFound();
            }

            return View(kh);
        }
        [Route("SuaKH")]
        [HttpPost]

        public ActionResult SuaKH(Khachhang p)
        {
            Khachhang up = db.Khachhangs.FirstOrDefault(x => x.IdKh == p.IdKh);
            if (up != null)
            {
                up.HoTenKh = p.HoTenKh;
                up.NgaySinhKh = p.NgaySinhKh;
                up.EmailKh = p.EmailKh;
                up.SdtKh = p.SdtKh;
                up.DiaChiKh = p.DiaChiKh;
                db.SaveChanges();
            }

            return RedirectToAction("QLKhachHang");
        }
        //Xoa loai phong
        [Route("XoaKH")]
        [HttpGet]
        public IActionResult XoaKH(int IdKH)
        {
            db.Remove(db.Khachhangs.Find(IdKH));
            db.SaveChangesAsync();
            TempData["Message"] = "khách hàng đã được xóa!!!";
            return RedirectToAction("QLKhachHang");

        }

        [Route("danhsachthuephong")]
        public IActionResult danhsachthuephong()
        {
            var lstdatphong = db.Phieudatphongs.ToList();
            return View(lstdatphong);
        }

        [Route("ThemPhieuThuePhong")]
        public IActionResult ThemPhieuThuePhong()
        {
            // Lấy danh sách khách hàng, nhân viên và phòng để hiển thị trong dropdown
            ViewBag.IdKh = new SelectList(db.Khachhangs.ToList(), "IdKh", "HoTenKh");
            ViewBag.IdNv = new SelectList(db.Nhanviens.ToList(), "IdNv", "HoTenNv");
            ViewBag.IdP = new SelectList(db.Phongs.ToList(), "IdP", "TenP");

            // Trả về view để hiển thị form nhập liệu
            return View();
        }

        [Route("ThemPhieuThuePhong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemPhieuThuePhong(Phieudatphong pdp, Thongke tk)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem có bản ghi nào với ID_PDP giống như bản ghi mới không
                var existingBooking = db.Phieudatphongs.FirstOrDefault(b => b.IdPdp == pdp.IdPdp);
                if (existingBooking != null)
                {
                    // Nếu có, hiển thị lỗi
                    ModelState.AddModelError("ID_PDP", "Mã phiếu đặt phòng đã tồn tại.");
                    return View(pdp);
                }

                // Thêm bản ghi mới
                db.Phieudatphongs.Add(pdp);
                db.SaveChangesAsync();

                // Tạo bản ghi thống kê


                var thongKe = new Thongke
                {
                    IdTk = db.Thongkes.Count() + 1,
                    IdPdp = pdp.IdPdp,
                    ThanhTien = pdp.TongTien,
                    NgayThanhToan = pdp.NgayTraPhong
                };
                db.Thongkes.Add(thongKe);
                db.SaveChangesAsync();


                // Chuyển hướng đến trang danh sách thuê phòng
                return RedirectToAction("danhsachthuephong");
            }

            // Nếu dữ liệu nhập vào không hợp lệ, trả về view để hiển thị lại form
            return View(pdp);
        }

        //trang thong ke
        public IActionResult ThongKe()
        {   
           var lstThongke = db.Thongkes.ToList();
           return View(lstThongke);
        }

    }
}

