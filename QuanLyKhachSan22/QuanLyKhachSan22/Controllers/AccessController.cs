using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using QuanLyKhachSan22.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace QuanLyKhachSan22.Controllers
{
    public class AccessController : Controller
    {
        Qlks1Context db = new Qlks1Context();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Thực hiện kiểm tra thông tin người dùng
                if (KTUser(model.Username, model.Password)) // Hàm kiểm tra thông tin người dùng
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username)
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công.");
                }
            }

            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Access");
        }
        private bool KTUser(string Username, string Password)
        {

            bool isValid = false;
            Nhanvien User = db.Nhanviens.FirstOrDefault(u => u.Username == Username && u.Pass == Password);
            if (User != null)
            {
                HttpContext.Session.SetString("Username", User.Username);
                HttpContext.Session.SetString("Password", User.Pass);
                isValid = true;

            }

            return isValid;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem người dùng đã tồn tại hay chưa
                var existingUser = await db.Nhanviens
                    .FirstOrDefaultAsync(u => u.Username == model.Username);

                if (existingUser != null)
                {
                    ModelState.AddModelError("", "User name or email already exists.");
                    return View(model);
                }

                // Mã hóa mật khẩu
               

                var user = new Nhanvien     
                {                                       
                                                                                                          
                    HoTenNv = model.FullName,     
                    Username = model.Username,
                    Pass = model.Password,
                    SdtNv=model.PhoneNumber,
                    NgaySinhNv = model.DateOfBirth,
                };                                                                                                              

                db.Nhanviens.Add(user);
                await db.SaveChangesAsync();

                // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
                return RedirectToAction("Login", "Access");
            }

            return View(model);
        }

        
    }
}
