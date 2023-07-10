using Xunit;
using HQSOFT.eBiz.Inventor.Samples;

namespace HQSOFT.eBiz.Inventor.MongoDB.Samples;

[Collection(MongoTestCollection.Name)]
public class SampleRepository_Tests : SampleRepository_Tests<InventorMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
