## Create a SqlServer on mac :

- docker desktop --> settings --> Features in development --> Use Rosetta for x86/amd64 emulation on Apple Silicon
  - this is because Sql Server is designed to run on x86 architecture
- `docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=VeryStr0ngP@ssw0rd" -p 1433:1433 --name sql --hostname sql --platform linux/amd64 -v /sql_server:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2022-latest`


"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=EventCatalogDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
