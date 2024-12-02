using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using Company.GestioneDevice.Localization;

namespace Company.GestioneDevice.Web;

[Dependency(ReplaceServices = true)]
public class GestioneDeviceBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<GestioneDeviceResource> _localizer;

    public GestioneDeviceBrandingProvider(IStringLocalizer<GestioneDeviceResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
