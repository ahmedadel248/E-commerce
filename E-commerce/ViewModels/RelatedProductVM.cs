using E_commerce.Models;

namespace E_commerce.ViewModels
{
    public class RelatedProductVM
    {
        public Product Product { get; set; } = null!;
        public List<Product> RP { get; set; } = null!;
        public List<Product> TopTrafficProduct { get; set; } = null!;
        public List<Product> SimilarProduct { get; set; } = null!;


    }
}
