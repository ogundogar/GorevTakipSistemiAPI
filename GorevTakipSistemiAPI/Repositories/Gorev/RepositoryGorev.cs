using GorevTakipSistemiAPI.Contexts;
using GorevTakipSistemiAPI.Interface.IRepositories.IGorev;
using GorevTakipSistemiAPI.IRepositories;

namespace GorevTakipSistemiAPI.Repositories.Gorev
{
    public class RepositoryGorev:Repository<Entities.Gorev>,IRepositoryGorev
    {
        public RepositoryGorev(GorevTakipDbContext context) : base(context)
        {
        }
    }
}
