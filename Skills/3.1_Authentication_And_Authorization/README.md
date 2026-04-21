## Prerequisites
- A Google account (for OAuth2 sign-in demo)

## Step 1 Configure Identity Providers
1.1 Configure Microsoft Entra ID
1. Go to Azure Portal
2. Navigate to Microsoft Entra ID
3. Register a new application:
    - Go to App registrations → New registration
    - Provide a name
    - Set redirect URI if required (for local testing, typically http://localhost)
4. After creation:
Copy:
    - Application (client) ID
    - Directory (tenant) ID
    - Create a client secret under Certificates & secrets

1.2 Configure Google OAuth2
1. Go to Google Cloud Console
2. Create a new project
3. Navigate to APIs & Services → Credentials
4. Create OAuth Client ID:
    - Application type: Web application
    - Add authorized redirect URIs (e.g., http://localhost)
5. Copy:
    - Client ID
    - Client Secret

## Step 2 Project Overview
### 3.1.1_ImplementOAuth2

This project demonstrates how to integrate an external identity provider (Google) using OAuth2.

#### Key concepts covered:

- Initiating OAuth2 authorization requests
- Handling redirect URIs and authorization codes
- Exchanging authorization codes for access tokens
- Using access tokens to call protected APIs
- Understanding the role of external identity providers

### 3.1.2_AuthorizationServer

This project implements a basic OAuth2 Authorization Server using Authorization Code Flow.

#### Key concepts covered:

- Implementing the Authorization Code Flow
- Issuing authorization codes and access tokens
- Handling client authentication
- Managing redirect URIs and scopes
- Understanding the responsibilities of an authorization server

### 3.1.3_GenerateSASToken

This project demonstrates how to securely generate and use a Shared Access Signature (SAS) using Azure identity.

#### Key concepts covered:

- Using Azure.Identity to authenticate with Microsoft Entra ID
- Generating a User Delegation Key
- Creating a SAS token with scoped permissions and expiration
- Uploading and downloading blobs using SAS
- Understanding the interaction between:
    - Entra ID authentication
    - Azure RBAC
    - SAS-based access

## Step 3 Run the Applications

From the project root, navigate to each project individually:

### Run OAuth2 Google Sign-In Project
1. Run the following commands:
```
cd 3.1.1_ImplementOAuth2
dotnet restore
dotnet build
dotnet run
```

1. Sign in using the Google provider.

### Run Authorization Server Project

1. Start the server by running the following commands:
```
cd 3.1.2_AuthorizationServer
dotnet restore
dotnet build
dotnet run
```

2. Navigate to 
<https://localhost:7011/connect/authorize?client_id=test-client&response_type=code&redirect_uri=https://localhost:5001/callback&scope=openid%20profile%20email&code_challenge=abc&code_challenge_method=plain>
3. Use a tool like Postman or a simple client to connect using the returned authorization code in the previous request:

*Type*: GET \
*Header*: Content-Type application/x-www-form-urlencoded \
*URL*: <https://localhost:7011/connect/token> \
*Body*:

> grant_type:authorization_code \
> client_id:test-client \
> client_secret:secret \
> code:<authorization-code> \
> redirect_uri:https://localhost:5001/callback \
> code_verifier:abc


### Run SAS Token Project

1. Replace the **StorageAccount** and **ContainerName** variables with your Azure storage account and container names.
2. Run the application by executing the following commands:
```
cd 3.1.3_GenerateSASToken
dotnet restore
dotnet build
dotnet run
```