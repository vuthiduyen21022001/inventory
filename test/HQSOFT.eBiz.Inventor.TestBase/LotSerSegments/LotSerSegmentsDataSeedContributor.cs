using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HQSOFT.eBiz.Inventor.LotSerSegments;

namespace HQSOFT.eBiz.Inventor.LotSerSegments
{
    public class LotSerSegmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ILotSerSegmentRepository _lotSerSegmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public LotSerSegmentsDataSeedContributor(ILotSerSegmentRepository lotSerSegmentRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _lotSerSegmentRepository = lotSerSegmentRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _lotSerSegmentRepository.InsertAsync(new LotSerSegment
            (
                id: Guid.Parse("af7233a0-c0cd-4283-bd48-88f99eebbefe"),
                segmentID: 632252695,
                asgmentType: default,
                value: "f14058c43e204f328f62ac7f1fb638cb7313444"
            ));

            await _lotSerSegmentRepository.InsertAsync(new LotSerSegment
            (
                id: Guid.Parse("92735467-b221-412a-b56a-b23e7d149ce3"),
                segmentID: 523884098,
                asgmentType: default,
                value: "a930a0d0130846c586839ff78d05c887f6a295c7f8f8488c9f3af800e"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}