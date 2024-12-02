using Xunit;

namespace Company.GestioneDevice.EntityFrameworkCore;

[CollectionDefinition(GestioneDeviceTestConsts.CollectionDefinitionName)]
public class GestioneDeviceEntityFrameworkCoreCollection : ICollectionFixture<GestioneDeviceEntityFrameworkCoreFixture>
{

}
