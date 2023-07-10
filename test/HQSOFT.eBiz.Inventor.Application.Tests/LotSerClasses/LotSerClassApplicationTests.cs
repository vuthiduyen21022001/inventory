using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HQSOFT.eBiz.Inventor.LotSerClasses
{
    public class LotSerClassesAppServiceTests : InventorApplicationTestBase
    {
        private readonly ILotSerClassesAppService _lotSerClassesAppService;
        private readonly IRepository<LotSerClass, Guid> _lotSerClassRepository;

        public LotSerClassesAppServiceTests()
        {
            _lotSerClassesAppService = GetRequiredService<ILotSerClassesAppService>();
            _lotSerClassRepository = GetRequiredService<IRepository<LotSerClass, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _lotSerClassesAppService.GetListAsync(new GetLotSerClassesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("9ee0a9e5-3864-44a7-8744-635bba2959bd")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("98af4718-e00b-47c7-b6a1-a32f5132e370")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _lotSerClassesAppService.GetAsync(Guid.Parse("9ee0a9e5-3864-44a7-8744-635bba2959bd"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("9ee0a9e5-3864-44a7-8744-635bba2959bd"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new LotSerClassCreateDto
            {
                ClassID = "2d19a81ba0aa45bb8fdeeab1d4afd74c",
                Description = "3c47eabc32b545dba35fea621ca0e8a9f6f",
                TrackingMethod = default,
                TrackExpriationDate = true,
                RequiredforDropShip = true,
                AssignMethod = default,
                IssueMethod = default,
                ShareAutoIncremenitalValueBetwenAllClassItems = true,
                AutoIncremetalValue = 11892714,
                AutoGenerateNextNumber = true,
                MaxAutoNumbers = 2060420180
            };

            // Act
            var serviceResult = await _lotSerClassesAppService.CreateAsync(input);

            // Assert
            var result = await _lotSerClassRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ClassID.ShouldBe("2d19a81ba0aa45bb8fdeeab1d4afd74c");
            result.Description.ShouldBe("3c47eabc32b545dba35fea621ca0e8a9f6f");
            result.TrackingMethod.ShouldBe(default);
            result.TrackExpriationDate.ShouldBe(true);
            result.RequiredforDropShip.ShouldBe(true);
            result.AssignMethod.ShouldBe(default);
            result.IssueMethod.ShouldBe(default);
            result.ShareAutoIncremenitalValueBetwenAllClassItems.ShouldBe(true);
            result.AutoIncremetalValue.ShouldBe(11892714);
            result.AutoGenerateNextNumber.ShouldBe(true);
            result.MaxAutoNumbers.ShouldBe(2060420180);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new LotSerClassUpdateDto()
            {
                ClassID = "2936ac2d11f6405d8dfc62c5ffa158f1cfdfa591078",
                Description = "995be7c3d06c41de92a50c6266912d06a96",
                TrackingMethod = default,
                TrackExpriationDate = true,
                RequiredforDropShip = true,
                AssignMethod = default,
                IssueMethod = default,
                ShareAutoIncremenitalValueBetwenAllClassItems = true,
                AutoIncremetalValue = 129543689,
                AutoGenerateNextNumber = true,
                MaxAutoNumbers = 515494509
            };

            // Act
            var serviceResult = await _lotSerClassesAppService.UpdateAsync(Guid.Parse("9ee0a9e5-3864-44a7-8744-635bba2959bd"), input);

            // Assert
            var result = await _lotSerClassRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ClassID.ShouldBe("2936ac2d11f6405d8dfc62c5ffa158f1cfdfa591078");
            result.Description.ShouldBe("995be7c3d06c41de92a50c6266912d06a96");
            result.TrackingMethod.ShouldBe(default);
            result.TrackExpriationDate.ShouldBe(true);
            result.RequiredforDropShip.ShouldBe(true);
            result.AssignMethod.ShouldBe(default);
            result.IssueMethod.ShouldBe(default);
            result.ShareAutoIncremenitalValueBetwenAllClassItems.ShouldBe(true);
            result.AutoIncremetalValue.ShouldBe(129543689);
            result.AutoGenerateNextNumber.ShouldBe(true);
            result.MaxAutoNumbers.ShouldBe(515494509);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _lotSerClassesAppService.DeleteAsync(Guid.Parse("9ee0a9e5-3864-44a7-8744-635bba2959bd"));

            // Assert
            var result = await _lotSerClassRepository.FindAsync(c => c.Id == Guid.Parse("9ee0a9e5-3864-44a7-8744-635bba2959bd"));

            result.ShouldBeNull();
        }
    }
}