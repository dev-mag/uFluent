using Umbraco.Core.Models;
using System.Linq;

namespace uFluent.Persistence
{
    internal class FluentDataTypeService
    {
        private IUmbracoUtils UmbracoUtils { get; set; }

        public FluentDataTypeService(IUmbracoUtils umbracoUtils)
        {
            UmbracoUtils = umbracoUtils;
        }

        public DataType Create(string name, string propertyEditor, DataTypeDatabaseType databaseType = DataTypeDatabaseType.Ntext)
        {
            var allDataTypeDefinitions = UmbracoUtils.DataTypeService.GetAllDataTypeDefinitions();

            if (allDataTypeDefinitions.Any(x => x.Name == name))
            {
                throw new FluentException(string.Format("Cannot create Data Type Definition `{0}` as it already exists.", name));
            }

            var newDataTypeDefinition = new DataTypeDefinition(-1, propertyEditor) { Name = name, DatabaseType = databaseType };

            UmbracoUtils.DataTypeService.Save(newDataTypeDefinition);

            return new DataType(newDataTypeDefinition, UmbracoUtils.UmbracoDatabase, UmbracoUtils.DataTypeService);
        }

        public DataType Get(string name)
        {
            var dataTypeDefinition = UmbracoUtils.DataTypeService.GetAllDataTypeDefinitions().FirstOrDefault(x => x.Name == name);

            if (dataTypeDefinition == null)
            {
                throw new FluentException(string.Format("The Data Type Definition `{0}` does not exist.", name));
            }

            return new DataType(dataTypeDefinition, UmbracoUtils.UmbracoDatabase, UmbracoUtils.DataTypeService);
        }
    }
}
