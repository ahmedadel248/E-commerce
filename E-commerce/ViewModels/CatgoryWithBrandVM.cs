using E_commerce.Models;

namespace E_commerce.ViewModels
{
    public class CatgoryWithBrandVM
    {

            public List<Catgory> Catgories { get; set; } = null!;
            public List<Brand> Brands { get; set; } = null!;
            public Product? Product { get; set; }
        

}

}
