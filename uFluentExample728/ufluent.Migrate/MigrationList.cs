using System.Collections.Generic;
using uFluent.Migrate;
using uFluentExample728.ufluent.Migrate.Migrations.Article;
using uFluentExample728.ufluent.Migrate.Migrations.Category;
using uFluentExample728.ufluent.Migrate.Migrations.Homepage;
using uFluentExample728.ufluent.Migrate.Migrations.UsefulLinksPicker;
using uFluentExample728.uFluent.Migrate.Migrations;
using uFluentExample728.uFluent.Migrate.Migrations.HomepagePicker;

namespace uFluentExample728.ufluent.Migrate
{
    public class MigrationList : IMigrationList
    {
        public IEnumerable<IUmbracoMigration> Migrations
        {
            get
            {
                return new List<IUmbracoMigration>
                {
                    new HomepageCreateTemplateAndDocType(),
                    new CategoryCreateTemplateAndDocType(),
                    new ArticleCreateTemplateAndDocType(),
                    new UsefulLinksPickerCreateDataType(),
                    new UsefulLinksPickerAddHomepageCategoryArticleDocTypes(),
                    new DocumentTypeSetIcons(),
                    new UsefulLinksPickerRemoveCategoryArticleFromAllowedItems(),
                    new HomepagePickerCreateDataType(),
                    new UsefulLinksPickerChangeDbTypeToNvarchar(),
                    new UsefulLinksPickerChangeDbTypeToNtext(),
                    new ArticleTagsGroupCreateDataType()
                };
            }
        }
    }
}