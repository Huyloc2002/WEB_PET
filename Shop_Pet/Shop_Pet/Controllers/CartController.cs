using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Pet.Models.DataModels;

using Shop_Pet; // Replace with your actual namespace
using System.Collections.Generic;
using Shop_Pet.Models;
using System.Diagnostics;

namespace Shop_Pet.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopPetDbContext _context; // Use your actual DbContext
      

        public CartController(ShopPetDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Lấy dữ liệu giỏ hàng từ Session
            var carts = HttpContext.Session.Get<List<Cart>>((MySetting.CART_KEY)) ?? new List<Cart>();

            // Kiểm tra trạng thái đăng nhập
            var isLoggedIn = HttpContext.Session.GetString("AccountId") != null;

            // Truyền thông tin đăng nhập vào View thông qua ViewData
            ViewData["IsLoggedIn"] = isLoggedIn;

            return View(carts);
          
        }
        public List<Cart> carts
        {
      
             get
            {
                var data = HttpContext.Session.Get<List<Cart>>(MySetting.CART_KEY);
                return data ?? new List<Cart>(); // Simplified
            }
        }

        public IActionResult AddToCart(int  id)
        {
            var mycart = carts;
            var item = mycart.SingleOrDefault(p=>p.Id == id);
            if(item == null)
            {
                var pet = _context.Pets.SingleOrDefault(p=>p.pet_id ==  id);
                item = new Cart
                {
                    Id = id,
                    Tenpet = pet.name,
                    Price = pet.price,
                    Quantity = 1,
                    Images = pet.Images


                };
                mycart.Add(item);
            }
            else
            {
                item.Quantity++;
            }
            HttpContext.Session.Set(MySetting.CART_KEY, mycart);
           
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var mycart = carts;
            var item = mycart.SingleOrDefault(p => p.Id == id);

            if (item != null)
            {
                mycart.Remove(item);
            }

            HttpContext.Session.Set(MySetting.CART_KEY, mycart);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult PlaceOrder(string username, string email, string phone, string password)
        {
            var accountId = HttpContext.Session.GetString("AccountId");
            if (string.IsNullOrEmpty(accountId))
            {
                TempData["Error"] = "Bạn Đã đăng nhập Thành Công khi đặt hàng.";
                return RedirectToAction("Login", "Cart", new { returnUrl = Url.Action("Index", "Order") });
            }

            // Kiểm tra dữ liệu nhập hợp lệ
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "All fields are required.";
                return RedirectToAction("Index");
            }

            var carts = HttpContext.Session.Get<List<Cart>>(MySetting.CART_KEY) ?? new List<Cart>();
            if (carts.Count == 0)
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            var newOrder = new Order
            {
                Account = new Account
                {
                    UserName = username,
                    FullName = username,
                    Picture = "",
                    IsActive = true
                },
                Carts = carts
            };

            // Xử lý lưu đơn hàng vào CSDL nếu cần
            //_context.Orders.Add(newOrder);
            //_context.SaveChanges();

            HttpContext.Session.Remove(MySetting.CART_KEY); // Xóa giỏ hàng sau khi đặt hàng thành công
            TempData["Success"] = "Mua Hàng Thành Công!";

            return RedirectToAction("Order");
        }

       /* public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
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

            // Điều hướng người dùng quay lại trang trước đó nếu có ReturnUrl
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Cart");
        }
*/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
    }
}
