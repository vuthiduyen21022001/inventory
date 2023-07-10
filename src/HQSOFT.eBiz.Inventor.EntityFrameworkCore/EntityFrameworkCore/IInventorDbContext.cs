using HQSOFT.eBiz.Inventor.LotSerSegments;
using HQSOFT.eBiz.Inventor.LotSerClasses;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HQSOFT.eBiz.Inventor.EntityFrameworkCore;

[ConnectionStringName(InventorDbProperties.ConnectionStringName)]
public interface IInventorDbContext : IEfCoreDbContext
{
    DbSet<LotSerSegment> LotSerSegments { get; set; }
    DbSet<LotSerClass> LotSerClasses { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}