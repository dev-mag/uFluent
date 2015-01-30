using uFluent.Validation;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace uFluent.Persistence
{
    internal class UmbracoUtils : IUmbracoUtils
    {
        public IUmbracoDatabaseAdaptor UmbracoDatabase { get; private set; }

        public IFileService FileService { get; private set; }

        public IContentTypeService ContentTypeService { get; private set; }

        public IDataTypeService DataTypeService { get; private set; }

        public IAliasValidator AliasValidator { get; private set; }

        public static UmbracoUtils Instance
        {
            get
            {
                return NestedLazyLoader.instance;
            }
        }

        private class NestedLazyLoader
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static NestedLazyLoader()
            {
            }

            internal static readonly UmbracoUtils instance = new UmbracoUtils();
        }

        private UmbracoUtils()
        {
            if (ApplicationContext.Current == null)
            {
                throw new FluentException("Current Umbraco application context is not loaded.");
            }

            FileService = ApplicationContext.Current.Services.FileService;

            ContentTypeService = ApplicationContext.Current.Services.ContentTypeService;

            DataTypeService = ApplicationContext.Current.Services.DataTypeService;

            UmbracoDatabase = new UmbracoDatabaseAdaptor();

            AliasValidator = new AliasValidator();
        }
    }
}
