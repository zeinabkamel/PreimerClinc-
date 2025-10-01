using Microsoft.AspNetCore.Builder;
using PreimerClinc;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("PreimerClinc.csproj");
await builder.RunAbpModuleAsync<PreimerClincTestModule>(applicationName: "PreimerClinc");
namespace PreimerClinc
{
    public partial class Program
    {
    }
}
