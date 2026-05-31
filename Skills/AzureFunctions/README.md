## Prerequisites
1. Azurite
2. CosmosDB Emulator
3. Azure Functions Core Tools

## Step 1
Identify triggers, input  bindings, outut bindings 
<br>*[Supported Bindings](https://learn.microsoft.com/en-us/azure/azure-functions/functions-triggers-bindings?tabs=isolated-process%2Cnode-v4%2Cpython-v2&pivots=programming-language-csharp)*
1. Run the following command to initialize a functions project
`func init --worker-runtime dotnet-isolated` 
2. Use decorators for functions and parameters. *See BlobEventProcessor function in FunctionExample.cs*

## Step 2
Download & setup dependencies 
1. Download Azurite
2. Set Azurite file location
    * Open File → Preferences → Settings
    * Search for “Azurite: Location”
    * Set it to `.azurite`
3. Set local.settings.json:
    ``` 
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "ImagesBlobStorage": "UseDevelopmentStorage=true",
    "CosmosDBConnection": "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;",
    "AzureSignalRConnectionString": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "FUNCTIONS_INPROC_NET8_ENABLED": 1
    ```
4. Download the Azure CosmosDB emulator
    * Create a database named 'GIC'
    * Create a container named 'Processed_Images'
    * Launch the emulator
5. Create a new blob storage container in Azurite named 'images' 

## Step 3
Run the function app
1. Open the Command Palette (Ctrl + Shift + P). Type “Azurite: Start” → press Enter.
2. Add a test image to your Azurite container
3. Ensure you are in the root directory and run the following commands to start the Function app:
    ```
    cd Functions
    dotnet restore
    dotnet build
    dotnet run
    ```
4. To test the FunctionTester, open another command line terminal at the project root directory and run the following commands:
    ```
    cd FunctionTester
    dotnet restore
    dotnet build
    dotnet run
    ```

Your image saved in Azurite is read by the Azure Function and the image meta data is added to a document that is inserted into your Cosmos DB container.