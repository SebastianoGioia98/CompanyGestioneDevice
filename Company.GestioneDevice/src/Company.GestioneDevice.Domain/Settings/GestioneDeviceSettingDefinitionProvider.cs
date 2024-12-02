using Volo.Abp.Settings;

namespace Company.GestioneDevice.Settings;

public class GestioneDeviceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(GestioneDeviceSettings.MySetting1));
    }
}
