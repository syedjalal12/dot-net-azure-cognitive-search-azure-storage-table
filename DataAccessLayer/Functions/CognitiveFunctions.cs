using AzureServices.SearchServices;
using AzureServices;
using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Functions.Interfaces;
using DataAccessLayer.Models;
using CognitiveHelpers = DataAccessLayer.Helpers.CognitiveHelpers;

namespace DataAccessLayer.Functions
{
    public class CognitiveFunctions : ICognitiveFunctions
    {
        public async Task<CognitiveEntities> InitializeCognitiveSearch()
        {
            try
            {
                AzureConfig azureConfig = new AzureConfig();

                ISearchServiceClient searchServiceClient = new SearchServiceClient(azureConfig.SearchServiceName, new SearchCredentials(azureConfig.SearchServiceAdminApiKey));
                ISearchIndexClient searchIndexClient = new SearchIndexClient(azureConfig.SearchServiceName, azureConfig.IndexName, new SearchCredentials(azureConfig.SearchServiceAdminApiKey));

                CognitiveSearch cognitiveSearch = new CognitiveSearch(searchServiceClient, searchIndexClient);

                var cognitiveEntities = await cognitiveSearch.SearchServiceInitializerAsync(azureConfig.AzureStorageAccountName, azureConfig.IndexName, azureConfig.IndexerName, azureConfig.AzureStorageConnectionString, azureConfig.AzureStorageTableName);

                CognitiveEntities response = CognitiveHelpers.ConvertCognitiveEntities(cognitiveEntities);
                
                return response;
            }
            catch (Exception ex)
            {
                var excption = new Exception($"\nDataAccessLayer: CognitiveFunctions: InitializeCognitiveSearch: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(excption.Message);
            }
        }
    }
}
