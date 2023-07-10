using HQSOFT.eBiz.Inventor.LotSerSegments;
using HQSOFT.eBiz.Inventor.LotSerClasses;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HQSOFT.eBiz.Inventor.EntityFrameworkCore;

[ConnectionStringName(InventorDbProperties.ConnectionStringName)]
public class InventorDbContext : AbpDbContext<InventorDbContext>, IInventorDbContext
{
    public DbSet<LotSerSegment> LotSerSegments { get; set; }
    public DbSet<LotSerClass> LotSerClasses { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public InventorDbContext(DbContextOptions<InventorDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureInventor();
    }
}