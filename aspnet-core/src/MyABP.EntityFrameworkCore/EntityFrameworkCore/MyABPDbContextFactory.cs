using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyABP.Configuration;
using MyABP.Web;

namespace MyABP.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MyABPDbContextFactory : IDesignTimeDbContextFactory<MyABPDbContext>
    {
        public MyABPDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MyABPDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MyABPDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MyABPConsts.ConnectionStringName));

            return new MyABPDbContext(builder.Options);
        }
    }
}
