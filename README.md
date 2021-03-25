## Loyalty Logistix Sample App

This is a sample app used for internal purposes, demonstrating various techniques and patterns.

There are comments all over the code, providing details on various usages.

The sample covers the following topics:
- How to structure your repository
- How to structure your solution and projects. If you doing microservices, you want to keep it simple and use a single project (anyway it's a small app). But, in our case, we're not doing microservices per se. The Customer component is relatively large, and it's wise to separate it into several layers. That's especially useful since you're having different implementations for various clients. You can use the common stuff, and create separate layers per client as required.
- How to use various persistence technologies/techniques/patterns
	- EntityFramework Core
	- EntityFramework Core with raw SQL
	- EntityFramework Core with ADO.NET approach (if you're nostalgic :grinning: )
	- Repository pattern
	- Repository pattern with specifications
- Simple caching techniques through repositories
- How to easily switch from "monolithic" to "distributed" mode.
- Automapper usage
- How to use and configure IoC containers (including runtime configurations)

## How to run the application

The solution can be run in two modes, either in "monolithic" or "distributed" mode. You can switch it easily through the `appsetting.json` config file.

### Monolith mode
- Open the solution in Visual Studio
- Select `Loyalty.ApiGateway` project as a startup project.
- Run the application :)
- It will automatically create a database, seed it with test data, and you'll get the swagger page so you can test the endpoints.

### Distributed mode
- Run the Customer component (web API). Open PowerShell, get under `src\Customer\CustomerApp.Api\` folder, and execute `dotnet run`. Leave the process working.
- Open the solution in Visual Studio
- Select `Loyalty.ApiGateway` project as a startup project.
- Edit the `appsetting.json` file in `Loyalty.ApiGateway` project. Change the `Type` to "Distributed"
```
"ApplicationOptions": {
// Available options: "Monolithic", "Distributed"
"Type": "Distributed"
},
```
- Run the application. 

## References

Some references to various patterns that are applied here:

https://github.com/ardalis/ApiEndpoints

https://github.com/ardalis/Specification

https://ardalis.com/building-a-cachedrepository-in-aspnet-core/

https://fiseni.com/posts/open-close-principle-and-runtime-di-configuration/

https://fiseni.com/posts/immutable-entities-in-ef-core/

https://fiseni.com/posts/alternative-caching-implementations-and-cache-invalidation/

