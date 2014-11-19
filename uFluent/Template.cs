using System;
using System.IO;
using System.Reflection;
using System.Web;
using log4net;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using UmbracoTemplateDef = Umbraco.Core.Models.Template;
using uFluent.Validation;
using Umbraco.Core;
using uFluent.Persistence;

namespace uFluent
{
    public class Template
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Template));

        internal readonly ITemplate UmbracoTemplate;

        private readonly IFileService FileService;

        public string Alias
        {
            get { return UmbracoTemplate.Alias; }
        }

        public Template(ITemplate umbracoTemplate,
            IFileService fileService)
        {
            UmbracoTemplate = umbracoTemplate;
            this.FileService = fileService;
        }

        public Template SetMasterTemplate(Template masterTemplate)
        {

            if (masterTemplate == null)
            {
                throw new ArgumentNullException("masterTemplate");
            }

            var type = typeof(UmbracoTemplateDef);
            var alias = type.GetProperty("MasterTemplateAlias", BindingFlags.NonPublic | BindingFlags.Instance);
            alias.SetValue(UmbracoTemplate, masterTemplate.Alias, null);

            var id = type.GetProperty("MasterTemplateId", BindingFlags.NonPublic | BindingFlags.Instance);
            var templateId = masterTemplate.UmbracoTemplate.Id;
            id.SetValue(UmbracoTemplate, new Lazy<int>(() => templateId), null);

            return this;
        }

        public Template ClearMasterTemplate()
        {
            var type = typeof(UmbracoTemplateDef);
            var alias = type.GetProperty("MasterTemplateAlias", BindingFlags.NonPublic | BindingFlags.Instance);
            alias.SetValue(UmbracoTemplate, null, null);

            var id = type.GetProperty("MasterTemplateId", BindingFlags.NonPublic | BindingFlags.Instance);
            id.SetValue(UmbracoTemplate, null, null);

            return this;
        }

        public void Save()
        {
            this.FileService.SaveTemplate(UmbracoTemplate);
        }

        public void Delete()
        {
            this.FileService.DeleteTemplate(UmbracoTemplate.Alias, 0);
        }

        private static FluentTemplateService TemplateService
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

            internal static readonly FluentTemplateService Instance = new FluentTemplateService(UmbracoUtils.Instance);
        }

        public static Template Create(string alias, string name)
        {
            return TemplateService.Create(alias, name);
        }

        public static Template Get(string alias)
        {
            return TemplateService.Get(alias);
        }
    }
}