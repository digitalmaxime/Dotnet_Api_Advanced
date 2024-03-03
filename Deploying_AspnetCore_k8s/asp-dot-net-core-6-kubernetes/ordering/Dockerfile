# Use the official .NET Core image as a parent. Use either "aspnet" or "runtime" (small images)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# The WORKDIR command changes the current directory inside of the container to App
WORKDIR /app
# Default .NET Core port. This doesn't open the port, simply a contract between the running of the container
EXPOSE 80
EXPOSE 443

# Use the official .NET Core SDK image. (large image)
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
# Copy the project file and restore any dependencies (use .csproj for the project name)
COPY ["ordering/ordering.csproj", "ordering/"]
RUN dotnet restore "ordering/ordering.csproj"
# Copy the rest of the application code
COPY . .
# Build the runtime image
WORKDIR "/src/ordering"
RUN dotnet build "ordering.csproj" -c Release -o /app/build
# At this point, we have a temp image built that contains the compiled application (on the sdk image)

# Publish the application (based on the sdk image with alias "publish")
FROM build AS publish
RUN dotnet publish "ordering.csproj" -c Release -o /app/publish


# back to the first pulled image, "base" with a new "final" alias.
FROM base AS final
WORKDIR /app
# Copy from the image aliased "publish" (just above) the published content to the current dir.
COPY --from=publish /app/publish .
# Start the application
ENTRYPOINT ["dotnet", "ordering.dll"]
