# Setup
dotnet add package Microsoft.EntityFrameworkCore.Design

# Migrate
dotnet ef migrations add InitialCreate --project ../PassTxt.Shared --startup-project .
dotnet ef database update --project ../PassTxt.Shared --startup-project .


