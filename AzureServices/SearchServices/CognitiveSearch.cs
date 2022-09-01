using AzureServices.SearchServices.Interfaces;
using Microsoft.Azure.Search;
using Index = Microsoft.Azure.Search.Models.Index;
using DataSource = Microsoft.Azure.Search.Models.DataSource;
using Microsoft.Azure.Search.Models;
using AzureServices.Models;
using System.Reflection.Metadata.Ecma335;

namespace AzureServices.SearchServices
{
    public class CognitiveSearch : ICognitiveSearch
    {

        /// <summary>
        /// Instance of Azure Search service client.
        /// </summary>
        private readonly ISearchServiceClient searchServiceClient;

        /// <summary>
        /// Instance of Azure Search index client.
        /// </summary>
        private readonly ISearchIndexClient searchIndexClient;

        /// <summary>
        /// CognitiveSearch cunstructor
        /// </summary>
        /// <param name="searchServiceClient"></param>
        /// <param name="searchIndexClient"></param>
        public CognitiveSearch(ISearchServiceClient searchServiceClient, ISearchIndexClient searchIndexClient)
        {
            this.searchServiceClient = searchServiceClient;
            this.searchIndexClient = searchIndexClient;
        }


        /// <summary>
        /// Creates an Azure Cognitive Search index
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<bool> CreateIndexAsync(string indexName)
        {
            try
            {
                var tableIndex = new Index()
                {
                    Name = indexName,
                    Fields = FieldBuilder.BuildForType<CustomerEntity>(),
                };
                await this.searchServiceClient.Indexes.CreateAsync(tableIndex);
                System.Threading.Thread.Sleep(2000);
                //Console.WriteLine($"{indexName}\t--INDEX Created");

                return await this.searchServiceClient.Indexes.ExistsAsync(indexName);
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nAzureServices: CognitiveSearch: CreateIndexAsync: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        /// <summary>
        /// Creates an Azure Cognitive Search indexer
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="indexerName"></param>
        /// <param name="dataSourceName"></param>
        /// <returns></returns>
        public async Task<bool> CreateIndexerAsync(string indexName, string indexerName, string dataSourceName)
        {
            try
            {
                var indexer = new Indexer()
                {
                    Name = indexerName,
                    DataSourceName = dataSourceName,
                    TargetIndexName = indexName,
                };

                await this.searchServiceClient.Indexers.CreateAsync(indexer);
                System.Threading.Thread.Sleep(2000);
                //Console.WriteLine($"{indexerName}\t--INDEXER Created");


                //await this.searchServiceClient.Indexers.RunAsync(indexerName);
                //System.Threading.Thread.Sleep(2000);
                //Console.WriteLine($"{indexerName}\t--INDEXER Running");

                return await this.searchServiceClient.Indexers.ExistsAsync(indexerName);
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nAzureServices: CognitiveSearch: CreateIndexerAsync: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        /// <summary>
        /// Creates a data source for Azure Cognitive Search indexer
        /// </summary>
        /// <param name="dataSourceName"></param>
        /// <param name="storageConnectionString"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public async Task<bool> CreateDataSourceAsync(string dataSourceName, string storageConnectionString, string tableName)
        {
            try
            {
                var dataSource = DataSource.AzureTableStorage(
                dataSourceName,
                storageConnectionString,
                tableName,
                query: null);
                //new SoftDeleteColumnDeletionDetectionPolicy("isActive", false));

                await this.searchServiceClient.DataSources.CreateAsync(dataSource);
                System.Threading.Thread.Sleep(2000);
                //Console.WriteLine($"{dataSourceName}\t--DATASOURCE Created");
                return await this.searchServiceClient.DataSources.ExistsAsync(dataSourceName);
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nAzureServices: CognitiveSearch: CreateDataSourceAsync: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        /// <summary>
        /// Manually runs the Azure Cognitive Search indexer
        /// </summary>
        /// <param name="indexerName"></param>
        /// <returns></returns>
        public async Task<bool> RunIndexerAsync(string indexerName)
        {
            try
            {
                await this.searchServiceClient.Indexers.RunAsync(indexerName);
                System.Threading.Thread.Sleep(2000);
                //Console.WriteLine($"{indexerName}\t--INDEXER Running");
                var indexerStatus = await this.searchServiceClient.Indexers.GetStatusAsync(indexerName);
                var status = indexerStatus.Status;

                return status.ToString() == "Running";
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nAzureServices: CognitiveSearch: RunIndexerAsync: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        /// <summary>
        /// Deletes an Azure Cognitive Search index
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<bool> DeleteIndexAsync(string indexName)
        {
            try
            {
                await this.searchServiceClient.Indexes.DeleteAsync(indexName);
                //System.Threading.Thread.Sleep(2000);
                //Console.WriteLine($"{indexName}\t--INDEX Deleted");

                return !await this.searchServiceClient.Indexes.ExistsAsync(indexName);
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nAzureServices: CognitiveSearch: DeleteIndexAsync: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        /// <summary>
        /// Deletes an Azure Cognitive Search indexer
        /// </summary>
        /// <param name="indexerName"></param>
        /// <returns></returns>
        public async Task<bool> DeleteIndexerAsync(string indexerName)
        {
            try
            {
                await this.searchServiceClient.Indexers.DeleteAsync(indexerName);
                //System.Threading.Thread.Sleep(2000);
                //Console.WriteLine($"{indexerName}\t--INDEXER Deleted");

                return !await this.searchServiceClient.Indexers.ExistsAsync(indexerName);
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nAzureServices: CognitiveSearch: DeleteIndexerAsync: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        /// <summary>
        /// Deletes an Azure Cognitive Search DataSource
        /// </summary>
        /// <param name="indexedataSourceName"></param>
        /// <returns></returns>
        public async Task<bool> DeleteDataSourceAsync(string dataSourceName)
        {
            try
            {
                await this.searchServiceClient.DataSources.DeleteAsync(dataSourceName);
                //System.Threading.Thread.Sleep(2000);
                //Console.WriteLine($"{dataSourceName}\t--DATASOURCE Deleted");

                return !await this.searchServiceClient.DataSources.ExistsAsync(dataSourceName);
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nAzureServices: CognitiveSearch: DeleteDataSourceAsync: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        
        public async Task<CognitiveEntities> SearchServiceInitializerAsync(string dataSourceName, string indexName, string indexerName, string storageConnectionString, string tableName)
        {
            try
            {
                if (await this.searchServiceClient.DataSources.ExistsAsync(dataSourceName))
                {
                    await DeleteDataSourceAsync(dataSourceName);
                }
                if (await this.searchServiceClient.Indexes.ExistsAsync(indexName))
                {
                    await DeleteIndexAsync(indexName);
                }
                if (await this.searchServiceClient.Indexers.ExistsAsync(indexerName))
                {
                    await DeleteIndexerAsync(indexerName);
                }

                bool indexExists = await CreateIndexAsync(indexName);
                bool dataSourceExists = await CreateDataSourceAsync(dataSourceName, storageConnectionString, tableName);
                bool indexerExists = await CreateIndexerAsync(indexName, indexerName, dataSourceName);
                bool indexRunning = await RunIndexerAsync(indexerName);

                var indexerStatus = this.searchServiceClient.Indexers.GetStatusAsync(indexerName);

                CognitiveEntities cognitiveEntities = new CognitiveEntities
                {
                    IndexCreated = indexExists,
                    IndexerCreated = dataSourceExists,
                    DataSourceCreated = indexerExists,
                    IndexerRunning = indexRunning,
                };

                return cognitiveEntities;

            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nAzureServices: CognitiveSearch: SearchServiceInitializerAsync: FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }
    }
}
