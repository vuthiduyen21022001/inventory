using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HQSOFT.eBiz.Inventor.EntityFrameworkCore;

public class InventorHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<InventorHttpApiHostMigrationsDbContext>
{
    public InventorHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<InventorHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Inventor"));

        return new InventorHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
