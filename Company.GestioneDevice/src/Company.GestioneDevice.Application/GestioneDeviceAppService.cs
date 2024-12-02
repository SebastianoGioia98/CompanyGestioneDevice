using Company.GestioneDevice.Localization;
using Volo.Abp.Application.Services;

namespace Company.GestioneDevice;

/* Inherit your application services from this class.
 */
public abstract class GestioneDeviceAppService : ApplicationService
{
    protected GestioneDeviceAppService()
    {
        LocalizationResource = typeof(GestioneDeviceResource);
    }
}
