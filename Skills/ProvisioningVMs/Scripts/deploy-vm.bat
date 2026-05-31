::Azure cli template deployment

::login to azure
az login

az group create --name AZ204-ResourceGroupDemo --location eastus
az deployment group create --resource-group AZ204-ResourceGroupDemo --name AZ204DemoDeployment --template-file ../ArmTemplates/vm-deployment-template.json --parameters @../ArmTemplateParameters/vm-deployment-parameters.json