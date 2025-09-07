using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_commerce.Repositories.IRepositories
{
    public interface IRepositories<T> where T : class 
    
    {


              Task Create(T entity);


             void Update(T entity);

             void Delete(T entity);

              Task CommitAsync();

              Task<List<T>> GetAll(Expression<Func<T, bool>>? expression = null,
                 Expression<Func<T, object>>[]? includes = null,
                 bool tracked = true);

              Task<T?> GetOne(Expression<Func<T, bool>> expression,
                Expression<Func<T, object>>[]? includes = null,
                bool tracked = true);

        
    }
}
