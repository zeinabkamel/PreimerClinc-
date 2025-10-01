using Volo.Abp.Application.Services;
using PreimerClinc.Localization;

namespace PreimerClinc.Services;

/* Inherit your application services from this class. */
public abstract class PreimerClincAppService : ApplicationService
{
    protected PreimerClincAppService()
    {
        LocalizationResource = typeof(PreimerClincResource);
    }
}