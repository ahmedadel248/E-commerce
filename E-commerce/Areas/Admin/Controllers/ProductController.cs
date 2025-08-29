using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class ProductController : Controller
    {
        private ApplicationDbContext _context = new();

        public IActionResult Index()
        {
            var products = _context.Products.Include(p=>p.Catgory).OrderBy(p=>p.Quantity);

            return View(products.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var catgories = _context.Catgories;
            var brands = _context.Brands;
            CatgoryWithBrandVM catgorywithbrandVM = new()
              
            {
                Catgories = catgories.ToList(),
                Brands = brands.ToList()
            };
            
            return View(catgorywithbrandVM);
        }

        [HttpPost]
        public IActionResult Create(Product product ,IFormFile MainImg)
        {
            // if we will save in phsical strage img save in wwwroot and
            // only its name will be in db so ican rich it using name 
            //or if we want to save img in db
            //you should change datatype in product model to be i formfile

            #region save img in wwwroot
            if (MainImg is not null && MainImg.Length > 0)
            {

                
                
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(MainImg.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", filename);

                //save img in wwwrooot
                using (var stream = System.IO.File.Create(filePath))
                {
                    MainImg.CopyTo(stream);
                }
                //save img in db
                product.MainImg = filename;

                //save in db
                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

                  #endregion

                return BadRequest();

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            var product = _context.Products.FirstOrDefault(c => c.Id == id);

            if (product is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);
            var catgories = _context.Catgories;
            var brands = _context.Brands;

            CatgoryWithBrandVM catgorywithbrandVM = new()

            {
                Catgories = catgories.ToList(),
                Brands = brands.ToList(),
                Product = product
            };

            return View(catgorywithbrandVM);
        }

        [HttpPost]
        public IActionResult Edit(Product product, IFormFile? MainImg)
        {
            var productimgindb = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);
            if (productimgindb is null)
                return BadRequest();

            if (MainImg is not null && MainImg.Length > 0)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(MainImg.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", filename);

                using (var stream = System.IO.File.Create(filePath))
                {
                    MainImg.CopyTo(stream);
                }
                //delete old img
                var oldfilepath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot\\images", productimgindb.MainImg);

                if (System.IO.File.Exists(oldfilepath))
                
                {
                    System.IO.File.Delete(oldfilepath);
                }

                //update img in db
                product.MainImg = filename;

            }


           else //because we dont edit image dont make error cause update method not partial
            {
                product.MainImg = productimgindb.MainImg ;
            }

            //update in db
            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }


        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(c => c.Id == id);

            if (product is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            //delete old img
            var oldfilepath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot\\images", product.MainImg);

            if (System.IO.File.Exists(oldfilepath))

            {
                System.IO.File.Delete(oldfilepath);
            }
            //remove in db
            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
