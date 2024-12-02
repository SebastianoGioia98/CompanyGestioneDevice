using Company.GestioneDevice.Samples;
using Xunit;

namespace Company.GestioneDevice.EntityFrameworkCore.Applications;

[Collection(GestioneDeviceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<GestioneDeviceEntityFrameworkCoreTestModule>
{

}
