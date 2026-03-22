## Prerequisites
1. CosmosDB resource in Azure


## Step 1
Configure Cosmos DB 
1. Create an Azure Cosmos DB account:
 - Go to the Azure Portal
 - Create a new resource → Azure Cosmos DB
 - Select Core (SQL) API
2. Configure basic settings:
 - Choose a Resource Group
 - Provide a unique account name
 - Select a region close to you (e.g., Canada Central)
3. After deployment completes:
 - Navigate to your Cosmos DB account
 - Copy:
    - URI (Endpoint)
    - PRIMARY KEY
 - Update your application configuration in Program.cs:
```
private const string ENPOINT_URI = "<your-cosmos-endpoint>";
private const string KEY = "<your-primary-key>";
```
## Step 2
Review the program.cs file to understand how to use the Azure Cosmos DB SQL API with the .NET SDK to:
1. Create a database and container
2. Insert documents
3. Query data using both LINQ and SQL
4. Update and delete documents

## Step 3 — Run the Application
From the project root enter the following command:
`cd Cosmos_DB_SQL_API_Example`

Run the following to start the application:
```
dotnet restore
dotnet build
dotnet run
```
