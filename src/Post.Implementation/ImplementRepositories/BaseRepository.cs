using Post.Application.Interfaces;
using Post.Application.Interfaces.Const;
using System.Linq.Expressions;

namespace Post.Implementation.ImplementRepositories
{
    public sealed class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
      

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
         
        }

        public async Task<T> AddAsync(T type)
        {
           await _context.Set<T>().AddAsync(type);
           await _context.SaveChangesAsync();
           return type;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().AsNoTracking().CountAsync(criteria);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().AsNoTracking().CountAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> criteria = null,
            string orederByDirection = OrderBy.Ascending, 
            Expression<Func<T, object>>? orderby = null, 
            string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (criteria != null)
                query = query.Where(criteria);
            if (orderby != null)
            {
                if (orederByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderby);
                else
                    query = query.OrderByDescending(orderby);
            }

            //if (skip.HasValue && take.HasValue)
            //    query = query.Skip(skip.Value).Take(take.Value);


            if (includes != null)
                foreach (var inc in includes)
                    query = query.Include(inc);
            return await query.ToListAsync();
        }

        public async Task<T> GetByAsync(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var inc in includes)
                    query = query.Include(inc);
            return await query.SingleOrDefaultAsync(criteria);
        }

        public async Task UpdateAsync(T type)
        {
            //_context.Entry(type).State = EntityState.Modified;
            _context.Set<T>().Update(type);
            await _context.SaveChangesAsync();
            
        }
    }
}
