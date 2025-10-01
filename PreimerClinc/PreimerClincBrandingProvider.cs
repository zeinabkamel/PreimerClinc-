using Microsoft.Extensions.Localization;
using PreimerClinc.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace PreimerClinc;

[Dependency(ReplaceServices = true)]
public class PreimerClincBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<PreimerClincResource> _localizer;

    public PreimerClincBrandingProvider(IStringLocalizer<PreimerClincResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}