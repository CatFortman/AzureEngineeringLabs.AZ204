using Microsoft.Extensions.Configuration;

namespace QueryMetadata
{
    public class AppSettings
    {
        public string? SASToken { get; set; }
        public string? StorageAccountName { get; set; }
        public string? ContainerName { get; set; }

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