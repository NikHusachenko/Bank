using Microsoft.EntityFrameworkCore;

namespace Bank.Database.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public DbSet<T> Table { get; }

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            Table = _context.Set<T>();
        }

        public void Create(T item)
        {
            Table.Add(item);
        }

        public T GetById(int id)
        {
            return Table.Find(id);
        }

        public List<T> Get()
        {
            return Table.AsNoTracking().ToList();
        }

        public List<T> Get(Func<T, bool> predicate)
        {
            return Table.AsNoTracking().Where(predicate).ToList();
        }

        public void Remove(T item)
        {
            Table.Remove(item);
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}