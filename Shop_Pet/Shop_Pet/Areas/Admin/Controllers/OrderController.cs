using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_Pet.Models.DataModels;

namespace Shop_Pet.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ShopPetDbContext _context;

        public OrderController(ShopPetDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Orders> orders = new List<Orders>();
            orders = _context.Orders.ToList();

            ViewBag.Orders = orders;
            return View();
        }
    }
}
