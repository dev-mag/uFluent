using System;
using Umbraco.Core.Models;
namespace uFluent.Persistence
{
    internal class FluentDocumentTypeService
    {
        private IUmbracoUtils UmbracoUtils { get; set; }

        public FluentDocumentTypeService(IUmbracoUtils umbracoUtils)
        {
            UmbracoUtils = umbracoUtils;
        }

        /// <summary>
        /// Retrieve a document type by its alias.
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public DocumentType Get(string alias)
        {
            var existingContentType = UmbracoUtils.ContentTypeService.GetContentType(alias);

            if (existingContentType == null)
            {
                throw new InvalidOperationException(string.Format("Cannot get DocumentType `{0}` as it does not exist", alias));
            }

            var documentType = new DocumentType(UmbracoUtils.ContentTypeService, UmbracoUtils.DataTypeService)
            {
                UmbracoContentType = existingContentType
            };
            return documentType;
        }

        /// <summary>
        /// Create a new document type and save it to the database.
        /// </summary>
        /// <param name="alias">Alias. Cannot contain spaces or exotic punctuation.</param>
        /// <param name="name">Friendly document type name. Visible to content editors.</param>
        /// <returns></returns>
        public DocumentType Create(string alias, string name)
        {
            var existingContentType = UmbracoUtils.ContentTypeService.GetContentType(alias);

            if (existingContentType != null)
            {
                throw new InvalidOperationException(string.Format("Cannot create DocumentType `{0}` as it already exists", alias));
            }

            var newDocumentType = new DocumentType(UmbracoUtils.ContentTypeService, UmbracoUtils.DataTypeService);
            newDocumentType.UmbracoContentType = new ContentType(-1) { Name = name, Alias = alias, Icon = "folder.gif" };

            newDocumentType.Save();

            return newDocumentType;
        }
    }
}
