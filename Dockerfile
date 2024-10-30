# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj files and restore as distinct layers
COPY PassTxt.ConsoleApp/*.csproj ./PassTxt.ConsoleApp/
COPY PassTxt.Shared/*.csproj ./PassTxt.Shared/
COPY PassTxt.WebAPI/*.csproj ./PassTxt.WebAPI/

#RUN dotnet restore ./PassTxt.ConsoleApp/PassTxt.ConsoleApp.csproj
RUN dotnet restore ./PassTxt.WebAPI/PassTxt.WebAPI.csproj

# Copy everything else and build
COPY . ./

#RUN dotnet publish ./PassTxt.ConsoleApp/PassTxt.ConsoleApp.csproj -c Release -o /app/publish
RUN dotnet publish ./PassTxt.WebAPI/PassTxt.WebAPI.csproj -c Release -o /app/publish

# Stage 2: Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/publish ./

# Set environment variables
ENV DOTNET_ENVIRONMENT=Development

# Command to run the app

#ENTRYPOINT ["dotnet", "PassTxt.ConsoleApp.dll"]
ENTRYPOINT ["dotnet", "PassTxt.WebAPI.dll"]
