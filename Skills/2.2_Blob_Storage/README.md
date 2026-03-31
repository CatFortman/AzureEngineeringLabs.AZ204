## Prerequisites
Azure Storage Account with Blob Storage enabled

## Step 1 Configure Azure Blob Storage
1. Create an Azure Storage Account:
   - Go to the Azure Portal
   - Create a new resource → Storage Account
   - Select standard performance and StorageV2 (general-purpose v2)
2. Configure basic settings:
    - Choose a Resource Group
    - Provide a globally unique storage account name
    - Select a region close to you (e.g., Canada Central)
3. After deployment completes:
    - Navigate to your Storage Account
    - Go to Access Keys under Security + networking
    - Copy:
        - Storage Account Name
        - Key1 (or connection string)
4. Update your application configuration in each project.

## Step 2

### Project Overview

#### 2.2.1_QueryMetadata

This project demonstrates how to retrieve and query blob metadata using the .NET SDK.

Key concepts covered:

    1. Accessing a blob container
    2. Retrieving blob properties and metadata
    3. Filtering or inspecting blobs based on metadata
    4. Understanding system properties vs user-defined metadata

#### 2.2.2_ManipulateData

This project focuses on working with blob data directly.

Key concepts covered:

    1. Creating containers and blobs
    2. Uploading data to blobs
    3. Downloading blob content
    4. Updating and deleting blobs
    5. Overwriting existing blobs and handling conflicts

#### 2.2.3_StorageTiers

This project demonstrates how to manage and modify blob access tiers using the .NET SDK.

Azure Blob Storage supports multiple access tiers to optimize for cost vs access frequency. This project shows how to:

    1. Retrieve the current access tier of a blob
    2. Change the tier programmatically
    3. Observe how tier transitions affect blob properties

## Step 3 — Run the Applications

From the project root, navigate to each project individually:

Run Query Metadata Project
``` 
cd 2.2.1_QueryMetadata
dotnet restore
dotnet build
dotnet run
```
Run Manipulate Data Project
```
cd 2.2.2_ManipulateData
dotnet restore
dotnet build
dotnet run
```

Run Storage Tiers Project
```
cd 2.2.3_StorageTiers
dotnet restore
dotnet build
dotnet run
```

Notes / Best Practices
- Use connection strings only for local development; in real applications, prefer:
    - Managed Identity
    - Azure Key Vault
- Blob Storage is optimized for:
    - Unstructured data (files, images, logs)
    - Large object storage
- Metadata:
    - Stored as key-value pairs
    - Case-insensitive keys
    - Not indexed (important for performance considerations)
- Access Tiers
    - Hot → Frequently accessed data (higher storage cost, lower access cost)
    - Cool → Infrequently accessed data (lower storage cost, higher access cost)
    - Archive → Rarely accessed data (lowest cost, but requires rehydration before access)
