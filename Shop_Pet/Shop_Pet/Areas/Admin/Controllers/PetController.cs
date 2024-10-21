using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Pet.Models.DataModels;

namespace Shop_Pet.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PetController : Controller
    {
        private readonly ShopPetDbContext _context;

        public PetController(ShopPetDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string name)
        {
            var pets = _context.Pets.Include(p => p.CategoryPet).AsQueryable();

            // Filter pets based on search input
            if (!string.IsNullOrWhiteSpace(name))
            {
                pets = pets.Where(p => p.name.Contains(name) ||
                                       p.breed.Contains(name) ||
                                       (p.CategoryPet != null && p.CategoryPet.species.Contains(name))); // Assuming CategoryPet has a Name property
            }
            ViewBag.Pets = pets;
            return View(pets.ToList());
        }

        public IActionResult Themmoi()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Luu(Pet data)
        {
            //Thêm mới
            _context.Pets.Add(data);
            //luu vao database
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Xoa(int id)
        {
            //xoa Theo Id
            Pet pet = _context.Pets.FirstOrDefault(x=>x.pet_id == id);
            
            _context.Pets.Remove(pet);
            //lưu
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Sua(int id)
        {
            // Tìm kiếm thú cưng theo id
            var pet = _context.Pets.Include(p => p.CategoryPet).FirstOrDefault(p => p.pet_id == id);

            if (pet == null)
            {
                return NotFound(); // You may want to return a NotFound view or redirect as necessary
            }

            ViewBag.Categories = _context.Categories.ToList();
            // Trả về view để hiển thị thông tin hiện tại cho người dùng chỉnh sửa
            return View(pet);
        }

        [HttpPost]
        public IActionResult Sua(Pet pet)
        {
            var existingPet = _context.Pets.Find(pet.pet_id);
            if (existingPet != null)
            {
                // Tìm kiếm thú cưng trong cơ sở dữ liệu

               
                // Cập nhật các thuộc tính của đối tượng
                existingPet.Images = pet.Images;
                existingPet.name = pet.name;
                existingPet.CategoryPetId = pet.CategoryPetId;
                existingPet.breed = pet.breed;
                existingPet.age = pet.age;
                existingPet.price = pet.price;
                existingPet.quantity = pet.quantity;
                existingPet.description = pet.description;

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

            }


            // Chuyển hướng đến một trang khác hoặc trả về view thành công
            return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách
        }

       
    }
}
