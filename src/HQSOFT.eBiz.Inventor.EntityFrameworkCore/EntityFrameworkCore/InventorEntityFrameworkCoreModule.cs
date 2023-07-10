using HQSOFT.eBiz.Inventor.LotSerSegments;
using HQSOFT.eBiz.Inventor.LotSerClasses;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace HQSOFT.eBiz.Inventor.EntityFrameworkCore;

[DependsOn(
   typeof(InventorDomainModule),
   typeof(AbpEntityFrameworkCoreModule)
)]
public class InventorEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<InventorDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<LotSerClass, LotSerClasses.EfCoreLotSerClassRepository>();

            options.AddRepository<LotSerSegment, LotSerSegments.EfCoreLotSerSegmentRepository>();

        });

        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
    }
}