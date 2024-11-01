using CosmosDbNoSqlHelpers.Models;
using Microsoft.Azure.Cosmos;

namespace CosmosDbNoSqlHelpers.DataAccess;

// class setup using a primary constructor
public class RestuarantData(ILogger<RestuarantData> log, ICosmosWrapper cosmos) : IRestuarantData
{
    private readonly ILogger<RestuarantData> logger = log;
    private readonly ICosmosWrapper cosmosDb = cosmos;

    /// <summary>
    /// Returns a list of all restuarants in the database
    /// </summary>
    /// <returns>Collection of available restuarant records.  Returns empty list if there are no records</returns>
    public async Task<Restuarant[]> GetAllRestuarants()
    {
        string query = "SELECT * FROM c";

        logger.LogInformation("Finding all restuarants");
        IEnumerable<Restuarant> items = await cosmosDb.QueryItems<Restuarant>(query);
        return items.ToArray();
    }

    /// <summary>
    /// Simple method for finding restuarants by name and type of cuisine.
    /// This could be enhanced to include more criteria like location
    /// </summary>
    /// <param name="name">Search Parameter on the Restuarant Name</param>
    /// <param name="cuisine">Search Parameter on the Restuarant CuisineType</param>
    /// <returns>Collection of available restuarant records.  Returns empty list if there are no records found matching criteria</returns>
    public async Task<Restuarant[]> FindRestuarants(string name, string cuisine)
    {
        string query = $"SELECT * FROM c WHERE CONTAINS(c.name, '{name}') AND CONTAINS(c.CuisineType, '{cuisine}')";

        logger.LogInformation("Finding restuarants by name and cuisine type");
        IEnumerable<Restuarant> items = await cosmosDb.QueryItems<Restuarant>(query);
        return items.ToArray();
    }

    /// <summary>
    /// Retrieves a restuarant record based on the matching id
    /// </summary>
    /// <param name="id">Unique Identifier for a restuarant</param>
    /// <returns>Restuarant record if found.  Returns new Restuarant if not found</returns>
    public async Task<Restuarant> GetRestuarant(string id)
    {
        string query = $"SELECT * FROM c WHERE c.id = {id}";

        logger.LogInformation("Finding restuarant by id");
        Restuarant? restuarant = await cosmosDb.QueryItem<Restuarant>(query);

        if(restuarant is null)
        {
            return new Restuarant();
        }

        return restuarant;
    }

    /// <summary>
    /// Inserts a new Restuarant Record
    /// </summary>
    /// <param name="restuarant">Restuarant object to insert</param>
    /// <returns>Item Response object containing status of the insert operation</returns>
    public async Task<ItemResponse<Restuarant>> InsertRestuarant(Restuarant restuarant)
    {
        logger.LogInformation("Adding new restuarant");
        return await cosmosDb.CreateItem(restuarant);
    }

    /// <summary>
    /// Inserts many new Restuarant Records
    /// </summary>
    /// <param name="restuarants">Array of new restuarant objects to add</param>
    /// <returns>Item Response objects containing status of each insert operation</returns>
    public async Task<ItemResponse<Restuarant>[]> InsertRestuarants(Restuarant[] restuarants)
    {
        logger.LogInformation("Adding new restuarants");
        IEnumerable<ItemResponse<Restuarant>> results = await cosmosDb.CreateItems(restuarants);
        return results.ToArray();
    }

    /// <summary>
    /// Updates and existing restuarant record
    /// </summary>
    /// <param name="restuarant">Restuarant object to update</param>
    /// <returns>Item Response object containing status of the update operation</returns>
    public async Task<ItemResponse<Restuarant>> UpdateRestuarant(Restuarant restuarant)
    {
        logger.LogInformation("replacing restuarant document");
        return await cosmosDb.ReplaceItem(restuarant, restuarant.Id);
    }
}
