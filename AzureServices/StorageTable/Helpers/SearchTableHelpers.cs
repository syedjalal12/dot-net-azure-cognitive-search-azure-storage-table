using AzureServices.Models;
using AzureServices.SearchServices;
using AzureServices.StorageTable.Helpers.Interfaces;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureServices.StorageTable.Helpers
{
    public class SearchTableHelpers : ISearchTableHelpers
    {
        public async Task reRunIndexer()
        {
            try
            {
                AzureConfig azureConfig = new AzureConfig();

                ISearchServiceClient searchServiceClient = new SearchServiceClient(azureConfig.SearchServiceName, new SearchCredentials(azureConfig.SearchServiceAdminApiKey));
                ISearchIndexClient searchIndexClient = IndexClient();

                CognitiveSearch cognitiveSearch = new CognitiveSearch(searchServiceClient, searchIndexClient);

                await cognitiveSearch.RunIndexerAsync(azureConfig.IndexerName);
            }
            catch (Exception ex)
            {
                var excption = new Exception($"\nAzureServices: SearchTableHelpers: reRunIndexer: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(excption.Message);
            }

        }

        public ISearchIndexClient IndexClient()
        {
            try
            {
                AzureConfig azureConfig = new AzureConfig();
                ISearchIndexClient searchIndexClient = new SearchIndexClient(azureConfig.SearchServiceName, azureConfig.IndexName, new SearchCredentials(azureConfig.SearchServiceAdminApiKey));
                return searchIndexClient;
            }
            catch (Exception ex)
            {
                var excption = new Exception($"\nAzureServices: SearchTableHelpers: IndexClient: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(excption.Message);
            }
        }

        public CustomerEntity converToGetTableResponse(DocumentSearchResult<CustomerEntity> documentSearchResult)
        {
            try
            {
                CustomerEntity customer = new CustomerEntity();

                foreach (SearchResult<CustomerEntity> data in documentSearchResult.Results)
                {
                    customer = data.Document;
                }

                return customer;
            }
            catch (Exception ex)
            {
                var excption = new Exception($"\nAzureServices: SearchTableHelpers: converToCustomerEntity: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(excption.Message);
            }
        }
    }
}
