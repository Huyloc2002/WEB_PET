using Microsoft.AspNetCore.Mvc;
using Shop_Pet.Models.DataModels;

namespace Shop_Pet.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShopPetDbContext _context; // Use your actual DbContext

        public AccountController(ShopPetDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var account = _context.Accounts
                                  .SingleOrDefault(a => a.UserName == username && a.Password == password);

            if (account != null)
            {
                HttpContext.Session.SetString("AccountId", account.AccountId);
                return RedirectToAction("Index", "Cart");
            }

            TempData["Error"] = "Thông tin đăng nhập không chính xác.";
            return RedirectToAction("Login");
        }
    }
}
