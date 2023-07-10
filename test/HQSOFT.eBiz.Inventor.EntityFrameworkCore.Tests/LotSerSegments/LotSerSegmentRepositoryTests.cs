using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HQSOFT.eBiz.Inventor.LotSerSegments;
using HQSOFT.eBiz.Inventor.EntityFrameworkCore;
using Xunit;

namespace HQSOFT.eBiz.Inventor.LotSerSegments
{
    public class LotSerSegmentRepositoryTests : InventorEntityFrameworkCoreTestBase
    {
        private readonly ILotSerSegmentRepository _lotSerSegmentRepository;

        public LotSerSegmentRepositoryTests()
        {
            _lotSerSegmentRepository = GetRequiredService<ILotSerSegmentRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _lotSerSegmentRepository.GetListAsync(
                    asgmentType: default,
                    value: "f14058c43e204f328f62ac7f1fb638cb7313444"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("af7233a0-c0cd-4283-bd48-88f99eebbefe"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _lotSerSegmentRepository.GetCountAsync(
                    asgmentType: default,
                    value: "a930a0d0130846c586839ff78d05c887f6a295c7f8f8488c9f3af800e"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}