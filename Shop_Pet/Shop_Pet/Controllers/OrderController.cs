using Microsoft.AspNetCore.Mvc;
using Shop_Pet.Models.DataModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Shop_Pet.Models;
using System.Diagnostics;

namespace Shop_Pet.Controllers
{
    public class OrderController : Controller

    {
        private readonly ILogger<HomeController> _logger;
       

        public readonly ShopPetDbContext _Context;

      
        public OrderController(ShopPetDbContext context)
        {
           _Context = context;
        }
        public List<Cart> carts
        {

            get
            {
                var data = HttpContext.Session.Get<List<Cart>>(MySetting.CART_KEY);
                return data ?? new List<Cart>(); // Simplified
            }
        }
        public IActionResult Index()
        {
          

            return View(carts);
        }
        [HttpPost]
      
       public IActionResult Checkout(string fullname,string email,string phone,string address)
        {
            // Su ly du lieu nguoi mua
            if(string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address))
            {
                ViewBag.error = "Vui long nhap thong tin";
                return View();
            }
            string namepet = string.Join(",", carts.Select(c=>c.Tenpet));
            string imagesPet = string.Join(",", carts.Select(c=>c.Images));

            Orders neworders = new Orders() 
            {
                Fullname = fullname,
                Email = email,
                Phone = phone,
                Address = address,
                NamePet = namepet,
                ImagesPet = imagesPet
            };
            _Context.Orders.Add(neworders);
            _Context.SaveChanges();
            return RedirectToAction("OrdersSucess");
        
        }
        public IActionResult OrdersSucess()
        {
            return View();
        }


    }
}
