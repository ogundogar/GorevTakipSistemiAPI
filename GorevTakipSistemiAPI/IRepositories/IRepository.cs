using System.Linq.Expressions;
using GorevTakipSistemiAPI.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace GorevTakipSistemiAPI.IRepository
{
    public interface IRepository<T> where T :BaseEntity
    {
        DbSet<T> Table { get; }

        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);

        Task<bool> Add(T model);

        bool RemoveRange(List<int> id);
        Task<bool> Remove(int id);

        bool Update(T model);
        Task<int> SaveAsync();

    }
}
