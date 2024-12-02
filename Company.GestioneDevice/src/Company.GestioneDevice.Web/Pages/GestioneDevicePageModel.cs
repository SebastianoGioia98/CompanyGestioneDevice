using Company.GestioneDevice.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Company.GestioneDevice.Web.Pages;

public abstract class GestioneDevicePageModel : AbpPageModel
{
    protected GestioneDevicePageModel()
    {
        LocalizationResourceType = typeof(GestioneDeviceResource);
    }
}
