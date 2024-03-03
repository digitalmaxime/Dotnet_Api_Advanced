# Docker Compose

Infrastructure as code / Declarative code 

The docker-compose.yml file (and Dockerfile) should be placed at the root of the project. 

Docker Compose can abstract the process of running containers (`docker run`)

as well as the process of passing of parameters to those containers (`docker run -e ...`)

It encapsulate the various command line execution that would normally have to be done in a shell

e.g. `docker build -t myimage:latest .` and `docker run --rm -it -p 8080:80 myimage:latest`

## Content
1) [Docker Compose file structure](#structure)
2) [Network](#network)
3) [Volumes](#volumes)
4) [Services](#services)
5) [Commands](#commands)
6) [References](#references)

## Structure

The file follows a specific yaml format

- `version: ` `3.1` \# https://docs.docker.com/compose/compose-file/compose-versioning/#versioning
- `services: `
  - `<my-service-name>`
    - `build: `. # path to the context (which holds the Dockerfile)
    - `image: ` `<image-name>:<tag>` # if the image already exists ??
    - `ports: `
      - `"8080:80"` # 8080 is the host's port, 80 is the container port
      - 

Note that the `build: ` instruction for a given service can be more specific

e.g.
- build:
  - context: .
  - dockerfile: Dockerfile.publish
  - args: <my-args>

Note also that the build tag (`docker build -t my-tag`) will be taken from the image tag

## Network

Service discovery is enabled by Docker based on DNS, based on the name of the service.

The IP address of an other container service declared in the same docker-compose file 

can be reference by its service name within the application. 

e.g. HttpClient calling another smtp micro service on `localhost:1025` wont work if the services are containerized. 

The http client should instead make a call to the `<smtp-service-name>:1025` IP address. Where `<smtp-service-name>` is the name of the service defined in the docker compose file.



## Volumes

## Commands

`docker compose up`

- When a change is made to the definition of a service (`services: <my-service>`), 
running docker compose up will recreate an instance of the container
- Note that if the Dockerfile is change (the definition of the image), `docker-compose up --build` is required to rebuild the image (and not use the  cached image)

`docker compose down`

- Stop and remove containers, networks, images(not really) and volumes

`docker-compose logs` shows logs

`docker-compose logs -f` shows logs and follow the logs (monitor the app)

`docker-compose -d` detached mode

`docker inspect <container_id>` shows network information about the container

--

does not delete the associated volume

## References

- https://docs.docker.com/compose/
- https://medium.com/@Likhitd/asp-net-core-and-mysql-with-docker-part-3-e3827e006e3
- https://www.youtube.com/watch?v=WQFx2m5Ub9M
