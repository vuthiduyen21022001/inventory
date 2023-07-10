using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HQSOFT.eBiz.Inventor.LotSerSegments
{
    public class LotSerSegmentsAppServiceTests : InventorApplicationTestBase
    {
        private readonly ILotSerSegmentsAppService _lotSerSegmentsAppService;
        private readonly IRepository<LotSerSegment, Guid> _lotSerSegmentRepository;

        public LotSerSegmentsAppServiceTests()
        {
            _lotSerSegmentsAppService = GetRequiredService<ILotSerSegmentsAppService>();
            _lotSerSegmentRepository = GetRequiredService<IRepository<LotSerSegment, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _lotSerSegmentsAppService.GetListAsync(new GetLotSerSegmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("af7233a0-c0cd-4283-bd48-88f99eebbefe")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("92735467-b221-412a-b56a-b23e7d149ce3")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _lotSerSegmentsAppService.GetAsync(Guid.Parse("af7233a0-c0cd-4283-bd48-88f99eebbefe"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("af7233a0-c0cd-4283-bd48-88f99eebbefe"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new LotSerSegmentCreateDto
            {
                SegmentID = 599618040,
                AsgmentType = default,
                Value = "95094bf1642349e38587de761599fd49e3c5d7c832"
            };

            // Act
            var serviceResult = await _lotSerSegmentsAppService.CreateAsync(input);

            // Assert
            var result = await _lotSerSegmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.SegmentID.ShouldBe(599618040);
            result.AsgmentType.ShouldBe(default);
            result.Value.ShouldBe("95094bf1642349e38587de761599fd49e3c5d7c832");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new LotSerSegmentUpdateDto()
            {
                SegmentID = 1592844100,
                AsgmentType = default,
                Value = "00ad227933754b7a994666a8d3ffd47799903272e3fb43e79169dff9364d02dafbebb2b9d77c44f6b50017521cd52474290"
            };

            // Act
            var serviceResult = await _lotSerSegmentsAppService.UpdateAsync(Guid.Parse("af7233a0-c0cd-4283-bd48-88f99eebbefe"), input);

            // Assert
            var result = await _lotSerSegmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.SegmentID.ShouldBe(1592844100);
            result.AsgmentType.ShouldBe(default);
            result.Value.ShouldBe("00ad227933754b7a994666a8d3ffd47799903272e3fb43e79169dff9364d02dafbebb2b9d77c44f6b50017521cd52474290");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _lotSerSegmentsAppService.DeleteAsync(Guid.Parse("af7233a0-c0cd-4283-bd48-88f99eebbefe"));

            // Assert
            var result = await _lotSerSegmentRepository.FindAsync(c => c.Id == Guid.Parse("af7233a0-c0cd-4283-bd48-88f99eebbefe"));

            result.ShouldBeNull();
        }
    }
}