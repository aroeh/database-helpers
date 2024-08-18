# database-helpers
This is a repo focused around helper classes and libraries for communicating with databases


## Demonstrated Features
- Database Images and Containers
- MongoDB CRUD Operations
    - Generic class to handle MongoDB Specific operations and generic return types
- MSSQL CRUD Operations
    - Class to handle SQL Specific operations with a more user friendly wrapper class
    - Generic class to handle SQL Operations and generically map responses using reflection
    - Text Commands
    - Stored Procedure Commands
    - Transactions
    - DataReader
    - SQL Adapter DataTable Fill


# Tools
- Docker Desktop
- Docker Compose V2
- SQL Server Management Studio (SSMS) or Azure Data Studio

# Getting Started
There are different projects in the solution with the goal of showcasing connecting to databases and performing basic CRUD operations.  The goal of this repo is not to demonstrate perfect security, as there are some very obvious security no nos here, but to show some options and abstractions for database code.

## MSSQL Setup
I haven't found a better way to handle the MSSQL setup just yet, but for now these are the steps for setting the MS SQL Containers

1. Run the docker compose command to generate all images and start all containers
```
docker compose up -d
```

2. Once the mssql-data container is running, connect to using either SSMS or Azure Data Studio
    > You can connect using the following connection string
    ```
    Persist Security Info=False;User ID=sa;Password=YourStrong@Passw0rd;Server=localhost;Encrypt=True;
    ```

3. You should be in the master database at this point.  Open a new query editor and run the script in the `setup.sql` file
> This file contains all script to create the Samples database, tables, insert seed data, and stored procedures

# References
- [MongoDb Docker](https://hub.docker.com/_/mongo)
- [Deploy and Connect to MSSQL](https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-docker-container-deployment?view=sql-server-ver16&pivots=cs1-bash)
- [MSSQL Docker Installation](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&tabs=cli&pivots=cs1-bash)
- [MSSQL Container Github](https://github.com/microsoft/mssql-docker)
- [MSSQL Docker Samples](https://docs.docker.com/samples/ms-sql/)
- [Azure CosmosDb Emulator](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator?tabs=windows%2Ccsharp&pivots=api-nosql)
- [Azure CosmosDb Emulator Docker](https://github.com/Azure/azure-cosmos-db-emulator-docker)
- [PostgresSql Docker](https://hub.docker.com/_/postgres)