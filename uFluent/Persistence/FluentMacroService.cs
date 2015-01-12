using log4net;
using uFluent.Dto;

namespace uFluent.Persistence
{
    internal class FluentMacroService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(FluentMacroService));

        private IUmbracoUtils UmbracoUtils { get; set; }

        public FluentMacroService(IUmbracoUtils umbracoUtils)
        {
            UmbracoUtils = umbracoUtils;
        }

        public MacroPropertyDto GetProperty(string alias)
        {
            var property = UmbracoUtils.UmbracoDatabase.SingleOrDefault<MacroPropertyDto>(
                "WHERE macroPropertyAlias = @alias", new { alias = alias });
            return property;
        }

        public Macro Get(string alias)
        {
            Log.DebugFormat("Retrieving Macro with alias `{0}`", alias);

            var existing = UmbracoUtils.UmbracoDatabase.SingleOrDefault<MacroDto>("WHERE macroAlias = @alias", new { alias = alias });
            if (existing == null)
            {
                throw new FluentException(string.Format("Macro named {0} does not exist", alias));
            }

            return new Macro(existing, UmbracoUtils.UmbracoDatabase);

        }

        public Macro Create(string alias, string name)
        {
            var existing = UmbracoUtils.UmbracoDatabase.SingleOrDefault<MacroDto>("WHERE macroAlias = @alias", new { alias = alias });
            if (existing != null)
            {
                throw new FluentException(string.Format("Cannot create macro named {0} as it already exists", alias));
            }

            var macro = new Macro(alias, name, UmbracoUtils.UmbracoDatabase);
            macro.Save();

            return macro;
        }
    }
}
