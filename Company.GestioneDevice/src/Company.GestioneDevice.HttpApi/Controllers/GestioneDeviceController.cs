using Company.GestioneDevice.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Company.GestioneDevice.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class GestioneDeviceController : AbpControllerBase
{
    protected GestioneDeviceController()
    {
        LocalizationResource = typeof(GestioneDeviceResource);
    }
}
