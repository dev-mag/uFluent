using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using uFluent.Annotations;
using uFluent.Persistence;

namespace uFluent
{
    /// <summary>
    /// Encapsulates management of document types and their associated properties.
    /// </summary>
    public class DocumentType
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (DocumentType));

        internal IContentType UmbracoContentType;

        private IContentTypeService ContentTypeService { get; set; }

        private IDataTypeService DataTypeService { get; set; }

        internal DocumentType(IContentTypeService contentTypeService,
            IDataTypeService dataTypeService)
        {
            this.ContentTypeService = contentTypeService;

            this.DataTypeService = dataTypeService;
        }

        /// <summary>
        /// Set the default template associated with this document type. The
        /// template is also aded to the list of allowed templates.
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public DocumentType SetDefaultTemplate(Template template)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

            UmbracoContentType.SetDefaultTemplate(template.UmbracoTemplate);

            AddAllowedTemplate(template);

            return this;
        }

        /// <summary>
        /// Add a template to the list of allowed templates for this document type.
        /// If this is only template allowed be sure to call <see cref="SetDefaultTemplate"/>.
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public DocumentType AddAllowedTemplate(Template template)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

            if (UmbracoContentType.AllowedTemplates.Contains(template.UmbracoTemplate))
            {
                return this;
            }

            var allowedTemplates = new List<ITemplate>(UmbracoContentType.AllowedTemplates) { template.UmbracoTemplate };
            UmbracoContentType.AllowedTemplates = allowedTemplates;

            return this;
        }

        /// <summary>
        /// Add a new property to this document type
        /// </summary>
        /// <param name="alias">Property alias, follow C# variable naming guidelines.</param>
        /// <param name="name">Name visible to content editors</param>
        /// <param name="dataTypeName">Data type to be used. <see cref="uMigrate.Consts.DataTypes"/></param>
        /// <param name="tabName"></param>
        /// <param name="mandatory"></param>
        /// <param name="description"></param>
        /// <param name="validationRegex"></param>
        /// <returns></returns>
        public DocumentType AddProperty(string alias, string name, string dataTypeName, string tabName, bool mandatory, string description = "", string validationRegex = "")
        {
            var dataTypeDefinition = GetDataTypeDefinition(dataTypeName);

            var propertyType = new PropertyType(dataTypeDefinition)
            {
                Alias = alias,
                Name = name,
                Mandatory = mandatory,
                Description = description,
                ValidationRegExp = validationRegex
            };

            if (String.IsNullOrEmpty(tabName)) {
                UmbracoContentType.AddPropertyType(propertyType);
            } else {
                UmbracoContentType.AddPropertyType(propertyType, tabName);
            }

            return this;
        }

        /// <summary>
        /// Change the data type of an existing property.
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="newDataTypeName"></param>
        /// <returns></returns>
        public DocumentType SetPropertyDataType(string alias, string newDataTypeName)
        {
            var propertyType = UmbracoContentType.PropertyTypes.SingleOrDefault(x => x.Alias.Equals(alias, StringComparison.OrdinalIgnoreCase));
            if (propertyType == null)
            {
                throw new FluentException(string.Format("Could not find property with alias `{0}`", alias));
            }

            var dataTypeDefinition = GetDataTypeDefinition(newDataTypeName);
            if (dataTypeDefinition == null)
            {
                throw new FluentException(string.Format("Could not find data type definition with alias `{0}`", newDataTypeName));
            }

            propertyType.DataTypeDefinitionId = dataTypeDefinition.Id;

            return this;
        }

        /// <summary>
        /// Set the mandatory flag on the specified property.
        /// </summary>
        /// <param name="propertyAlias">Property alias</param>
        /// <param name="mandatory">Mandatory flag</param>
        /// <returns></returns>
        public DocumentType SetPropertyMandatory(string propertyAlias, bool mandatory)
        {
            var property =
                UmbracoContentType.PropertyTypes.Single(
                    x => x.Alias.Equals(propertyAlias, StringComparison.InvariantCultureIgnoreCase));

            property.Mandatory = mandatory;

            return this;
        }

        /// <summary>
        /// Move a property onto a different tab.
        /// </summary>
        /// <param name="alias">Property alias</param>
        /// <param name="destinationTabName">Destination tab</param>
        /// <returns></returns>
        public DocumentType MoveProperty(string alias, string destinationTabName)
        {
            UmbracoContentType.MovePropertyType(alias, destinationTabName);
            return this;
        }

        /// <summary>
        /// Delete a property from this document type.
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public DocumentType DeleteProperty(string alias)
        {
            UmbracoContentType.RemovePropertyType(alias);
            return this;
        }

        /// <summary>
        /// Set the name property of this document type
        /// </summary>
        /// <param name="name">Name (mandatory)</param>
        /// <returns></returns>
        public DocumentType SetName(string name)
        {
            UmbracoContentType.Name = name;
            return this;
        }

        /// <summary>
        /// Set the icon of this document type, see /umbraco/icons/umbraco for a list
        /// of available icons.
        /// </summary>
        /// <param name="icon">File name of the icon.</param>
        /// <returns></returns>
        public DocumentType SetIcon(string icon)
        {
            UmbracoContentType.Icon = icon;
            return this;
        }

        /// <summary>
        /// Set the thumbnail image of this document type, see /umbraco/images/thumbnails for a list
        /// of available thumbnail images.
        /// </summary>
        /// <param name="thumbnail">File name of the thumbnail image.</param>
        /// <returns></returns>
        public DocumentType SetThumbnail(string thumbnail)
        {
            UmbracoContentType.Thumbnail = thumbnail;
            return this;
        }

        /// <summary>
        /// Set the description text.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public DocumentType SetDescription(string description)
        {
            UmbracoContentType.Description = description;
            return this;
        }

        /// <summary>
        /// Save changes to this document type, includes any changes to properties.
        /// </summary>
        /// <returns></returns>
        public DocumentType Save()
        {
            this.ContentTypeService.Save(UmbracoContentType);

            return this;
        }

        private IDataTypeDefinition GetDataTypeDefinition(string dataTypeName)
        {
            return this.DataTypeService.GetAllDataTypeDefinitions().Single(x => x.Name.Equals(dataTypeName, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Create a new tab.
        /// </summary>
        /// <param name="tabName"></param>
        /// <param name="sortOrder">Optional sort order index. Can be negative.</param>
        /// <returns></returns>
        public DocumentType CreateTab(string tabName, int sortOrder = 0)
        {
            if (UmbracoContentType.PropertyGroups.Contains(tabName))
            {
                throw new InvalidOperationException(string.Format("DocumentType `{0}` already contains tab `{1}`", UmbracoContentType.Name, tabName));
            }

            var propertyGroup = new PropertyGroup { Name = tabName, SortOrder = sortOrder };
            UmbracoContentType.PropertyGroups.Add(propertyGroup);

            return this;
        }

        /// <summary>
        /// Delete an existing tab.
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns></returns>
        public DocumentType DeleteTab(string tabName)
        {
            if (!UmbracoContentType.PropertyGroups.Contains(tabName))
            {
                throw new InvalidOperationException("");
            }

            UmbracoContentType.PropertyGroups.RemoveItem(tabName);

            return this;
        }

        /// <summary>
        /// Set the parent document type. Should be done at the point of DocumentType creation only.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public DocumentType SetParent([NotNull]DocumentType parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            UmbracoContentType.AddContentType(parent.UmbracoContentType);
            UmbracoContentType.ParentId = parent.UmbracoContentType.Id;

            return this;
        }

        /// <summary>
        /// Set whether content nodes of this document type can be created at the content tree root.
        /// </summary>
        /// <param name="allowAtRoot"></param>
        /// <returns></returns>
        public DocumentType SetAllowAtRoot(bool allowAtRoot)
        {
            UmbracoContentType.AllowedAsRoot = allowAtRoot;
            return this;
        }

        /// <summary>
        /// Allow content nodes of a specific document type to be created as children of content
        /// nodes of this document type.
        /// </summary>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public DocumentType AddAllowedChildNodeType([NotNull]DocumentType documentType)
        {
            var contentTypes = UmbracoContentType.AllowedContentTypes.ToList();
            var id = documentType.UmbracoContentType.Id;

            if (contentTypes.Any(x => x.Alias == documentType.UmbracoContentType.Alias))
            {
                Log.DebugFormat("{0} is already an allowed content type of {1}", documentType.UmbracoContentType.Alias, UmbracoContentType.Alias);
                return this;
            }

            contentTypes.Add(new ContentTypeSort(new Lazy<int>(() => id), 0, documentType.UmbracoContentType.Alias));

            UmbracoContentType.AllowedContentTypes = contentTypes;

            return this;
        }

        /// <summary>
        /// Remove the ability for content nodes of a specific document type to be created as children
        /// under content nodes of this document type.
        /// </summary>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public DocumentType RemoveAllowedChildNodeType([NotNull]DocumentType documentType)
        {
            var contentTypes = UmbracoContentType.AllowedContentTypes.ToList();

            contentTypes.RemoveAll(
                sort =>
                    sort.Alias.Equals(documentType.UmbracoContentType.Alias, StringComparison.InvariantCultureIgnoreCase));

            UmbracoContentType.AllowedContentTypes = contentTypes;

            return this;
        }

        /// <summary>
        /// Delete this document type and all related content nodes.
        /// </summary>
        public void Delete()
        {
            this.ContentTypeService.Delete(UmbracoContentType);
        }

        private static FluentDocumentTypeService FluentDocumentTypeService
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

            internal static readonly FluentDocumentTypeService Instance = new FluentDocumentTypeService(UmbracoUtils.Instance);
        }

        /// <summary>
        /// Retrieve a document type by its alias.
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static DocumentType Get([NotNull]string alias)
        {
            return FluentDocumentTypeService.Get(alias);
        }

        /// <summary>
        /// Create a new document type and save it to the database.
        /// </summary>
        /// <param name="alias">Alias. Cannot contain spaces or exotic punctuation.</param>
        /// <param name="name">Friendly document type name. Visible to content editors.</param>
        /// <returns></returns>
        public static DocumentType Create([NotNull]string alias, [NotNull]string name)
        {
            return FluentDocumentTypeService.Create(alias, name);
        }
    }
}