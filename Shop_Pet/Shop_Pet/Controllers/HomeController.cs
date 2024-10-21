using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Pet.Models;
using Shop_Pet.Models.DataModels;
using System.Diagnostics;

namespace Shop_Pet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ShopPetDbContext _context;  

        public HomeController(ILogger<HomeController> logger,ShopPetDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Pet> pets = new List<Pet>();
            pets = _context.Pets.ToList();

            ViewBag.Pets = pets;
            // Kiểm tra trạng thái đăng nhập
            var isLoggedIn = HttpContext.Session.GetString("AccountId") != null;

            // Truyền thông tin đăng nhập vào View thông qua ViewData
            ViewData["IsLoggedIn"] = isLoggedIn;

            return View();
        }
        public IActionResult ThuCung(string Species = null)
        {
            // Retrieve all pets
            var pets = _context.Pets.Include(p => p.CategoryPet).AsQueryable();

            // If a species is provided, filter the pets by the corresponding CategoryPet species
            if (!string.IsNullOrEmpty(Species))
            {
                pets = pets.Where(p => p.CategoryPet.species == Species);
            }

            // Pass filtered pets to the view
            ViewBag.Pets = pets.ToList();

            return View();
        }

        public IActionResult GioiThieu()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public List<Cart> carts
        {

            get
            {
                var data = HttpContext.Session.Get<List<Cart>>("GioHang");
                return data ?? new List<Cart>(); // Simplified
            }
        }
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Login(string username, string password, string returnUrl = null)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.UserName == username);
            var passmd5 = Cipher.GenerateMD5(password);

            if (account == null || !account.Password.Equals(passmd5))
            {
                ViewBag.error = "<div class='alert alert-danger'>Đăng nhập sai</div>";
                return View();
            }

            // Đăng nhập thành công: Lưu thông tin vào session
            HttpContext.Session.SetString("AccountId", account.AccountId);
            HttpContext.Session.SetString("fullname", account.FullName);
            HttpContext.Session.SetString("picture", account.Picture);
            HttpContext.Session.SetString("username", account.UserName);
            HttpContext.Session.SetString("email", account.Email);
            HttpContext.Session.SetString("phone", account.PhoneNumber);

            // Điều hướng người dùng quay lại trang trước đó nếu có ReturnUrl
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Cart");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string username,string fullname,string email,string phone, string password)
        {
            // Su ly du lieu nguoi mua
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
            {
                ViewBag.error = "Vui long nhap thong tin";
                return View();
            }

            Account newaccount = new Account()
            {
                AccountId = Guid.NewGuid().ToString(),
                UserName = username,
                FullName = fullname,
                Email = email,
                PhoneNumber = phone,
                Password = Cipher.GenerateMD5(password),
                IsActive = true,
                IsAdmin = false
               
            };
            _context.Accounts.Add(newaccount);
            _context.SaveChanges();
            return RedirectToAction("Login","Home");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}