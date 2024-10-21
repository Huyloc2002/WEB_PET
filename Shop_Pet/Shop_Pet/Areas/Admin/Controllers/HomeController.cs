using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_Pet.Models;
using Shop_Pet.Models.DataModels;
using System.Security.Claims;

namespace Shop_Pet.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class HomeController : Controller
    {

        ShopPetDbContext _context;

        public HomeController(ShopPetDbContext context)
        {
            this._context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("ShopPet");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Login( string username,string password,string? requestUrl)
        {
            string passmd5 = Cipher.GenerateMD5(password);
            var account = _context.Accounts.SingleOrDefault(x => x.UserName == username && x.Password == passmd5 && x.IsAdmin && x.IsActive);
            if (account == null)
            {
                ViewBag.error = "<div class='alert alert-danger'>Tên đăng nhập hoặc mật khẩu sai, hoặc bạn không có quyền</div>";
                return View();
            }
            else
            {
                var identity = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim("accountid",account.AccountId),
                        new Claim("fullname",account.FullName),
                        new Claim("picture",account.Picture),
                    }, "ShopPet"
                    );
                var pricipal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync("ShopPet", pricipal);
                if (requestUrl != null)
                    return Redirect(requestUrl);
                else
                    return RedirectToAction("Index");
            }
        }

    }
}
