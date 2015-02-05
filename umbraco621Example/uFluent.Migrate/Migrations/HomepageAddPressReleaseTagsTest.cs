using uFluent;
using uFluent.Migrate;

namespace umbraco621Example.uFluent.Migrate.Migrations
{
    public class HomepageAddPressReleaseTagsTest : IUmbracoMigration
    {
        public void Migrate()
        {
            DocumentType.Get("Homepage")
                .AddProperty("prTags", "PR Tags", "Press Release Tags", "Content", false)
                .Save();
        }
    }
}