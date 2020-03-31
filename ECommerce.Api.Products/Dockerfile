FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-nanoserver-1909 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.3-nanoserver-1909 AS build
WORKDIR /src
COPY ["ECommerce.Api.Products/ECommerce.Api.Products.csproj", "ECommerce.Api.Products/"]
RUN dotnet restore "ECommerce.Api.Products/ECommerce.Api.Products.csproj"
COPY . .
WORKDIR "/src/ECommerce.Api.Products"
RUN dotnet build "ECommerce.Api.Products.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Api.Products.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Api.Products.dll"]
