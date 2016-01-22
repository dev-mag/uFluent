using uFluent;
using uFluent.Consts;
using uFluent.Extensions.Tags;
using uFluent.Extensions.Tags.Enums;
using uFluent.Migrate;
using Umbraco.Core.Models;

namespace uFluentExample728.ufluent.Migrate.Migrations
{
    public class SubCategoryCreateDataType : IUmbracoMigration
    {
        public void Migrate()
        {
            var tagGroup = DataType.Create("Article Sub Category", PropertyEditor.Tags, DataTypeDatabaseType.Ntext).Save();
            tagGroup.SetTagDataTypePreValues("Article Sub Category", StorageType.Csv).Save();
        }
    }
}