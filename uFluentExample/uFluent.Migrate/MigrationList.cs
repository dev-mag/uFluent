using System.Collections.Generic;
using uFluent.Migrate;
using uFluentExample.ufluent.Migrate.Migrations.UsefulLinksPicker;
using uFluentExample.uFluent.Migrate.Migrations;
using uFluentExample.uFluent.Migrate.Migrations.HomepagePicker;

namespace uFluentExample.uFluent.Migrate
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
                    new HomepagePickerCreateDataType()
                };
            }
        }
    }
}