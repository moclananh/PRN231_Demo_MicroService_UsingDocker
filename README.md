1. API service
[B1] Libraly
	<ItemGroup>
		<PackageReference Include="Microsoft.EntityFrameworkCore" Version="5.0.17" />
		<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.17" />
		<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="5.0.17">
			<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
			<PrivateAssets>all</PrivateAssets>
		</PackageReference>
		<PackageReference Include="Microsoft.Extensions.Configuration" Version="5.0.0" />
		<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="5.0.0" />
	</ItemGroup>

[B2]
"ConnectionStrings": {
    "DefaultConnection": "Server=Moclananhh;Database=PRN221_SU23_PE;Persist Security Info=True;User ID=sa;Password=123456"
  },

[B3]
dotnet ef dbcontext scaffold "server =moclananhh; database = PRN221_SU23_PE;uid=sa;pwd=123456;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir DataAccess

[B4]
services.AddDbContext<SchoolContext>(options => 
options.UseSqlServer(Configuration.GetConnectionString("SchoolContext")));

[B5]
-> Generate API CRUD

[B6] Create DockerFile: new item/DockerFile || Note: Update name of service

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY CustomerAPI.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "CustomerAPI.dll" ]

[B7] Docker create image: docker build -t productimage . || Note: imgname must be lowercase
[B8] Docker create container: docker run -it -d --rm -p 3000:80 --name Product productimage  || Note: nếu nhiều API thì chỉnh port khác nhau
-> test docker


2. API Gateway
[B1] Libraly
	<PackageReference Include="Ocelot" Version="15.0.7" />

[B2] Create Ocelot.json || Note: Tùy theo API mà tạo downstream và upstream phù hợp  (theo Http của API có truyền {Id} hay không), Port phải cùng port Docker đã push container

{
  "ReRoutes": [
    {
      "DownstreamPathTemplate": "/api/Customers",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 3000
        }
      ],
      "UpstreamPathTemplate": "/apiGateway/Customers",
      "UpstreamHttpMethod": [ "GET", "POST" ]
    },
    {
      "DownstreamPathTemplate": "/api/Products",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 3001
        }
      ],
      "UpstreamPathTemplate": "/apiGateway/Products",
      "UpstreamHttpMethod": [ "GET", "POST" ]
    },
    {
      "DownstreamPathTemplate": "/api/Customers/{id}", // This includes {id} to capture the customer ID.
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 3000
        }
      ],
      "UpstreamPathTemplate": "/apiGateway/Customers/{id}", // Include {id} in the upstream path.
      "UpstreamHttpMethod": [ "GET", "PUT", "DELETE"]
    },
    {
      "DownstreamPathTemplate": "/api/Products/{id}", // This includes {id} to capture the product ID.
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 3001
        }
      ],
      "UpstreamPathTemplate": "/apiGateway/Products/{id}", // Include {id} in the upstream path.
      "UpstreamHttpMethod": [ "GET", "PUT", "DELETE"]
    }
  ]
}

[B3]: Program.cs
public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //update code here...
                }).ConfigureAppConfiguration((HostingContext, config) =>
                {

                    config.SetBasePath(HostingContext.HostingEnvironment.ContentRootPath).
			AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);

                });

[B4]: Startup.cs

services.AddOcelot();
app.UseOcelot().Wait();

-> test API gateway: https://localhost:7001/apiGateway/Customers



3.Client
[B1]: Create MVC project
[B2]: Create Model for each API entities
[B3]: Create Controller and generate view

-> Test CRUD func

