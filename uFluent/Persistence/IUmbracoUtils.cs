using uFluent.Validation;
using Umbraco.Core.Persistence;
using Umbraco.Core.Services;

namespace uFluent.Persistence
{
    internal interface IUmbracoUtils
    {
        IUmbracoDatabaseAdaptor UmbracoDatabase { get; }

        IFileService FileService { get; }

        IContentTypeService ContentTypeService { get; }

        IDataTypeService DataTypeService { get; }

        IAliasValidator AliasValidator { get; }
    }
}
