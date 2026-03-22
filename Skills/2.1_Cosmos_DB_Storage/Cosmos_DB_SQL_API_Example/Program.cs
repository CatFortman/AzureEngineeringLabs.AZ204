using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Azure.Cosmos;

namespace Cosmos_DB_SQL_API_Example
{
    internal class Program
    {
        private const string ENPOINT_URI = "https://<your-cosmos-db-account>.documents.azure.com:443/";
        private const string KEY = "<your-primary-key>";
        private CosmosClient client;
        private Database database;
        private Container container;
        static void Main(string[] args)
        {
            Program program = new Program();
            program.StartDemo().GetAwaiter().GetResult();
            Console.WriteLine("Demo completed. Press any key to exit.");
            Console.ReadKey();
        }

        private async Task StartDemo()
        {
            Console.WriteLine("Starting Cosmos DB SQL API demo...");

            // Initialize Cosmos DB client and create database and container
            string databaseName = "DemoDatabase" + Guid.NewGuid().ToString("N").Substring(0, 5);
            this.SendMessageToConsoleAndWait($"Creating database: {databaseName}");

            client = new CosmosClient(ENPOINT_URI, KEY);
            database = await client.CreateDatabaseIfNotExistsAsync(databaseName);

            // Create a new demo collection (container)
            // This creates a collection with a reserved throughput of 400 RU/s. You can adjust this value based on your needs.
            string containerName = "collection" + Guid.NewGuid().ToString("N").Substring(0, 5);
            this.SendMessageToConsoleAndWait($"Creating container: {containerName}");
            container = await database.CreateContainerIfNotExistsAsync(containerName, "/id");

            // Create some documents in the collection
            Person newItem = new Person
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John Doe",
                Age = 30,
                Email = "john.doe@example.com",
                Devices = new Device[]
                {
                    new Device { Id = "device1", Type = "Laptop" },
                    new Device { Id = "device2", Type = "Smartphone" }
                },
                Gender = "Male",
                Address = new Address
                {
                    Street = "123 Main St",
                    City = "Anytown",
                    State = "CA",
                    ZipCode = "12345"
                },
                IsRegistered = true,
                RegistrationDate = DateTime.UtcNow
            };


            await this.CreateDocumentIfNotExistsAsync(databaseName, containerName, newItem);
            Console.WriteLine("Item created successfully.");
        }

        private async Task CreateDocumentIfNotExistsAsync(string databaseName, string containerName, Person person)
        {
            try
            {
                ItemResponse<Person> response = await container.ReadItemAsync<Person>(person.Id, new PartitionKey(person.Id));
                Console.WriteLine($"Document with id {person.Id} already exists. Skipping creation.");
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Document does not exist, create it
                ItemResponse<Person> response = await container.CreateItemAsync(person, new PartitionKey(person.Id));
                Console.WriteLine($"Document with id {person.Id} created successfully. Request charge: {response.RequestCharge} RU/s");
            }
        }

        private void SendMessageToConsoleAndWait(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    internal class Person
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public Device[] Devices { get; set; }
        public string? Gender { get; set; }
        public Address? Address { get; set; }
        public bool IsRegistered { get; set; }  
        public DateTime RegistrationDate { get; set; }
    }

    public class Address
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
    }

    public class Device
    {
        public string? Id { get; set; }
        public string? Type { get; set; }
    }
}