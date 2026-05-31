## Step 1
Create a VM without remote access
1. Register an application in your azure subscription
2. Create a service policy on your application
3. Update the azureauth.properties file with your Azure subscription parameters
4. Use the dotnet run command in this skill folder to execute Program1
   
## Step 2
Create a VM with remote access
1. Comment out Program1
2. Uncomment Program2
3. Use the dotnet run command in this skill folder to execute Program2
   
## Step 3
Deploy a vm using an arm template
1. Review the vm arm template in the ArmTemplates folder 
2. Modify the vm-deployment-parameters.json file in the ArmTemplateParameters folder with your prefered values
3. Run the each command in the deploy-vm.bat script in Scripts folder
