using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class BrandController : Controller
    {
        private ApplicationDbContext _context = new();

        public IActionResult Index()
        {
            var brands = _context.Brands;

            return View(brands.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Brand Brand)
        {
            _context.Brands.Add(Brand);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Brand = _context.Brands.FirstOrDefault(c => c.Id == id);

            if (Brand is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            return View(Brand);
        }

        [HttpPost]
        public IActionResult Edit(Brand Brand)
        {
            _context.Brands.Update(Brand);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Delete(int id)
        {
            var brand = _context.Brands.FirstOrDefault(c => c.Id == id);

            if (brand is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            _context.Brands.Remove(brand);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
