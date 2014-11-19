using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using uFluent.Dto;
using uFluent.Persistence;
using Umbraco.Core.Persistence;

namespace uFluent
{
    public class DataType
    {
        private IDataTypeDefinition DataTypeDefinition { get; set; }

        private IUmbracoDatabaseAdaptor UmbracoDatabase { get; set; }

        private IDataTypeService DataTypeService { get; set; }

        internal DataType(IDataTypeDefinition dataTypeDefinition,
            IUmbracoDatabaseAdaptor umbracoDatabase,
            IDataTypeService dataTypeService)
        {
            DataTypeDefinition = dataTypeDefinition;
            this.UmbracoDatabase = umbracoDatabase;
            this.DataTypeService = dataTypeService;
        }

        public DataType SetName(string name)
        {
            DataTypeDefinition.Name = name;
            return this;
        }

        public DataType SetControlId(string propertyEditor)
        {
            var property = typeof(DataTypeDefinition).GetProperty("ControlId");
            property.SetValue(DataTypeDefinition, new Guid(propertyEditor), BindingFlags.NonPublic | BindingFlags.Instance, null, null, null);

            return this;
        }

        public DataType AddPreValue(string value, int sortOrder = 0, string alias = "")
        {
            var dtpv = new DataTypePreValueDto
            {
                Alias = alias, 
                Value = value, 
                DataTypeNodeId = DataTypeDefinition.Id, 
                SortOrder = sortOrder
            };

            this.UmbracoDatabase.Insert(dtpv);

            return this;
        }
        
        public DataType AddPreValueJson(object value, int sortOrder = 0, string alias = "")
        {
            var dtpv = new DataTypePreValueDto
            {
                Alias = alias,
                Value = JsonConvert.SerializeObject(value),
                DataTypeNodeId = DataTypeDefinition.Id,
                SortOrder = sortOrder
            };

            this.UmbracoDatabase.Insert(dtpv);

            return this;
        }

        public DataType DeleteAllPreValues()
        {
            this.UmbracoDatabase.Delete<DataTypePreValueDto>("WHERE datatypeNodeId = @NodeId", new { NodeId = DataTypeDefinition.Id });
            return this;
        }

        public DataType Save()
        {
            this.DataTypeService.Save(DataTypeDefinition);
            return this;
        }

        public void Delete()
        {
            var dataTypeDefinition = this.DataTypeService.GetAllDataTypeDefinitions().FirstOrDefault(x => x.Name == DataTypeDefinition.Name);

            if (dataTypeDefinition == null)
            {
                throw new FluentException(string.Format("The Data Type Definition `{0}` does not exist.", DataTypeDefinition.Name));
            }

            this.UmbracoDatabase.Delete<DataTypePreValueDto>("WHERE datatypeNodeId = @NodeId", new { NodeId = DataTypeDefinition.Id });

            this.DataTypeService.Delete(dataTypeDefinition);
        }

        private static FluentDataTypeService FluentDataTypeService
        {
            get
            {
                return NestedLazyLoader.Instance;
            }
        }

        private class NestedLazyLoader
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static NestedLazyLoader()
            {
            }

            internal static readonly FluentDataTypeService Instance = new FluentDataTypeService(UmbracoUtils.Instance);
        }

        public static DataType Create(string name, string propertyEditor, DataTypeDatabaseType databaseType = DataTypeDatabaseType.Ntext)
        {
            return FluentDataTypeService.Create(name, propertyEditor, databaseType);
        }

        public static DataType Get(string name)
        {
            return FluentDataTypeService.Get(name);
        }
    }
}