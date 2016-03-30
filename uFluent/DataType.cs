using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using uFluent.Dto;
using uFluent.Persistence;

namespace uFluent
{
    public class DataType : IDataType
    {
        private IDataTypeDefinition DataTypeDefinition { get; set; }

        private IUmbracoDatabaseAdaptor UmbracoDatabase { get; set; }

        private IDataTypeService DataTypeService { get; set; }

        internal DataType(IDataTypeDefinition dataTypeDefinition,
            IUmbracoDatabaseAdaptor umbracoDatabase,
            IDataTypeService dataTypeService)
        {
            DataTypeDefinition = dataTypeDefinition;
            UmbracoDatabase = umbracoDatabase;
            DataTypeService = dataTypeService;
        }

        public IDataType SetName(string name)
        {
            DataTypeDefinition.Name = name;
            return this;
        }

        public IDataType SetPropertyEditor(string propertyEditorAlias)
        {
            DataTypeDefinition.PropertyEditorAlias = propertyEditorAlias;
            return this;
        }

        public IDataType SetControlId(string propertyEditor)
        {
            var property = typeof(DataTypeDefinition).GetProperty("ControlId");
            property.SetValue(DataTypeDefinition, propertyEditor, BindingFlags.NonPublic | BindingFlags.Instance, null, null, null);

            return this;
        }

        public IDataType SetDataTypeDatabaseType(DataTypeDatabaseType dataTypeDatabaseType)
        {
            DataTypeDefinition.DatabaseType = dataTypeDatabaseType;
            return this;
        }

        public IDataType AddPreValue(string value, int sortOrder = 0, string alias = "")
        {
            var dtpv = new DataTypePreValueDto
            {
                Alias = alias, 
                Value = value, 
                DataTypeNodeId = DataTypeDefinition.Id, 
                SortOrder = sortOrder
            };

            UmbracoDatabase.Insert(dtpv);

            return this;
        }

        public IDataType AddPreValueJson(object value, int sortOrder = 0, string alias = "")
        {
            var dtpv = new DataTypePreValueDto
            {
                Alias = alias,
                Value = JsonConvert.SerializeObject(value),
                DataTypeNodeId = DataTypeDefinition.Id,
                SortOrder = sortOrder
            };

            UmbracoDatabase.Insert(dtpv);

            return this;
        }

        public IEnumerable<string> GetDataTypePreValues()
        {
            return this.DataTypeService.GetPreValuesByDataTypeId(DataTypeDefinition.Id);
        }

        public IDataType DeleteAllPreValues()
        {
            UmbracoDatabase.Delete<DataTypePreValueDto>("WHERE datatypeNodeId = @NodeId", new { NodeId = DataTypeDefinition.Id });
            return this;
        }

        public IDataType Save()
        {
            DataTypeService.Save(DataTypeDefinition);
            return this;
        }

        public void Delete()
        {
            var dataTypeDefinition = DataTypeService.GetAllDataTypeDefinitions().FirstOrDefault(x => x.Name == DataTypeDefinition.Name);

            if (dataTypeDefinition == null)
            {
                throw new FluentException(string.Format("The Data Type Definition `{0}` does not exist.", DataTypeDefinition.Name));
            }

            UmbracoDatabase.Delete<DataTypePreValueDto>("WHERE datatypeNodeId = @NodeId", new { NodeId = DataTypeDefinition.Id });

            DataTypeService.Delete(dataTypeDefinition);
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

        public static IDataType Create(string name, string propertyEditor, DataTypeDatabaseType databaseType = DataTypeDatabaseType.Ntext)
        {
            return FluentDataTypeService.Create(name, propertyEditor, databaseType);
        }

        public static IDataType Get(string name)
        {
            return FluentDataTypeService.Get(name);
        }

    }
}