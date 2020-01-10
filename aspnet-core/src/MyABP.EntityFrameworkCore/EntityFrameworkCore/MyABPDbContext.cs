using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MyABP.Authorization.Roles;
using MyABP.Authorization.Users;
using MyABP.MultiTenancy;
using MyABP.Game;
using MyABP.Operation;
using MyABP.Shop;
using MyABP.Extensions;

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

        public DbSet<ProductOrder> ProductOrder { set; get; }

        public DbSet<Address> Address { set; get; }

        public DbSet<Product> Product { set; get; }

        public DbSet<NotificationsTemplate> NotificationsTemplate { set; get; }

        public MyABPDbContext(DbContextOptions<MyABPDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Ignore<NotificationSubscriptionInfo>().;
            //modelBuilder.Entity<NotificationSubscriptionInfoForUser>()
            //    .HasBaseType<NotificationSubscriptionInfo>();

            //modelBuilder.Entity<NotificationSubscriptionInfoForUser>()
            //    .HasDiscriminator<NotificationSubscriptionInfoForUser, string>(x => x.Discriminator)
            //    .HasValue("NotificationSubscriptionInfo");

            // 修改表名和字段格式（MySQL）
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!string.IsNullOrWhiteSpace(entityType.Relational().TableName))
                {
                    entityType.Relational().TableName = entityType.Relational().TableName.HumpToUnderline();
                }
                foreach (var property in entityType.GetProperties())
                {
                    property.Relational().ColumnName = property.Relational().ColumnName.HumpToUnderline();
                }
            }
        }
    }
}
