using Microsoft.Azure.Management.Compute.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.Network.Fluent.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

// Create a VM with remote access
namespace step2
{

    class Program2
    {
        // static void Main(string[] args)
        // {
        //     // create the management client. This will be used for all the operations
        //     // performed in Azure.
        //     var credentials = SdkContext.AzureCredentialsFactory.FromFile("./azureauth.properties");

        //     var azure = Azure.Configure()
        //     .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
        //     .Authenticate(credentials)
        //     .WithDefaultSubscription();

        //     // We need tocreate a resource group where we will add all the resources.
        //     // Needed for the virtual machine.
        //     var groupName = "az04-ResourceGroup";
        //     var vmName = "az04VMTesting";
        //     var location = Region.USEast2;
        //     var vNetName = "az204VNet";
        //     var vNetAddress = "172.16.0.0/16";
        //     var subnetName = "az204Subnet";
        //     var subnetAddress = "172.16.0.0/24";
        //     var nicName = "az204NIC";
        //     var adminUser = "azureadminuser";
        //     var password = "pa$$w0rd!2019";
        //     var publicIPName = "az204PublicIP";
        //     var nsgName = "az204VNET-NSG";

        //     System.Console.WriteLine($"Creating resource group {groupName}...");
        //     var resourceGroup = azure.ResourceGroups.Define(groupName)
        //     .WithRegion(location)
        //     .Create();

        //     // Every vm needs to be connected to virtual network.
        //     System.Console.WriteLine($"Creating vm network {vNetName}...");
        //     var network = azure.Networks.Define(vNetName)
        //     .WithRegion(location)
        //     .WithExistingResourceGroup(groupName)
        //     .WithAddressSpace(vNetAddress)
        //     .WithSubnet(subnetName, subnetAddress)
        //     .Create();

        //     // You need a public IP to be able to connect to the VM from the internet.
        //     Console.WriteLine($"Creating public IP {publicIPName} ...");
        //     var publicIp = azure.PublicIPAddresses.Define(publicIPName)
        //     .WithRegion(location)
        //     .WithExistingResourceGroup(groupName)
        //     .Create();

        //     // You need a netwprk security group to control access to the VM.
        //     Console.WriteLine($"Network security group {nsgName} ...");
        //     var nsg = azure.NetworkSecurityGroups.Define(nsgName)
        //     .WithRegion(location)
        //     .WithExistingResourceGroup(groupName)
        //     .Create();

        //     // You need a security rule for allowing access to the VM from the internet.
        //     Console.WriteLine($"Creating a security rule for allowing the remote access ...");
        //     nsg.Update()
        //     .DefineRule("Allow-RDP")
        //     .AllowInbound()
        //     .FromAnyAddress()
        //     .FromAnyPort()
        //     .ToAnyAddress()
        //     .ToPort(3389)
        //     .WithProtocol(SecurityRuleProtocol.Tcp)
        //     .WithPriority(100)
        //     .WithDescription("Allow-RDP")
        //     .Attach()
        //     .Apply();

        //     // Any virtual machine needs a network interface for connecting to
        //     // the virtual network.
        //     System.Console.WriteLine($"Creating network interface {nicName}...");
        //     var nic = azure.NetworkInterfaces.Define(nicName)
        //     .WithRegion(location)
        //     .WithExistingResourceGroup(groupName)
        //     .WithExistingPrimaryNetwork(network)
        //     .WithSubnet(subnetName)
        //     .WithPrimaryPrivateIPAddressDynamic()
        //     .WithExistingPrimaryPublicIPAddress(publicIp)
        //     .WithExistingNetworkSecurityGroup(nsg)
        //     .Create();

        //     // Create the vm.
        //     System.Console.WriteLine($"Creating virtual machine {vmName}...");
        //     azure.VirtualMachines.Define(vmName)
        //     .WithRegion(location)
        //     .WithExistingResourceGroup(groupName)
        //     .WithExistingPrimaryNetworkInterface(nic)
        //     .WithLatestWindowsImage("MicrosoftWindowsServer", "WindowsServer", "2012-R2-DataCenter")
        //     .WithAdminUsername(adminUser)
        //     .WithAdminPassword(password)
        //     .WithComputerName(vmName)
        //     .WithSize(VirtualMachineSizeTypes.StandardDS2V2)
        //     .Create();
        // }
    }
}