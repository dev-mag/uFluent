using uFluent;
using uFluent.Migrate;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class HomepageRemoveTitleValidationRegEx : IUmbracoMigration
    {
        public void Migrate()
        {
            DocumentType.Get("Homepage").SetPropertyValidaton("title", string.Empty).Save();
        }
    }
}