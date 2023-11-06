## InMemory Db Ef core


## Docker


## K8s



run :
```
docker build -t web-api .
docker images
```

```
docker run -d -p 5001:80 — name web-api-container web-api
docker ps

```
*-p 5001:80: Map port 5001 on your local machine to port 80 inside the container.*

*web-api: Use the image you built earlier*

ref : [Medium Article](https://medium.com/@jaydeepvpatil225/containerize-the-net-core-7-web-api-with-docker-and-kubernetes-9dd23e392936)