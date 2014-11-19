using System;
using System.Linq;
using log4net;
using uFluent.Dto;
using umbraco.cms.businesslogic.macro;
using Umbraco.Core.Persistence;
using uFluent.Persistence;

namespace uFluent
{
    public class Macro
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Macro));

        private readonly MacroDto _macroDto;

        private IUmbracoDatabaseAdaptor UmbracoDatabase { get; set; }

        private Macro(IUmbracoDatabaseAdaptor umbracoDatabase)
        {
            this.UmbracoDatabase = umbracoDatabase;
        }

        internal Macro(string alias,
            string name,
            IUmbracoDatabaseAdaptor umbracoDatabase)
            : this(umbracoDatabase)
        {
            _macroDto = new MacroDto
            {
                Alias = alias,
                Name = name
            };
        }

        internal Macro(MacroDto macroDto,
            IUmbracoDatabaseAdaptor umbracoDatabase)
            : this(umbracoDatabase)
        {
            _macroDto = macroDto;
        }

        public void Save()
        {
            this.UmbracoDatabase.Save(_macroDto);

            FireAfterSave();
        }

        public void Delete()
        {
            Log.DebugFormat("Deleting Macro {0}", _macroDto.Alias);

            this.UmbracoDatabase.Delete<MacroPropertyDto>("WHERE macro = @macroId", new { macroId = _macroDto.Id });
            this.UmbracoDatabase.Delete(_macroDto);

            FireAfterDelete();
        }

        public Macro DeleteProperty(string alias)
        {
            Log.DebugFormat("Deleting Macro property with alias {0}", alias);

            var property = this.UmbracoDatabase.SingleOrDefault<MacroPropertyDto>(
                "WHERE macroPropertyAlias = @alias", new {alias = alias});

            if (property == null)
            {
                throw new FluentException(
                    string.Format("Cannot delete property with alias {0} from {1} as it does not exist", 
                    alias, _macroDto.Alias));
            }

            this.UmbracoDatabase.Delete<MacroPropertyDto>("WHERE macroPropertyAlias = @alias", new { alias = alias });


            FireAfterDelete();

            return this;
        }

        public Macro SetPropertySortOrder(string alias, int sortOrder)
        {
            Log.DebugFormat("Moving Macro property with alias {0} to {1}", alias, sortOrder);

            var property = GetProperty(alias);

            property.SortOrder = (byte)sortOrder;

            this.UmbracoDatabase.Update(property);

            FireAfterDelete();

            return this;
        }

        public Macro RenameProperty(string alias, string newName)
        {
            Log.DebugFormat("Renaming Macro property with alias {0} to {1}", alias, newName);

            var property = GetProperty(alias);
            property.Name = newName;

            this.UmbracoDatabase.Update(property);

            FireAfterDelete();

            return this;
        }

        public Macro AddProperty(string alias, string name, string type, int sortOrder = 0)
        {
            Log.DebugFormat("Adding property to Macro of type {0} with alias `{1}` and name `{2}`, ", type, alias, name);

            var propertyTypes = MacroPropertyType.GetAll;
            var propertyType = propertyTypes.Single(x => x.Alias.Equals(type, StringComparison.InvariantCulture));

            var propertyDto = new MacroPropertyDto
            {
                Alias = alias,
                Name = name,
                Macro = _macroDto.Id,
                SortOrder = (byte) sortOrder,
                Type = (short) propertyType.Id
            };

            this.UmbracoDatabase.Save(propertyDto);

            FireAfterSave();

            return this;
        }

        public Macro SetScriptingFile(string scriptingFile)
        {
            Log.DebugFormat("Setting Macro scripting file to `{0}`", scriptingFile);

            _macroDto.Python = scriptingFile;
            return this;
        }

        public Macro SetCacheByPage(bool cacheByPage)
        {
            Log.DebugFormat("Setting Macro cache by page to `{0}`", cacheByPage);

            _macroDto.CacheByPage = cacheByPage;
            return this;
        }

        public Macro SetRefreshRate(int refreshRate)
        {
            Log.DebugFormat("Setting Macro RefreshRate to `{0}`", refreshRate);

            _macroDto.RefreshRate = refreshRate;
            return this;
        }

        public Macro SetCachePersonalized(bool cachedPersonalized)
        {
            Log.DebugFormat("Setting Macro CachePersonalized to `{0}`", cachedPersonalized);

            _macroDto.CachePersonalized = cachedPersonalized;
            return this;
        }

        public Macro SetName(string name)
        {
            Log.DebugFormat("Setting Macro name to `{0}`", name);

            _macroDto.Name = name;
            return this;
        }

        public Macro SetUseInEditor(bool useInEditor)
        {
            Log.DebugFormat("Setting Macro UseInEditor to `{0}`", useInEditor);

            _macroDto.UseInEditor = useInEditor;
            return this;
        }

        public Macro SetXsltPath(string xsltPath)
        {
            Log.DebugFormat("Setting Macro Xslt to `{0}`", xsltPath);

            _macroDto.Xslt = xsltPath;
            return this;
        }

        private void FireAfterSave()
        {
            // todo: version 2?
        }

        private void FireAfterDelete()
        {
            // todo: version 2?
        }

        private static FluentMacroService FluentMacroService
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

            internal static readonly FluentMacroService Instance = new FluentMacroService(UmbracoUtils.Instance);
        }

        private static MacroPropertyDto GetProperty(string alias)
        {
            return FluentMacroService.GetProperty(alias);
        }

        public static Macro Get(string alias)
        {
            return FluentMacroService.Get(alias);
        }

        public static Macro Create(string alias, string name)
        {
            return FluentMacroService.Create(alias, name);
        }
    }

}