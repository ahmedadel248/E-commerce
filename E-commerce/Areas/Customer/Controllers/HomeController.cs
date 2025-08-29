using System.Diagnostics;
using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using E_commerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using E_commerce.Data;
namespace E_commerce.Areas.Customer.Controllers {

    
        [Area (SD.CustomerArea) ]
        public class HomeController : Controller
        {
            private readonly ILogger<HomeController> _logger;
            private ApplicationDbContext _context=new ApplicationDbContext();
            public HomeController(ILogger<HomeController> logger)
            {
                _logger = logger;
            }

            public IActionResult Index(ProductFilterVM productfilterVM ,[FromQuery]int page=1)
            {
            const double discount = 0.01;
                var products = _context.Products.Include(p => p.Catgory).AsQueryable();
            
            #region filter

            if (productfilterVM.ProductName is not null)
                {
                    products = products.Where(p => p.Name.Contains(productfilterVM.ProductName));
                    ViewBag.ProductName = productfilterVM.ProductName;
                }
                //لما اجى اعمل فيلتر بعد ما بملي الفورم ويفلتر بيمسح اللى مليته فحلها
                // انى ارد من الفرونت للباك تانى زي مالباك راح للفرونت
                // هستخدم الفيو باج عشان احنفظ بالداتا بالنسبة للباك
                //وفي الفرونت هتاخد الفاليو دي المكان اللي بيتملي في الفورم الي بتقرا منه كله خانة
                
                if (productfilterVM.Minprice is not null)
                {
                    products = products.Where(p => p.Price - p.Price * (p.Discount / 100) >= productfilterVM.Minprice);
                    ViewBag.MinPrice = productfilterVM.Minprice;
                }

                if (productfilterVM.Maxprice is not null)
                {
                    products = products.Where(p => p.Price - p.Price * (p.Discount / 100) <= productfilterVM.Maxprice);
                    ViewBag.Maxprice = productfilterVM.Maxprice;
                }

                if (productfilterVM.CatgoryId is not null)
                {
                    products = products.Where(p => p.CatgoryId == productfilterVM.CatgoryId);
                    ViewBag.CatgoryId = productfilterVM.CatgoryId;
                }


               if (productfilterVM.IsHot is not null)
               {
                  if (productfilterVM.IsHot.Value)
                   {
                        products = products.Where(p => p.Discount > discount); // hot
                   }
                  else
                   {
                        products = products.Where(p => p.Discount <= discount); // not hot
                   }
                    ViewBag.IsHot = productfilterVM.IsHot;
                }

            #endregion

            #region Pagination
            double totalPages = Math.Ceiling(products.Count() / 8.0); // 3.1 => 4
                int currentPage = page;

                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = currentPage;

                products = products.Skip((page - 1) * 8).Take(8);
                var catgories = _context.Catgories.ToList();

            //view bag also used as annynomyess class or view model
            //used to send data from backend to frontend but it special only for mvc and ther
            //view data like view bag but has autocomplete
            // ViewData["Catgories"] = catgories;
            ViewBag.Catgories = catgories;
            #endregion

            return View(products.ToList());
            }


            public IActionResult Details([FromRoute]int id) 
            {
                /*
                 هنا بدلنا الرقم بتاع الاي دي اللى كنت حطه بقيمه متغير وهو الاي دي اللي موجود ف الرووت ف البروجرم فايل 
                وبدلالتها هقدر اوصل لتفاصيل المنتج وعندي طريقتين 
                Model binding
                1-Route date= hidden field /id=1
                ميزة الكويري سترينج انى اقدر ابعت معاها اكتر ن حاجة لو الميثود عندى مستنيها 
                2- query string ?id= &Name=ahmed, arrays[0]=,....
                3-form data all data send using submit button depend on quey string
                4-header
                5-Body
                طبعا هنشيل الكلام ده ونحطه ف ميثود ديتيلز في الانيدس اللي فيه الصفحة الرئيسية عشان لما ادوس عليها توديني ليها
                */
                var product = _context.Products.Include(p => p.Catgory).FirstOrDefault(p=>p.Id==id);

                #region NotFoundPageAutoBy.net
                if (product is null)
                    return NotFound();
                #endregion

                #region RealtedProduct
                var rp = _context.Products.Include(p=>p.Catgory).Where(p => p.CatgoryId
                == product.CatgoryId && p.Id!=product.Id).Skip(0).Take(4);

                #endregion

                #region update traffic
                product.Traffic += 1;
                _context.SaveChanges();
                #endregion

                #region product most view
                var toptrafficproduct = _context.Products.Include(p=>p.Catgory).OrderByDescending(p => p.Traffic)
                    .Where(p => p.Id != product.Id).Skip(0).Take(4);
                #endregion

                #region similar product
                var similarproduct = _context.Products.Include(p=>p.Catgory).Where(p => p.Name.Contains(product.Name) 
                && p.Id != product.Id).Skip(0).Take(4);
                #endregion
                RelatedProductVM relatedproductvm = new()
                {
                    Product = product,
                    RP = rp.ToList(),
                    TopTrafficProduct = toptrafficproduct.ToList(),
                    SimilarProduct = similarproduct.ToList()
                };

                return View(relatedproductvm);

            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }

