using System.Linq.Expressions;
using GorevTakipSistemiAPI.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace GorevTakipSistemiAPI.Interface.IRepositories
{
    public interface IRepository<T> where T :BaseEntity
    {
        DbSet<T> Table { get; }

        List<T> GetAll(bool tracking = true);
        List<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);

        Task<bool> Add(T model);

        bool RemoveRange(List<int> id);
        Task<bool> Remove(int id);

        bool Update(T model);
        Task<int> SaveAsync();

    }
}
