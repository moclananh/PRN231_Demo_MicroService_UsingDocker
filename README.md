
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

**[B3]** Scaffold the database context using Entity Framework Core:  
```bash
dotnet ef dbcontext scaffold "server=Moclananhh;database=PRN221_SU23_PE;uid=sa;pwd=123456;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir DataAccess
```

**[B4]** Configure the database context in `Startup.cs`:  
```csharp
services.AddDbContext<YourDbContext>(options => 
    options.UseSqlServer(Configuration.GetConnectionString("SchoolContext")));
```

**[B5]** Generate API CRUD operations based on the database model.

---

## Docker Setup

**[B6]** Create a Dockerfile:  
```dockerfile
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

**[B7]** Build the Docker image:  
```bash
docker build -t productimage .
```
*Note*: Image name must be lowercase.

**[B8]** Create and run a Docker container:  
```bash
docker run -it -d --rm -p 3000:80 --name Product productimage
```
*Note*: For multiple APIs, use different ports to avoid conflicts.

---

## API Gateway

**[B1]** Include the Ocelot library.

**[B2]** Create an `Ocelot.json` file for routing configuration. Example:  
```json
{
  "ReRoutes": [
    {
      "DownstreamPathTemplate": "/api/Customers",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [{ "Host": "localhost", "Port": 3000 }],
      "UpstreamPathTemplate": "/apiGateway/Customers",
      "UpstreamHttpMethod": ["GET", "POST"]
    },
    {
      "DownstreamPathTemplate": "/api/Products",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [{ "Host": "localhost", "Port": 3001 }],
      "UpstreamPathTemplate": "/apiGateway/Products",
      "UpstreamHttpMethod": ["GET", "POST"]
    },
    {
      "DownstreamPathTemplate": "/api/Customers/{id}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [{ "Host": "localhost", "Port": 3000 }],
      "UpstreamPathTemplate": "/apiGateway/Customers/{id}",
      "UpstreamHttpMethod": ["GET", "PUT", "DELETE"]
    },
    {
      "DownstreamPathTemplate": "/api/Products/{id}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [{ "Host": "localhost", "Port": 3001 }],
      "UpstreamPathTemplate": "/apiGateway/Products/{id}",
      "UpstreamHttpMethod": ["GET", "PUT", "DELETE"]
    }
  ]
}
```

**[B3]** Update `Startup.cs`:  
```csharp
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                  .AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);
        });

services.AddOcelot();
app.UseOcelot().Wait();
```

**Test the API Gateway:**  
Navigate to: `https://localhost:7001/apiGateway/Customers`

---

## Client Application

**[B1]** Create an MVC project.

**[B2]** Create models for each API entity.

**[B3]** Create controllers and generate views for CRUD operations.

**Test the CRUD functionality** to ensure the application works as expected.
