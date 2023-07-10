using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HQSOFT.eBiz.Inventor.LotSerClasses;
using HQSOFT.eBiz.Inventor.EntityFrameworkCore;
using Xunit;

namespace HQSOFT.eBiz.Inventor.LotSerClasses
{
    public class LotSerClassRepositoryTests : InventorEntityFrameworkCoreTestBase
    {
        private readonly ILotSerClassRepository _lotSerClassRepository;

        public LotSerClassRepositoryTests()
        {
            _lotSerClassRepository = GetRequiredService<ILotSerClassRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _lotSerClassRepository.GetListAsync(
                    classID: "39b72eb4f1944c1ba2e5ce13b9570e4eb03de09cf1c34f5c91f68a07a1fa27e21c6bf67ceb914051bf0e55544",
                    description: "d8209daf500e4dfbbcb2f008b9c561b7bfa880cc10d6422981644a8ad59eeb41c1ddca37c8",
                    trackingMethod: default,
                    trackExpriationDate: true,
                    requiredforDropShip: true,
                    assignMethod: default,
                    issueMethod: default,
                    shareAutoIncremenitalValueBetwenAllClassItems: true,
                    autoGenerateNextNumber: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("9ee0a9e5-3864-44a7-8744-635bba2959bd"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _lotSerClassRepository.GetCountAsync(
                    classID: "a9e4adb624ef42",
                    description: "1df32d5868ad43308dd0a619732331e601d8afc9772040e3afc",
                    trackingMethod: default,
                    trackExpriationDate: true,
                    requiredforDropShip: true,
                    assignMethod: default,
                    issueMethod: default,
                    shareAutoIncremenitalValueBetwenAllClassItems: true,
                    autoGenerateNextNumber: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}