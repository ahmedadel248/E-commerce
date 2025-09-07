using E_commerce.Models;
using E_commerce.Repositories;
using E_commerce.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class ProductController : Controller
    {
        //private ApplicationDbContext _context = new();

        //  private IProductRepositories  _productRepository = new ProductRepository();

        //    private IRepositories<Brand> _brandRepository = new Repository<Brand>();

        //  private IRepositories<Catgory> _catgoryRepository = new Repository<Catgory>();


        private IProductRepositories _productRepository;// = new ProductRepository();

        private IRepositories<Brand> _brandRepository;// = new Repository<Brand>();

        private IRepositories<Catgory> _catgoryRepository;// = new Repository<Catgory>();

        public ProductController(IProductRepositories productRepository,
            IRepositories<Brand> brandRepository,
            IRepositories<Catgory> catgoryRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _catgoryRepository = catgoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            //var products = _context.Products.Include(p=>p.Catgory).OrderBy(p=>p.Quantity);

            var products = await _productRepository.GetAll(includes: [p => p.Brand , p=>p.Catgory]);

            return View(products.ToList());
        }

        [HttpGet]
        public async Task <IActionResult> Create()
        {
            // var catgories = _context.Catgories;
            // var brands = _context.Brands;

            var catgories = await _catgoryRepository.GetAll();
            var brands = await _brandRepository.GetAll();
            var products = await _productRepository.GetAll();

            CatgoryWithBrandVM catgorywithbrandVM = new()
              
            {
                Catgories = catgories.ToList(),
                Brands = brands.ToList()
            };
            
            return View(catgorywithbrandVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product ,IFormFile MainImg)
        {
            // if we will save in phsical strage img save in wwwroot and
            // only its name will be in db so ican rich it using name 
            //or if we want to save img in db
            //you should change datatype in product model to be i formfile
            if (!ModelState.IsValid) // لو الفاليداشن فشل
            {
                return RedirectToAction(nameof(Create));
            }

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
                // _context.Products.Add(product);
                //_context.SaveChanges();

              await  _productRepository.Create(product);
               await _productRepository.CommitAsync();


                return RedirectToAction(nameof(Index));
            }

                  #endregion

                return BadRequest();

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            // var product = _context.Products.FirstOrDefault(c => c.Id == id);

            var product = await _productRepository.GetOne(p => p.Id == id);

            if (product is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);
            // var catgories = _context.Catgories;
            //var brands = _context.Brands;
            var catgories = await _catgoryRepository.GetAll();
            var brands = await _brandRepository.GetAll();
            var products = await _productRepository.GetAll();

            CatgoryWithBrandVM catgorywithbrandVM = new()

            {
                Catgories = catgories.ToList(),
                Brands = brands.ToList(),
                Product = product
            };

            return View(catgorywithbrandVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile? MainImg)
        {
            // var productimgindb = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);
            var productimgindb = await _productRepository.GetOne(p => p.Id==product.Id, tracked: false);

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
            // _context.Products.Update(product);
            // _context.SaveChanges();

            _productRepository.Update(product);
            await _productRepository.CommitAsync();

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int id)
        {
            //var product = _context.Products.FirstOrDefault(c => c.Id == id);

            var product = await _productRepository.GetOne(p => p.Id == id);

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
            // _context.Products.Remove(product);
            //_context.SaveChanges();

            _productRepository.Delete(product);
            await _productRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
