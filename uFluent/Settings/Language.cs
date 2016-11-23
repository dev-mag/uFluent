using uFluent.Persistence;

namespace uFluent.Settings
{
    /// <summary>
    /// Language class
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Adds the new language to umbraco.
        /// </summary>
        /// <param name="isoCode">The iso code.</param>
        /// <param name="cultureName">Name of the culture.</param>
        public static void AddNewLanguageToUmbraco(string isoCode, string cultureName)
        {
            FluentLanguageServiceInstance.InsertLanguage(isoCode);
        }

        /// <summary>
        /// Removes the language from umbraco.
        /// </summary>
        /// <param name="isoCode">The iso code.</param>
        public static void RemoveLanguageFromUmbraco(string isoCode)
        {
            FluentLanguageServiceInstance.RemoveLanguage(isoCode);
        }

        /// <summary>
        /// Languages that exist in umbraco.
        /// </summary>
        /// <param name="isoCode">The iso code.</param>
        /// <returns></returns>
        public static bool LanguageExistsInUmbraco(string isoCode)
        {
            return FluentLanguageServiceInstance.LanguageExists(isoCode);
        }

        private static FluentLanguageService FluentLanguageServiceInstance => NestedLazyLoader.Instance;

        /// <summary>
        /// Nested lazy loader to instantiate upon initial allocation
        /// </summary>
        private class NestedLazyLoader
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static NestedLazyLoader()
            {
            }

            internal static readonly FluentLanguageService Instance = new FluentLanguageService();
        }
    }
}
