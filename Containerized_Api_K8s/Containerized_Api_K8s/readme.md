## InMemory Db Ef core


## Docker


run :
```
docker build -t web-api .
docker images
```

The docker build command is used to build a Docker image from a Docker file.
This command creates a Docker image that uses the Dockerfile in the current directory (.) and marks it as web-api.

```
docker run -d -p 5001:80 — name web-api-container web-api
docker ps

```
*-p 5001:80: Map port 5001 on your local machine to port 80 inside the container.*

*web-api: Use the image you built earlier*

## K8s

```
kubectl apply -f deployment.yml
kubectl apply -f service.yml
```


ref : [Medium Article](https://medium.com/@jaydeepvpatil225/containerize-the-net-core-7-web-api-with-docker-and-kubernetes-9dd23e392936)