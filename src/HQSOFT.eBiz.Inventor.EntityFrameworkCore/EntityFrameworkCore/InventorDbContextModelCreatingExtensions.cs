using HQSOFT.eBiz.Inventor.LotSerSegments;
using Volo.Abp.EntityFrameworkCore.Modeling;
using HQSOFT.eBiz.Inventor.LotSerClasses;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace HQSOFT.eBiz.Inventor.EntityFrameworkCore;

public static class InventorDbContextModelCreatingExtensions
{
    public static void ConfigureInventor(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(InventorDbProperties.DbTablePrefix + "Questions", InventorDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<LotSerClass>(b =>
{
    b.ToTable(InventorDbProperties.DbTablePrefix + "LotSerClasses", InventorDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.ClassID).HasColumnName(nameof(LotSerClass.ClassID)).IsRequired();
    b.Property(x => x.Description).HasColumnName(nameof(LotSerClass.Description)).IsRequired();
    b.Property(x => x.TrackingMethod).HasColumnName(nameof(LotSerClass.TrackingMethod));
    b.Property(x => x.TrackExpriationDate).HasColumnName(nameof(LotSerClass.TrackExpriationDate));
    b.Property(x => x.RequiredforDropShip).HasColumnName(nameof(LotSerClass.RequiredforDropShip));
    b.Property(x => x.AssignMethod).HasColumnName(nameof(LotSerClass.AssignMethod));
    b.Property(x => x.IssueMethod).HasColumnName(nameof(LotSerClass.IssueMethod));
    b.Property(x => x.ShareAutoIncremenitalValueBetwenAllClassItems).HasColumnName(nameof(LotSerClass.ShareAutoIncremenitalValueBetwenAllClassItems));
    b.Property(x => x.AutoIncremetalValue).HasColumnName(nameof(LotSerClass.AutoIncremetalValue));
    b.Property(x => x.AutoGenerateNextNumber).HasColumnName(nameof(LotSerClass.AutoGenerateNextNumber));
    b.Property(x => x.MaxAutoNumbers).HasColumnName(nameof(LotSerClass.MaxAutoNumbers));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<LotSerSegment>(b =>
{
    b.ToTable(InventorDbProperties.DbTablePrefix + "LotSerSegments", InventorDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.SegmentID).HasColumnName(nameof(LotSerSegment.SegmentID));
    b.Property(x => x.AsgmentType).HasColumnName(nameof(LotSerSegment.AsgmentType));
    b.Property(x => x.Value).HasColumnName(nameof(LotSerSegment.Value));
});

        }
    }
}