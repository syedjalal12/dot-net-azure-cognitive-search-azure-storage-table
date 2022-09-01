using Microsoft.Extensions.Configuration;

namespace AzureServices
{
    public class AzureConfig
    {
        public AzureConfig()
        {
            var configuration = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configuration.AddJsonFile(path, false);
            var root = configuration.Build();

            SearchServiceUri = root.GetSection("AzureAccess:SearchServiceUri").Value;
            SearchServiceAdminApiKey = root.GetSection("AzureAccess:SearchServiceAdminApiKey").Value;
            SearchServiceName = root.GetSection("AzureAccess:SearchServiceName").Value;

            AzureStorageAccountName = root.GetSection("AzureAccess:AzureStorageAccountName").Value;
            AzureStorageConnectionString = root.GetSection("AzureAccess:AzureStorageConnectionString").Value;
            AzureStorageTableName = root.GetSection("AzureAccess:AzureStorageTableName").Value;

            IndexName = root.GetSection("AzureAccess:IndexName").Value;
            IndexerName = root.GetSection("AzureAccess:IndexerName").Value;
        }
        public string SearchServiceUri { get; set; }
        public string SearchServiceAdminApiKey { get; set; }
        public string SearchServiceName { get; set; }
        public string AzureStorageAccountName { get; set; }
        public string AzureStorageConnectionString { get; set; }
        public string AzureStorageTableName { get; set; }
        public string IndexName { get; set; }
        public string IndexerName { get; set; }
    }
}
