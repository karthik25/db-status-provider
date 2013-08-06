### Read Me

This is a sample project that uses the DbStatusProvider 0.1. This project's web.config contains the following entry:

```xml
<dbStatusUpdater contextType="NugetTester.Contexts.SchemaContext" scriptsBase="~/Sql" scriptsPrefix="sc" />
```

"contextType" attribute has to contain the fully qualified name of the class that could serve as the data context for the db status provider. 
The SchemaContext class implements the ISchemaContext interface as required. However, it does not talk to a database, but just returns 
a static list, since this is just a sample project. This project has a Sql folder with 4 scripts (these are the scripts for sBlog.Net).
The path is specified in the scriptsBase attribute. Finally the scriptsPrefix attribute specifies the prefix of these scripts.
