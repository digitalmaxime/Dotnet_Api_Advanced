# Subject

Intro ..

## Implementation steps
1) [Add required packages](#packages)
3) [Create the database context and DbSets](#context)
7) [Migrations](#migrations)
9) [Reference](#reference)

## Packages
- `Microsoft.EntityFrameworkCore.SqlServer` includes the `Microsoft.EntityFrameworkCore` package itself
- `Microsoft.EntityFrameworkCore.Design` why not mentioned by Gill?? for migrationsa
- `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore` contains types that will be useful to capture and report diagnostics from EF Core

Dotnet CLI `dotnet add package <packageName>`

## Context

Defined in the `/models` folder if the project is super simple or..

- class that derives from DbContext base class
- allows
    - connection
    - model building

E.g. `public class MyDbCtx : DbContext `
- `public ctor(DbContextOptions<MyDbContext> options) : base(options)`
- `{...}`

*notice that the table name is plural*


## Model


## Migrations

CLI `dotnet ef migrations add` and  `dotnet ef database update`

## Reference

reference : [blabla](https://www.google.com)