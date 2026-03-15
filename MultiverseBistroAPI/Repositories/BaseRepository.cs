using Microsoft.EntityFrameworkCore;
using MultiverseBistroAPI.Data;
using MultiverseBistroAPI.Interfaces.Repositories;
using System.Linq.Expressions;

namespace MultiverseBistroAPI.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(AppDBContext appDbContext)
        {
            _context = appDbContext;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T FindById(Guid ID)
        {
            return _dbSet.Find(ID);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
