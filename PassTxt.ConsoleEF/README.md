# Setup EF Core Console App
dotnet new console -o EFCoreConsoleApp
cd EFCoreConsoleApp
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
