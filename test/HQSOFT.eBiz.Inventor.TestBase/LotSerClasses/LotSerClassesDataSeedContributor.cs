using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HQSOFT.eBiz.Inventor.LotSerClasses;

namespace HQSOFT.eBiz.Inventor.LotSerClasses
{
    public class LotSerClassesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ILotSerClassRepository _lotSerClassRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public LotSerClassesDataSeedContributor(ILotSerClassRepository lotSerClassRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _lotSerClassRepository = lotSerClassRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _lotSerClassRepository.InsertAsync(new LotSerClass
            (
                id: Guid.Parse("9ee0a9e5-3864-44a7-8744-635bba2959bd"),
                classID: "39b72eb4f1944c1ba2e5ce13b9570e4eb03de09cf1c34f5c91f68a07a1fa27e21c6bf67ceb914051bf0e55544",
                description: "d8209daf500e4dfbbcb2f008b9c561b7bfa880cc10d6422981644a8ad59eeb41c1ddca37c8",
                trackingMethod: default,
                trackExpriationDate: true,
                requiredforDropShip: true,
                assignMethod: default,
                issueMethod: default,
                shareAutoIncremenitalValueBetwenAllClassItems: true,
                autoIncremetalValue: 1894315135,
                autoGenerateNextNumber: true,
                maxAutoNumbers: 384691086
            ));

            await _lotSerClassRepository.InsertAsync(new LotSerClass
            (
                id: Guid.Parse("98af4718-e00b-47c7-b6a1-a32f5132e370"),
                classID: "a9e4adb624ef42",
                description: "1df32d5868ad43308dd0a619732331e601d8afc9772040e3afc",
                trackingMethod: default,
                trackExpriationDate: true,
                requiredforDropShip: true,
                assignMethod: default,
                issueMethod: default,
                shareAutoIncremenitalValueBetwenAllClassItems: true,
                autoIncremetalValue: 1979711955,
                autoGenerateNextNumber: true,
                maxAutoNumbers: 1109073748
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}