## Step 1
Verify Visual Studio Tools
1. Open Visual Studio 2022
2. Open the Tools menu
2. Click Get Tools & Features
3. Verify ASP.NET and Web Development in The Web & Cloud section is chedked
   
## Step 2
Create and run new web app in VS Code
1. Run `dotnet new web -n MySandboxWebApp`
2. Run `dotnet run`
3. Press Ctrl + C to stop the program

   
## Step 3
Create new Resource Group and App Service plan to host App Service
1. Sign into Azure using the Azure Resources view
2. In the terminal, create a resource group, App Service plan, and Web App:
> * `az group create --name MySandboxResourceGroup --location canadaeast` \
> * `az appservice plan create --name MySandboxAppServicePlan --resource-group MySandboxResourceGroup` \
> * `az webapp create --name MySandboxWebApp --resource-group MySandboxResourceGroup --plan MySandboxAppServicePlan` \
3. Publish and package the .NET web app:
> *  `dotnet publish -c Release -o ./publish` \
> `cd ./publish`
> `Compress-Archive -Path * -DestinationPath ../MySandboxWebApp.zip`
> `cd ..`
4. Deploy the zip file to Azure: 
> * `az webapp deploy --name MySandboxWebApp --resource-group MySandboxResourceGroup --src-path ./MySandboxWebApp.zip --type zip` 
