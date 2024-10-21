using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_Pet.Models.DataModels;

namespace Shop_Pet.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryPetController : Controller
    {
        private readonly ShopPetDbContext _context;

        public CategoryPetController(ShopPetDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CategoryPet> categoryPets = new List<CategoryPet>();
            categoryPets = _context.Categories.ToList();

            ViewBag.CategoryPets = categoryPets;

            return View();
        }

        public IActionResult Themmoi()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Luu(CategoryPet data)
        {
          //Thêm mới
             _context.Categories.Add(data);
            //Lưu
            _context.SaveChanges();
 
            return RedirectToAction("Index");

        }

        public IActionResult Xoa(int id)
        {
            CategoryPet categoryPet = _context.Categories.FirstOrDefault(c=>c.CategoryPetId == id);

            _context.Categories.Remove(categoryPet);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Sua(int id)
        {

            CategoryPet categoryPet = _context.Categories.FirstOrDefault(c => c.CategoryPetId == id);

            return View(categoryPet);
        }

        [HttpPost]
        public IActionResult Sua(CategoryPet categoryPet)

        { 

        // Attach the modified category to the context and mark it as modified.
        _context.Categories.Update(categoryPet);

        // Save changes to the database.
        _context.SaveChanges();

        // Redirect to the Index view after successful update.
        return RedirectToAction("Index");


   
}


    }
}
