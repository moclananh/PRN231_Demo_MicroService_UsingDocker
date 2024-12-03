# API and Gateway Configuration Guide

## API Service

**[B1]** Include the necessary libraries:  
`runtime; build; native; contentfiles; analyzers; buildtransitive`

**[B2]** Configure connection strings in `appsettings.json`:  
```json
"ConnectionStrings": { 
  "DefaultConnection": "Server=Moclananhh;Database=PRN221_SU23_PE;Persist Security Info=True;User ID=sa;Password=123456" 
} 
```
[B3] Scaffold the database context using Entity Framework Core:

```
dotnet ef dbcontext scaffold "server=Moclananhh;database=PRN221_SU23_PE;uid=sa;pwd=123456;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir DataAccess
```
[B4] Configure the database context in Startup.cs:

```
services.AddDbContext<YourDbContext>(options => 
    options.UseSqlServer(Configuration.GetConnectionString("SchoolContext")));
```
[B5] Generate API CRUD operations based on the database model.
Docker Setup

[B6] Create a Dockerfile:
````dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY CustomerAPI.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "CustomerAPI.dll"]
```

[B7] Build the Docker image:
```
docker build -t productimage .
```
Note: Image name must be lowercase.

[B8] Create and run a Docker container:
```
docker run -it -d --rm -p 3000:80 --name Product productimage
Note: For multiple APIs, use different ports to avoid conflicts.
```
