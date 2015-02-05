using ClientDependency.Core;
using Lucene.Net.Search;
using uFluent;
using uFluent.Consts;
using uFluent.Extensions.Tags;
using uFluent.Migrate;
using Umbraco.Core.Models;

namespace umbraco621Example.uFluent.Migrate.Migrations
{
    public class PressReleaseTagsDataType : IUmbracoMigration
    {
        public void Migrate()
        {
            var prTags = DataType.Create("Press Release Tags", PropertyEditor.Tags, DataTypeDatabaseType.Ntext).Save();
            prTags.SetTagGroup("Press Release Category").Save();
        }
    }
}