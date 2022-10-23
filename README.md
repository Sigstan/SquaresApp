# Square App
Square App is designed to store and edit list of coordinates and identify squares from them.

## Prerequisites 
- .NET Core CLI
- MSSQL Server

## Project structure
- SquaresApp.Api - API project.
- SquaresApp.Core - services project.
- SquaresApp.Models - shared models between projects.
- SquaresApp.Storage - EF entities, migrations, DbContext.
- SquaresApp.Tests -  unit tests project.

## SwaggerUI
SwaggerUI can be found https://localhost:<port>/

## Configuration
Database connection string is in appsettings.json {"ConnectionStrings":"DefaultConnection"}

## Project startup
Database can be migrated from ```Package manage console```, or when ```SquaresApp.Api``` is launched.

Open a terminal/command prompt and navigate to the folder in which you keep SquaresApp.Api project
Write command ```dotnet run```
