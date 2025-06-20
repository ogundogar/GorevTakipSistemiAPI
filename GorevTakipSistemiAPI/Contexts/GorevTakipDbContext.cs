using GorevTakipSistemiAPI.Entities;
using GorevTakipSistemiAPI.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GorevTakipSistemiAPI.Contexts
{
    public class GorevTakipDbContext:IdentityDbContext<Kullanici,Role,int>
    {
        public GorevTakipDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Gorev> Gorevler { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                    data.Entity.CreateDate = DateTime.Now;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
