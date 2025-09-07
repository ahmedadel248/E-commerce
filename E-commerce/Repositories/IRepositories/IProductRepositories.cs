using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repositories.IRepositories
{
    public interface IProductRepositories : IRepositories<Product>
    {
        Task AddRangeAsync(List<Product> products);
    }
}
