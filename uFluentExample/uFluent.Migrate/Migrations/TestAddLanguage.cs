using uFluent.Migrate;
using Language = uFluent.Settings.Language;
using LanguageConsts = uFluent.Consts;

namespace uFluentExample.ufluent.Migrate.Migrations
{
    public class TestAddLanguage : IUmbracoMigration
    {
        public void Migrate()
        {
            Language.AddNewLanguageToUmbraco(LanguageConsts.Languages.French, "French");
        }
    }
}