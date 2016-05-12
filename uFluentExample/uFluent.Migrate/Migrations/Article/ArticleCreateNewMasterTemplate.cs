using uFluent;
using uFluent.Migrate;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class ArticleCreateNewMasterTemplate : IUmbracoMigration
    {
        public void Migrate()
        {
            var template = Template.Create("NewArticleTemplate", "New Article Template")
                .SetMasterTemplate(Template.Get("article"));

            template.Save();
        }
    }
}