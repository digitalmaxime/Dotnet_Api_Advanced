# Dotnet_Api_Advanced

- Include Telemetry (dotnet Activity)

## Useful commands

dotnet certificate
`dotnet dev-certs https --verbose --trust`

list PID of port

`sudo lsof -i -P | grep LISTEN | grep 3306 `

`netstat -vanp tcp | grep 3000`

`sudo lsof -i :3000`

`kill -15 <PID>`

- `-9` kills process immediately no clean up
- `-15` (TERM)
- `-3` (QUIT)

Single command

`kill -9 $(lsof -ti:3000,3001)`