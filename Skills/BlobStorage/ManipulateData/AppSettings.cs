using Microsoft.Extensions.Configuration;

namespace ManipulateData
{
    public class AppSettings
    {
        public string? SourceSASConnectionString { get; set; }
        public string? SourceStorageAccountName { get; set; }
        public string? SourceContainerName { get; set; }
        public string? DestinationSASConnectionString { get; set; }
        public string? DestinationStorageAccountName { get; set; }
        public string? DestinationContainerName { get; set; }

        public static AppSettings LoadFromFile()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("app.settings.json")
                            .Build() ?? throw new Exception("Failed to load configuration.");
                            
            var appSettings = configuration.Get<AppSettings>();     
            if(appSettings == null)
            {
                throw new Exception("Failed to load application settings.");
            }
            return appSettings;
        }   
    }   
}