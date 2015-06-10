using uFluent;
using uFluent.Migrate;

namespace umbraco621Example.uFluent.Migrate.Migrations
{
    public class HomepageSetTitleValidationProperty : IUmbracoMigration
    {
        public void Migrate()
        {
            DocumentType.Get("Homepage")
                .SetPropertyValidation("title", "^[a-z][A-Z]$")
                .Save();
        }
    }
}