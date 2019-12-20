using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MyABP.EntityFrameworkCore
{
    public static class MyABPDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MyABPDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MyABPDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
