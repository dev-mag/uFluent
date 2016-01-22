using uFluent;
using uFluent.Extensions.Tags;
using uFluent.Extensions.Tags.Enums;
using uFluent.Migrate;
using Umbraco.Core.Models;

namespace uFluentExample728.uFluent.Migrate.Migrations
{
    public class ArticleTagsGroupCreateDataType : IUmbracoMigration
    {
        public void Migrate()
        {
            DataType.Create("Article Tags", "Umbraco.Tags", DataTypeDatabaseType.Ntext)
                .SetTagDataTypePreValues("Article Tags", StorageType.Csv).Save();
        }
    }
}