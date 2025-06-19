using GorevTakipSistemiAPI.Contexts;
using GorevTakipSistemiAPI.IRepositories;
using GorevTakipSistemiAPI.IRepositories.IGorev;

namespace GorevTakipSistemiAPI.Repositories.Gorev
{
    public class RepositoryGorev:Repository<Entities.Gorev>,IRepositoryGorev
    {
        public RepositoryGorev(GorevTakipDbContext context) : base(context)
        {
        }
    }
}
