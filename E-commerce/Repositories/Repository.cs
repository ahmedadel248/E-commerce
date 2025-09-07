using E_commerce.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_commerce.Repositories
{
    public class Repository<T> : IRepositories<T> where T : class
    {
        //private ApplicationDbContext _context = new();
        private ApplicationDbContext _context;// = new();
        private DbSet<T> _dbSet;

        // to get table name
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Create (T entity)
        {
            // async method task to speed up the process when we get data from database return Task
            //هنا بشتغل بالتزامن معناها انى مش لازم اخد النتيجة كلها عشان انتقل للى بعدها ممكن اخد جزء واتنقل عادى

            // to add new category
            await _dbSet.AddAsync(entity);

            // to save changes in database
        }


        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }


        public void Delete(T entity)
        {
             _dbSet.Remove(entity);
        }

        public async Task CommitAsync() 
        {
         await _context.SaveChangesAsync();
        } 

        public async Task<List<T>> GetAll(Expression<Func<T,bool>>? expression=null,
             Expression<Func<T, object>>[]? includes = null , 
             bool tracked = true )
        {
            var entitys = _dbSet.AsQueryable();

            if (expression != null)
            {
                entitys = entitys.Where(expression);
            }

            if (includes != null)
            {
                foreach (var item in includes) 
                {
                    entitys = entitys.Include(item);
                }
            }

            if (!tracked)
            {
                entitys = entitys.AsNoTracking();
            }

            return await entitys.ToListAsync();
        }

        public async Task<T?> GetOne(Expression<Func<T, bool>> expression , 
            Expression<Func<T, object>>[]? includes = null,
            bool tracked = true)
        {
            return (await GetAll(expression,includes,tracked)).FirstOrDefault();
        }

    }
}
