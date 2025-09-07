using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class BrandController : Controller
    {
        //private ApplicationDbContext _context = new();

        //private BrandRepository brandRepository = new();

        // private IRepositories<Brand> _brandRepository = new Repository<Brand>();
        private IRepositories<Brand> _brandRepository;// = new Repository<Brand>();

        public BrandController(IRepositories<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task <IActionResult> Index()
        {
            var brands = await _brandRepository.GetAll();

            return View(brands.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {
            //_context.Brands.Add(brand);
            //_context.SaveChanges();

            if (!ModelState.IsValid) // لو الفاليداشن فشل
            {
                return RedirectToAction(nameof(Create));
            }
            await _brandRepository.Create(brand);
            await _brandRepository.CommitAsync();
            TempData["success-notification"] = "Add Brand successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //var Brand = _context.Brands.FirstOrDefault(c => c.Id == id);
            var brand = await _brandRepository.GetOne(b => b.Id == id);
            if (brand is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            return View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Brand brand)
        {
            // _context.Brands.Update(brand);
            //_context.SaveChanges();

            _brandRepository.Update(brand);
            await _brandRepository.CommitAsync();
            TempData["success-notification"] = "Edit Brand successfully";

            return RedirectToAction(nameof(Index));
        }

        
        public async Task <IActionResult> Delete(int id)
        {
            //var brand = _context.Brands.FirstOrDefault(c => c.Id == id);
            var brand = await _brandRepository.GetOne(b => b.Id == id);
            if (brand is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            //_context.Brands.Remove(brand);
            //_context.SaveChanges();
            _brandRepository.Delete(brand);
            await _brandRepository.CommitAsync();
            TempData["success-notification"] = "Delete Brand successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
