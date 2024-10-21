using Microsoft.AspNetCore.Mvc;
using Shop_Pet.Models.DataModels;

namespace Shop_Pet.ViewComponent
{
    public class CartView: Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<Cart>>(MySetting.CART_KEY) ?? new List<Cart>();
            return View("CartPanel", new CartModel
            {
                Quantity = cart.Sum(x => x.Quantity),
                Total = cart.Sum(x => x.Thanhtien),
            });
        }
    }
}
