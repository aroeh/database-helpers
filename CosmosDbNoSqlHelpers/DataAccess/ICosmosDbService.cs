using Microsoft.Azure.Cosmos;

namespace CosmosDbNoSqlHelpers.DataAccess;

public interface ICosmosDbService
{
    /// <summary>
    /// Query items in the container using a query
    /// </summary>
    /// <typeparam name="T">Generic type of the object to return</typeparam>
    /// <param name="query">CosmosDb query command</param>
    /// <returns>List <T></returns>
    Task<List<T>> QueryItems<T>(string query);

    /// <summary>
    /// Create a new item in the cosmos db container
    /// </summary>
    /// <typeparam name="T">Generic type of the object to add</typeparam>
    /// <param name="item">Object to the add</param>
    /// <returns>ItemResponse<T></returns>
    Task<ItemResponse<T>> CreateItem<T>(T item);

    /// <summary>
    /// Replaces an existing item with a newer version of the item
    /// </summary>
    /// <typeparam name="T">Generic type of the object to replace</typeparam>
    /// <param name="item">New version of the object</param>
    /// <param name="id">Unique identifier for the object</param>
    /// <returns>ItemResponse<T></returns>
    Task<ItemResponse<T>> ReplaceItem<T>(T item, string id);
}
