using E_commerce.Models;
using E_commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class CatgoryController : Controller
    {
        private ApplicationDbContext _context = new();

        public IActionResult Index()
        {
            var catgories = _context.Catgories;

            return View(catgories.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View(new Catgory());
        }

        [HttpPost]
        public IActionResult Create(Catgory catgory)
        {
            //validate on data using model state dictionary
            //catch all datatypes(properties of model) excpact id icollection
            //key (model prop) value(model state entry
            //and its division into two part
            //1-add all data annotition we add in model file at model prop
            //2-aplly value with the annotition if good pass
            //3-not go to error message we add in span in view file or if we will ue error notifiction)
            //4-if not annotation will aplly or annotion good take 0 in the value result
            //5-make and op in result colum for all props
            //6-if and op = 0 its done
            //7-else go to step 3

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                TempData["error-notification"] = String.Join(", ", errors.Select(e => e.ErrorMessage));
                return View(catgory);
            }
            

            _context.Catgories.Add(catgory);
            _context.SaveChanges();

            TempData["success-notification"] = "Add Category successfully";
            //native tem data
            // Response.Cookies.Append("sucsess-notification", "Add Catgory sucsessfuly",new()
            // {
            //Secure = true,
            //            Expires = DateTime.Now.AddDays(14)
            // }
            // );

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var catgory = _context.Catgories.FirstOrDefault(c => c.Id == id);

            if (catgory is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            return View(catgory);
        }

        [HttpPost]
        public IActionResult Edit(Catgory catgory)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                TempData["error-notification"] = String.Join(", ", errors.Select(e => e.ErrorMessage));
                return View(catgory);
            }
            _context.Catgories.Update(catgory);
            _context.SaveChanges();

            TempData["success-notification"] = "Update Category successfully";

            return RedirectToAction(nameof(Index));

        }


        public IActionResult Delete(int id)
        {

            var catgory = _context.Catgories.FirstOrDefault(c => c.Id == id);

            if (catgory is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            _context.Catgories.Remove(catgory);
            _context.SaveChanges();

            TempData["success-notification"] = "Delete Category successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
