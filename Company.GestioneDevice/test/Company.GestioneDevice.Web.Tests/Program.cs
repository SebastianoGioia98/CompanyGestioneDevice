using Microsoft.AspNetCore.Builder;
using Company.GestioneDevice;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Company.GestioneDevice.Web.csproj"); 
await builder.RunAbpModuleAsync<GestioneDeviceWebTestModule>(applicationName: "Company.GestioneDevice.Web");

public partial class Program
{
}
