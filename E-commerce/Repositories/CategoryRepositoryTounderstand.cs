using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_commerce.Repositories
{
    public class CategoryRepositoryTounderstand
    {
        private ApplicationDbContext _context = new();
        private DbSet<Catgory> _dbSet;

        public async Task Create (Catgory catgory)
        {
            // async method task to speed up the process when we get data from database return Task
            //هنا بشتغل بالتزامن معناها انى مش لازم اخد النتيجة كلها عشان انتقل للى بعدها ممكن اخد جزء واتنقل عادى

            // to add new category
            await _context.Catgories.AddAsync(catgory);

            // to save changes in database
        }


        public void Update(Catgory catgory)
        {
            _context.Catgories.Update(catgory);
        }


        public void Delete(Catgory catgory)
        {
             _context.Catgories.Remove(catgory);
        }

        public async Task CommitAsync() 
        {
         await _context.SaveChangesAsync();
        } 

        public async Task<List<Catgory>> GetAll(Expression<Func<Catgory,bool>>? expression=null)
        {
            var catgories = _context.Catgories.AsQueryable();

            if (expression != null)
            {
                catgories = catgories.Where(expression);
            }
            return await catgories.ToListAsync();
        }

        public async Task<Catgory?> GetOne(Expression<Func<Catgory, bool>> expression)
        {
            return (await GetAll(expression)).FirstOrDefault();
        }

    }
}
