using System.Collections.Generic;
using Umbraco.Core.Models;

namespace uFluent
{
    public interface IDataType
    {
        IDataType SetName(string name);
        IDataType SetControlId(string propertyEditor);
        IDataType SetDataTypeDatabaseType(DataTypeDatabaseType dataTypeDatabaseType);
        IDataType AddPreValue(string value, int sortOrder = 0, string alias = "");
        IDataType AddPreValueJson(object value, int sortOrder = 0, string alias = "");
        IEnumerable<string> GetDataTypePreValues();
        IDataType DeleteAllPreValues();
        IDataType Save();
        void Delete();
    }
}