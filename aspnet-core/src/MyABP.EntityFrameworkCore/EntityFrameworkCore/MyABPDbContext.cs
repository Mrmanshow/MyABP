using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MyABP.Authorization.Roles;
using MyABP.Authorization.Users;
using MyABP.MultiTenancy;
using MyABP.Game;
using MyABP.Operation;

namespace MyABP.EntityFrameworkCore
{
    public class MyABPDbContext : AbpZeroDbContext<Tenant, Role, User, MyABPDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<LabaList> LabaList { set; get; }

        public DbSet<LabaMultiple> LabaMultiple { set; get; }

        public DbSet<LabaOrder> LabaOrder { set; get; }

        public DbSet<LabaOrderDetail> LabaOrderDetail { set; get; }

        public DbSet<UserAssets> UserAssets { set; get; }

        public DbSet<LabaWinRoute> LabaWinRoute { set; get; }

        public DbSet<Banner> Banner { set; get; }

        public DbSet<Article> Article { set; get; }

        public MyABPDbContext(DbContextOptions<MyABPDbContext> options)
            : base(options)
        {
        }
    }
}
