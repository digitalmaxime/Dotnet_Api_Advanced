

## SrcNugets

# WebApi
- Swagger
  - `dotnet add package Swashbuckle.AspNetCore`
  - `dotnet add package Swashbuckle.AspNetCore.Swagger`
- Middleware
  - Custom Exception Handler Middleware
- Logging
  - Serilog
    - `dotnet add package Serilog`
    - `dotnet add package Serilog.AspNetCore`
    - `dotnet add package Serilog.Extensions.Logging`
    - `dotnet add package Serilog.Settings.Configuration`
    - `dotnet add package Serilog.Sinks.File`
    - `dotnet add package Serilog.Sinks.Console`

# Application
- AutoMapper (Core project)
- MediatR (Core project)
- FluentValidation (Core project)

# Infrastructure
- Microsoft.EntityFrameworkCore.Design
- MySql.EntityFrameworkCore
- Microsoft.Extensions.Options.ConfigurationExtensions ??

# Tests

Using the xUnit framework

- Moq
- Shouldly
- Microsoft.AspNetCore.Mvc.Testing for Api Integration Tests (in order to spin up a test server and make requests to it)

# Create Client with NSwag
MVC Project

`dotnet add package NSwag.AspNetCore`

`builder.Services.AddOpenApiDocument();`

Create an “nswag.json” file with 
`nswag new` or pasting a file..

`nswag run nswag.json`
This will generate cs client under ./Generated/Client

# Security 

Using IdentityServer
available since AspNetCore 8.0

in the Identity project, 
reference 

`<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.0" />`

it exposes an endpoint for the client to authenticate and get a token
through minimal api

so `builder.Services.AddEndpointsApiExplorer();` is needed since identiy server uses minimal api

Map the endpoint to the IdentityServer

app.MapIdentityApi<ApplicationUser>();

migrations : 

`dotnet ef migrations add init --project .\Identity --startup-project .\API\ --context IdentityDbContext`

`dotnet ef database update --project .\Identity --startup-project .\API\ --context IdentityDbContext`
