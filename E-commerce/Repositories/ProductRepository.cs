using E_commerce.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_commerce.Repositories
{
    //open for extend close for modification
    public class ProductRepository : Repository<Product> , IProductRepositories 
    {
        // private ApplicationDbContext _context = new();

        private ApplicationDbContext _context; //= new();


        public ProductRepository (ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
        }
    }
}
