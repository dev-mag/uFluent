using Umbraco.Core.Models;

namespace uFluent.Persistence
{
    internal class FluentLanguageService
    {
        internal void InsertLanguage(string isoCode)
        {
            Umbraco.Core.ApplicationContext.Current.Services.LocalizationService.Save(new Language(isoCode));
        }

        internal void RemoveLanguage(string isoCode)
        {
            Umbraco.Core.ApplicationContext.Current.Services.LocalizationService.Delete(new Language(isoCode));
        }

        internal bool LanguageExists(string isoCode)
        {
            return Umbraco.Core.ApplicationContext.Current.Services.LocalizationService.GetLanguageByIsoCode(isoCode) != null;
        }
    }
}
