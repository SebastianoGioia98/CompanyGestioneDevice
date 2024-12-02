using Company.GestioneDevice.Samples;
using Xunit;

namespace Company.GestioneDevice.EntityFrameworkCore.Domains;

[Collection(GestioneDeviceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<GestioneDeviceEntityFrameworkCoreTestModule>
{

}
