﻿using CosmosDbNoSqlHelpers.Constants;
using Microsoft.Azure.Cosmos;

namespace CosmosDbNoSqlHelpers.DataAccess
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly string endpoint;
        private readonly string primaryKey;
        
        private readonly CosmosClient cosmosClient;
        private Database database;
        private Container container;

        public CosmosDbService()
        {
            endpoint = Environment.GetEnvironmentVariable(DataAccessConstants.CosmosEndpointUri);
            primaryKey = Environment.GetEnvironmentVariable(DataAccessConstants.CosmosPrimaryKey);

            CosmosClientOptions options = new()
            {
                ApplicationName = "NoSqlCosmosDbDemo",
                ConnectionMode = ConnectionMode.Direct
            };
            cosmosClient = new(endpoint, primaryKey, options);

            CreateDatabaseIfNotExists().Wait();
            CreateContainerIfNotExists().Wait();
        }

        /// <summary>
        /// Ensure that the database exists in CosmosDb
        /// </summary>
        /// <returns></returns>
        private async Task CreateDatabaseIfNotExists()
        {
            database = await cosmosClient.CreateDatabaseIfNotExistsAsync(DataAccessConstants.DatabaseName);
        }

        /// <summary>
        /// Ensure that the Container for storing items exists in CosmosDb
        /// </summary>
        /// <returns></returns>
        private async Task CreateContainerIfNotExists()
        {
            container = await database.CreateContainerIfNotExistsAsync(DataAccessConstants.ContainerName, DataAccessConstants.PartitionKey);
        }

        /// <summary>
        /// Query items in the container using a query
        /// </summary>
        /// <typeparam name="T">Generic type of the object to return</typeparam>
        /// <param name="query">CosmosDb query command</param>
        /// <returns>List <T></returns>
        public async Task<List<T>> QueryItems<T>(string query)
        {
            // Create a QueryDefinition
            QueryDefinition queryDefinition = new(query);

            // Create an item iterator to read results
            FeedIterator<T> feedIterator = container.GetItemQueryIterator<T>(queryDefinition);

            // instantiate a collection of objects for the return output
            List<T> results = [];

            // loop through the iterator as long as there are more results found
            while (feedIterator.HasMoreResults)
            {
                // read the next result set in the iterator
                FeedResponse<T> feedResponse = await feedIterator.ReadNextAsync();

                // loop through items in the response and add them to the results collection
                foreach(T item in feedResponse)
                {
                    results.Add(item);
                }
            }

            return results;
        }

        /// <summary>
        /// Create a new item in the cosmos db container
        /// </summary>
        /// <typeparam name="T">Generic type of the object to add</typeparam>
        /// <param name="item">Object to the add</param>
        /// <returns>ItemResponse<T></returns>
        public async Task<ItemResponse<T>> CreateItem<T>(T item)
        {
            ItemResponse<T> response = await container.CreateItemAsync(item);
            return response;
        }

        /// <summary>
        /// Replaces an existing item with a newer version of the item
        /// </summary>
        /// <typeparam name="T">Generic type of the object to replace</typeparam>
        /// <param name="item">New version of the object</param>
        /// <param name="id">Unique identifier for the object</param>
        /// <returns>ItemResponse<T></returns>
        public async Task<ItemResponse<T>> ReplaceItem<T>(T item, string id)
        {
            ItemResponse<T> replaceResult = await container.ReplaceItemAsync(item, id);
            return replaceResult;
        }
    }
}
