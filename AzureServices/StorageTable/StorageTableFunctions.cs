using AzureServices.Models;
using AzureServices.StorageTable.Helpers;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace AzureServices.StorageTable
{
    public class StorageTableFunctions
    {
        public async Task<CustomerEntity> AddDataToTable(CustomerEntity data)
        {
            try
            {
                SearchTableHelpers searchTableHelper = new SearchTableHelpers();

                StorageTableConfig storageTable = new StorageTableConfig();
                var table = storageTable.table;

                TableOperation insertOrMergeOperation = TableOperation.Insert(data);
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);

                await searchTableHelper.reRunIndexer();

                //bool solution = result.HttpStatusCode == 200;
                //return solution;
                return data;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nAzureServices: StorageTableFunctions: AddDataToTable: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public async Task<CustomerEntity> UpdateDataInTable(CustomerEntity data)
        {
            try
            {
                SearchTableHelpers searchTableHelpers = new SearchTableHelpers();

                StorageTableConfig storageTable = new StorageTableConfig();
                var table = storageTable.table;

                data.ETag = "*";

                TableOperation replaceOperation = TableOperation.Replace(data);
                TableResult result = await table.ExecuteAsync(replaceOperation);

                await searchTableHelpers.reRunIndexer();

                return data;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nAzureServices: StorageTableFunctions: UpdateDataInTable: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public async Task<CustomerEntity> GetTableData(string id)
        {
            try
            {
                SearchTableHelpers searchTableHelper = new SearchTableHelpers();

                SearchParameters parameters =
                    new SearchParameters()
                    {
                        Select = new[] { "PartitionKey", "RowKey", "Id", "Name", "Email", "PhoneNumber", "isActive" },
                        Top = 1
                    };

                DocumentSearchResult<CustomerEntity> result = await searchTableHelper.IndexClient().Documents.SearchAsync<CustomerEntity>(id, parameters);
                CustomerEntity customer = searchTableHelper.converToGetTableResponse(result);

                if (customer.isActive)
                {
                    return customer;
                }
                throw new Exception($"\n{id} is invalid.\n");
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nAzureServices: StorageTableFunctions: GetTableData: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public async Task<DocumentSearchResult<CustomerEntity>> GetAllTableData()
        {
            try
            {
                SearchTableHelpers searchTableHelpers = new SearchTableHelpers();

                SearchParameters parameters =
                    new SearchParameters()
                    {
                        Select = new[] { "PartitionKey", "RowKey", "Id", "Name", "Email", "PhoneNumber", "isActive" },
                    };

                DocumentSearchResult<CustomerEntity> results = await searchTableHelpers.IndexClient().Documents.SearchAsync<CustomerEntity>("*", parameters);

                return results;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nAzureServices: StorageTableFunctions: GetAllTableData: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }
    }
}
