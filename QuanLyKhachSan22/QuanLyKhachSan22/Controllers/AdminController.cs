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
        public IActionResult QLPhong(bool? tinhTrangPhong, decimal? to, decimal? from, int? IdLp)
        {
            var timKiem = db.Phongs.AsQueryable();
            if (tinhTrangPhong.HasValue)
            {

                timKiem = timKiem.Where(r => r.TinhTrang == tinhTrangPhong.Value);
            }
            if (to.HasValue)
            {
                timKiem = timKiem.Where(r => r.GiaP >= to.Value);
            }
            if (from.HasValue)
            {
                timKiem = timKiem.Where(r => r.GiaP <= from.Value);
            }
            if (IdLp.HasValue)
            {
                timKiem = timKiem.Where(r => r.IdLp == IdLp.Value);
            }
            return View(timKiem.ToList());

        }
        //checkout về 0
        [Route("checkout")]
        [HttpGet]
        public IActionResult Checkout(int id)
        {
            var phong = db.Phongs.Find(id);
            if (phong == null)
            {
                return NotFound();
            }

            if (phong.TinhTrang)
            {
                phong.TinhTrang = false;
                db.Update(phong);
                db.SaveChanges();
                TempData["SuccessMessage"] = $"Room {phong.IdP} has been checked out.";
            }
            else
            {
                TempData["ErrorMessage"] = $"Room {phong.IdP} is already available.";
            }

            return RedirectToAction(nameof(QLPhong));
        }
        //Them phong
        [Route("themphong")]
        public IActionResult ThemPhong()
        {
            ViewBag.IdLp = new SelectList(db.Loaiphongs.ToList(), "IdLp", "TenLp");
            return View();
        }
        [Route("themphong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemPhong(Phong phong)
        {
            if (ModelState.IsValid)
            {
                db.Phongs.Add(phong);
                db.SaveChanges();
                return RedirectToAction("QLPhong");
            }
            return View(phong);
        }

        //Sua phong
        [Route("suaphong")]
        public IActionResult SuaPhong(int IdP)
        {
            ViewBag.IdLp = new SelectList(db.Loaiphongs.ToList(), "IdLp", "TenLp");
            var phong = db.Phongs.Find(IdP);
            return View(phong);
        }
        [Route("suaphong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaPhong(Phong phong)
        {
            if (ModelState.IsValid)
            {
                db.Update(phong);
                db.SaveChanges();
                return RedirectToAction("QLPhong");
            }
            return View(phong);
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
        public IActionResult QLLoaiphong()
        {
            var lstLoaiPhong = db.Loaiphongs.ToList();
            return View(lstLoaiPhong);

        }

        //thêm loại phòng
        [Route("ThemLP")]
        public IActionResult ThemLP()
        {
            //ViewBag.IdLp = new SelectList(db.Loaiphongs.ToList(), "IdLp", "TenLp");
            return View();
        }
        [Route("ThemLP")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemLoaiPhongMoi(Loaiphong loaiphong)
        {
            if (ModelState.IsValid)
            {

                db.Loaiphongs.Add(loaiphong);
                db.SaveChanges();
                return RedirectToAction("QLLoaiphong");
            }
            return View(loaiphong);
        }

        //Xoa loai phong
        [Route("XoaLP")]
        [HttpGet]
        public IActionResult XoaLP(int IdLp)
        {
            db.Remove(db.Loaiphongs.Find(IdLp));
            db.SaveChanges();
            TempData["Message"] = "Loại phòng đã được xóa!!!";
            return RedirectToAction("QLLoaiphong");

        }

        //sửa loại phòng
        [Route("SuaLP")]
        [HttpGet]
        public IActionResult SuaLP(int IdLp)
        {
            //ViewBag.IdLp = new SelectList(db.Loaiphongs.ToList(), "IdLp", "TenLp");
            var loaiphong = db.Loaiphongs.Find(IdLp);
            return View(loaiphong);
        }
        [Route("SuaLP")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaLoaiPhong(Loaiphong loaiphong)
        {
            if (ModelState.IsValid)
            {
                db.Update(loaiphong);
                db.SaveChanges();
                return RedirectToAction("QLLoaiphong");
            }
            return View(loaiphong);
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
                db.SaveChanges();


                // Tạo bản ghi thống kê


                var thongKe = new Thongke
                {
                    IdTk = db.Thongkes.Count() + 1,
                    IdPdp = pdp.IdPdp,
                    ThanhTien = pdp.TongTien,
                    NgayThanhToan = pdp.NgayTraPhong
                };
                db.Thongkes.Add(thongKe);
                db.SaveChanges();



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

