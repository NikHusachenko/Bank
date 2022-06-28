using Microsoft.EntityFrameworkCore;

namespace Bank.Database.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        public DbSet<T> Table { get; }

        void Create(T item);

        T GetById(int id);

        List<T> Get();

        List<T> Get(Func<T, bool> predicate);

        void Remove(T item);

        void Update(T item);

        void SaveChanges();
    }
}