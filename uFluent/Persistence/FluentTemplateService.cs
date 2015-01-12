using log4net;
using System;
using System.IO;
using System.Web;
using UmbracoTemplateDef = Umbraco.Core.Models.Template;

namespace uFluent.Persistence
{
    internal class FluentTemplateService
    {
        private IUmbracoUtils UmbracoUtils { get; set; }

        private static readonly ILog Log = LogManager.GetLogger(typeof(FluentTemplateService));

        internal FluentTemplateService(IUmbracoUtils umbracoUtils)
        {
            if (umbracoUtils == null)
            {
                throw new ArgumentNullException("umbracoUtils");
            }

            UmbracoUtils = umbracoUtils;
        }

        public Template Create(string alias, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Template name must be specified", "name");
            }

            UmbracoUtils.AliasValidator.Validate(alias);

            if (UmbracoUtils.FileService.GetTemplate(alias) != null)
            {
                throw new FluentException(string.Format("Cannot create template `{0}` as it already exists", alias));
            }

            var template = new UmbracoTemplateDef(String.Format("~/Views/{0}.cshtml", alias), name, alias);

            var fileContents = string.Empty;

            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Views", string.Format("{0}.cshtml", alias));

            if (File.Exists(filePath))
            {
                fileContents = File.ReadAllText(filePath);
            }
            else
            {
                Log.Info(string.Format("Created blank template as the file {0} could not be found.", filePath));
            }

            template.Content = fileContents;

            UmbracoUtils.FileService.SaveTemplate(template);

            return new Template(template, UmbracoUtils.FileService);
        }

        public Template Get(string alias)
        {
            UmbracoUtils.AliasValidator.Validate(alias);

            var template = UmbracoUtils.FileService.GetTemplate(alias);
            if (template == null)
            {
                throw new FluentException(string.Format("Cannot get template `{0}` as it does not exist", alias));
            }

            return new Template(template, UmbracoUtils.FileService);
        }
    }
}
