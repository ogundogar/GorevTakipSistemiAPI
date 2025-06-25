using System.Linq.Expressions;
using GorevTakipSistemiAPI.Contexts;
using GorevTakipSistemiAPI.Entities.Common;
using GorevTakipSistemiAPI.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GorevTakipSistemiAPI.IRepositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private GorevTakipDbContext _context;
        public Repository(GorevTakipDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public List<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query.ToList();
        }

        public List<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query.ToList();
        }

        public async Task<bool> Add(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }


        public bool RemoveRange(List<int> id)
        {
            Table.RemoveRange(Table.Where(x=>id.Contains(x.Id)).ToList());
            return true;
        }

        public async Task<bool> Remove(int id)
        {
            T? model = await Table.FirstOrDefaultAsync(p => p.Id == id);
            if (model != null)
            {
                EntityEntry<T> entityEntry = Table.Remove(model);
                return entityEntry.State == EntityState.Deleted;
            }
            return false;
        }

        public bool Update(T model)
        {

            EntityEntry<T> entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    }
}
