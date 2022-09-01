using Microsoft.Azure.Cosmos.Table;

namespace AzureServices.StorageTable
{
    public class StorageTableConfig
    {
        public StorageTableConfig()
        {
            AzureConfig azureConfig = new AzureConfig();

            CloudStorageAccount storageAccount;
            storageAccount = CloudStorageAccount.Parse(azureConfig.AzureStorageConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            table = tableClient.GetTableReference(azureConfig.AzureStorageTableName);
        }

        public CloudTable table;
    }
}
