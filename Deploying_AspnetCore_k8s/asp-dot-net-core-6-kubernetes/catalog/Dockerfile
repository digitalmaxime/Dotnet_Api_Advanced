FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#ARG URL_PORT
#ENV BUILD_CONFIG Development
ENV ASPNETCORE_ENVIRONMENT Development
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["catalog/catalog.csproj", "catalog/"]
RUN dotnet restore "catalog/catalog.csproj"
RUN echo "\n\nHello\n\n"
COPY ./catalog ./catalog
RUN dotnet build "catalog/catalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "catalog/catalog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
#ENV ASPNETCORE_URLS http://*:${URL_PORT}
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "catalog.dll"]
