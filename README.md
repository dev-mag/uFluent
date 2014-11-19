##README##

###TL;DR

```c#
var homepageTemplate = Template.Create("Homepage");
var homepageDocType = DocumentType.Create("Homepage")
        .SetDefaultTemplate(homepageTemplate)
        .AddProperty("Title", DataTypes.TextString)
        .Save();
        
var contentPageTemplate = Template.Create("Content Page");
var contentPageDocType = DocumentType.Create("Content Page")
        .SetParent(homepageDocType)
        .SetDefaultTemplate(contentPageTemplate)
        .AddProperty("Image", DataTypes.MediaPicker)
        .AddProperty("Content", DataTypes.RichText)
        .Save();
        
homepageDocType
        .AddAllowedChildNodeType(contentPageDocType)
        .Save();

```

###What is uFluent.Migrate?###

uFluent.Migrate is a tool which allows you to migrate your Umbraco configuration (Doc Types, Properties, Data Types etc) from one version to another using our Fluent API.

####It enables####
* Safe automated deployments of umbraco configuration*
* Source controlled C# migration instructions
* Safe roll back of unsuccessful migrations

####Limitations####
* Updating Umbraco configuration outside of migrations is a bad idea, don't do it.
* Once a migration has executed successfully its done, don't change it, further changes/fixes must be in another migration.