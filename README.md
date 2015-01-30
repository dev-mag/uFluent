###Nuget Packages
####uFluent

[![NuGet version](https://badge.fury.io/nu/ufluent.svg)](http://badge.fury.io/nu/ufluent)

Url : **http://www.nuget.org/packages/uFluent/**

Cmd : ```PM> Install-Package uFluent```
####uFluent.Migrate

[![NuGet version](https://badge.fury.io/nu/ufluent.migrate.svg)](http://badge.fury.io/nu/ufluent.migrate)

Url : **http://www.nuget.org/packages/uFluent.Migrate/**

Cmd : ```PM> Install-Package uFluent.Migrate```
##README

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
##Contents
* **[uFluent](#uFluent)**
  * [What is uFluent](#what-is-ufluent) 
  * [Api Documentation](#api-documentation)
* **[uFluent.Migrate](#ufluent-migrate)**
  * [What is uFluent.Migrate](#what-is-ufluent-migrate)
  * [Usage](#usage) 
  * [Whats it doing](#whats-it-doing)
  * [Living with migrations](#Living-with-migrations)

##uFluent
###What is uFluent

uFluent is an a Api that supplies a nice simple, single and fluent approach to driving umbraco services.

###Api Documentation
In progress... watch this space.

##uFluent Migrate
###What is uFluent Migrate

uFluent.Migrate is a tool which allows you to migrate your Umbraco configuration (Doc Types, Properties, Data Types etc) from one version to another using our Fluent API.

####It enables
* Safe automated deployments of umbraco configuration*
* Source controlled C# migration instructions
* Safe roll back of unsuccessful migrations

####Limitations
* Updating Umbraco configuration outside of migrations is a bad idea, don't do it.
* Once a migration has executed successfully its done, don't change it, further changes/fixes must be in another migration.

###Usage
**First - Create a migration**
First you will need to add a migration. Add a new class that implements IUmbracoMigration in the uFluent.Migrate folder that was created as part of the NuGet package install. 
```C#
public class FirstMigration : IUmbracoMigration
{
    public override void Migrate()
    {

    }
}
```
We suggest you create some sort of hierarchy to store your migrations as in a large project the number of migrations can quickly build up. We currently have almost 200 migrations and counting in one of our projects.

**Then - Add the migration to execution list**
Next you need to add this migration to the list that uFluent.Migration will run through, you'll find this in the uFluent.Migration folder that was created as part of the NuGet package installation.

```C#
internal class MigrationList : IMigrationList
{
    public IEnumerable<IUmbracoMigration> Migrations
    {
        get
        {
            return new List<IUmbracoMigration>
            {
                new FirstMigration()
            };
        }
    }
}
```

**Then - Enable migrations**
Before we can run any migrations we need to modify the config\uFluent.config file to enable migrations.

```XML
<uFluent enableMigrations="true"/>
```

**Finally - Execute migrations on application started**
Last but not least you must make a call to uFluentMigrate.Run() on the application started event handler as below.
```C#
public class ApplicationStartupHandler : ApplicationEventHandler
{
    protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
    {
		base.ApplicationStarted(umbracoApplication, applicationContext);
        uFluentMigrate.Run();
    }
}
```

###Whats it doing
On .Run() command uFluent.Migration will iterate over the migration list and attempt to execute each one in turn. First it checks to see if there is a record of the migration having already been ran in the MigrationHistory table in your Umbraco database. If there is a record of it successful executing it moves on to the next migration, if the previous attempt failed it stops in its tracks running no further migrations.

All being well it will move on to run the migration, if at any point the migration throws an exception the migration is marked as failed in the database and all changes are rolled back as part of a transaction (each migration executes within its own transaction).

We have intentionally made it fail fast and completely if anything unexpected happens as ultimately it will be running against a production database which needs protecting at all costs. Even so we highly recommend you create a backup of your database before deploying just to be sure.

###Living with migrations
There is only one scenario where its ok to re run a migration and thats when it has previousely failed, there is no reason this wouldnt be caught and rectified during local testing. In this case simply delete the migrations entry in the MigrationHistory table and restart the application. Repeat untill you have a successfully executing migration.

If a migration has successfully ran **never** modify it and run it again, this is a bad idea as you can't be sure of the outcome in subsiquent environments. On a few occasions early on we tried undoing the effects of a migration using the back end and re running it... it never ended well, in one case all our doc types lost their hierachy! Needless to say that is cataclismic, and we have no idea why it happened, because Umbraco.

Our current development process is to take a backup of our local database, tweek and run a migrations untill we are happy with it, then do a restore and run it fresh. This will mimmick the deployment to test/production environments as close as is possible.
