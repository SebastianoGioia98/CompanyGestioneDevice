using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Company.GestioneDevice.Pages;

[Collection(GestioneDeviceTestConsts.CollectionDefinitionName)]
public class Index_Tests : GestioneDeviceWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
