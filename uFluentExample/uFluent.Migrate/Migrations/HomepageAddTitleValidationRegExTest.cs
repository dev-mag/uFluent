using uFluent;
using uFluent.Migrate;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class HomepageAddTitleValidationRegExTest : IUmbracoMigration
    {
        public void Migrate()
        {
            DocumentType.Get("Homepage").SetPropertyValidaton("title", @"^(BOB)$").Save();
        }
    }
}