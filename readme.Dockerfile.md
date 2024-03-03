# Dockerfile

Dockerfiles help to automate building images

## Content
1) [Dockerfile stages](#stages)
2) [Optimizations](#optimizations)
3) [Environment variables](#env)
4) [Docker CLI commands](#commands)
5) [Dockerfile example](#example)
6) [References](#references)

## Stages

```
FROM mcr.microsoft.com/dotnet/aspnet AS base
...

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
...

FROM build AS publish
...
```

These are stages. `FROM` statements refer to images used to run the stage.

Its a process of building upon images

A Dockerfile with many stages is referred to as [Multi Stage Build](https://docs.docker.com/build/building/multi-stage/)

There is conceptually 2 main stages, _build_ stage and _runtime_ stage

Its like manufacturing a car in stages where each stage requires different tools. 

Note that you can _dotnet build_ a solution, but should only _dotnet publish_ individual project files

---

The `FROM mcr.microsoft.com/dotnet/aspnet` image contains everything you need to run aspnet core applications.

Note that the `mcr.microsoft.com/dotnet/runtime` can also be used to run the app.

---

Using aliases, you can use the `COPY` instruction to refer to a previous stage image. As in

`COPY --from=build_stage /bin/hello /bin/hello`

Note that without `--from=...` it automatically copies from the host directory

---

The `FROM mcr.microsoft.com/dotnet/sdk` image (`sdk`) is much bigger than the `aspnet` image. 

It contains everything to build .NET application. 

---

Since the `sdk` image is much bigger than the `aspnet` image, 

the strategy is to build and publish the solution on the `sdk` image and copying it to the `aspnet` image afterwards.

hence the back and forth in the Dockerfile's stages between various `FROM` statements. 

## Optimizations

Separation of the .csproj `COPY` 

```
COPY ["ordering.csproj", "ordering/"]
RUN dotnet restore "ordering/ordering.csproj"
```
 and the `COPY` of the rest of the app source code

```
COPY . ./ordering
RUN dotnet build "ordering.csproj" -c Release -o /app/build
```
ensures that the `dotnet restore` will use caching appropriately. 

The `restore` will only restore packages when the .csproj file is updated.

Without this separation of the `COPY` instruction, the `restore` would re-execute each time source code is updated.

## Env

Use `-e` option to pass environment var to the docker cli run command

e.g. `docker run -e ASPNETCORE_ENVIRONMENT=Development --rm -p 1234:8080 webapp1:debug`

Or use a declarative approach by writing these ENV variables in the Dockerfile itself

Useful environment variables 
- ENV ASPNETCORE_HTTP_PORT=http://+:5000
- ENV ASPNETCORE_HTTPS_PORT=https://+:5001
- ENV ASPNETCORE_URLS=http://+:5000
- ENV ASPNETCORE_ENVIRONMENT=Development <-- Useful to be able to open Swagger
- ENV BUILD_CONFIG Debug

Note that the default container port for aspnet core is 8080. 

So in order to view the containerized app in you host's browser you need
- redirect whichever host's port to container port 8080 , as in `docker build -p 1234:8080`
- optionally override the default port of the container `docker build -e ASPNETCORE_URLS=http://+:5000`

ASPNETCORE_URLS is an environment variable used by ASP.NET core to set the IP:Port combination to listen to. This is set to http://*:${URL_PORT}. Notice the *; it translates to 0.0.0.0. If you set the url to localhost, or 127.0.0.1 you will not be able to redirect requests from your host to your containers.

This image sets the ASPNETCORE_URLS environment variable to http://+:80 which means that if you have not explicity set a URL in your application, via app.UseUrl in your Program.cs for example, then your application will be listening on port 80 inside the container

ARG declares an argument that must be specified at build time. As mentioned before here we take the port number the web app should run on as an argument. Environment variables and arguments can be referenced using the ${var_name} syntax.

`docker run --rm -it -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000  order_container`


## Commands

`docker build --rm -f ./WebApplication1/Dockerfile -t webapp1:test1 .`

`--rm` removes intermediate images

The `.` in the docker build command sets the build context of the image

`docker run --rm -d --name name1 order_container`

`docker run --rm -p 8080:8080 -p 8081:8081 -e ASPNETCORE_ENVIRONMENT=Development webapp1:debug`

`docker run -it --rm -p 1234:8080 -p 4321:443 -e ASPNETCORE_HTTPS_PORT=https://+:5001
-e ASPNETCORE_URLS=http://+:5000 --name aspnetcore_sample aspnetapp`

`docker run --rm -p 1234:5000 -p 4321:5000 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_URLS=http://+:5000 webapp1:debug8080`

`-it` interactive shell

`--rm` removes the containers when process is killed or stopped

`-d` detached mode

`-f` to specify the location of the Dockerfile

`docker inspect <container_id>` shows network information about the container

`docker logs <container_id>` shows logging information about the container

## Example

```
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
COPY ["ordering.csproj", "ordering/"]
RUN dotnet restore "ordering/ordering.csproj"
# Copy the rest of the application code
COPY . ./ordering
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
```

## References
- https://docs.docker.com/reference/dockerfile/
- https://marcroussy.com/2021/02/15/breakdown-of-aspnet-core-dockerfile/
- https://github.com/dotnet/dotnet-docker-samples 
- https://medium.com/@Likhitd/asp-net-core-and-mysql-with-docker-part-3-e3827e006e3

