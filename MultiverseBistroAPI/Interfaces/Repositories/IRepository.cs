using System.Linq.Expressions;

namespace MultiverseBistroAPI.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetAll();
        public T FindById(Guid ID);
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public void Save();
    }
}
