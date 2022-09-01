using AzureServices.Models;

namespace AzureServices.SearchServices.Interfaces
{
    public interface ICognitiveSearch
    {
        /// <summary>
        /// Run the Azure Cognitive Search indexer.
        /// </summary>
        /// <param name="indexerName"></param>
        /// <returns></returns>
        Task<bool> RunIndexerAsync(string indexerName);

        /// <summary>
        /// To start the Azure Cognitive Search afresh. 
        /// This will create a new index, indexer, datasource, and run the new indexer, while delteting all previous components.
        /// </summary>
        /// <param name="dataSourceName"></param>
        /// <param name="indexName"></param>
        /// <param name="indexerName"></param>
        /// <param name="storageConnectionString"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<CognitiveEntities> SearchServiceInitializerAsync(string dataSourceName, string indexName, string indexerName, string storageConnectionString, string tableName);
    }
}
