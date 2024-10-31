using CosmosDbNoSqlHelpers.Models;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace CosmosDbNoSqlHelpers.DataAccess;

// class setup using a primary constructor
public class RestuarantData(ILogger<RestuarantData> log, ICosmosDbService cosmos) : IRestuarantData
{
    private readonly ILogger<RestuarantData> logger = log;
    private readonly ICosmosDbService cosmosDb = cosmos;

    /// <summary>
    /// Returns a list of all restuarants in the database
    /// </summary>
    /// <returns>Collection of available restuarant records.  Returns empty list if there are no records</returns>
    public async Task<List<Restuarant>> GetAllRestuarants()
    {
        string query = "SELECT * FROM c";

        logger.LogInformation("Finding all restuarants");
        return await cosmosDb.QueryItems<Restuarant>(query);
    }

    /// <summary>
    /// Simple method for finding restuarants by name and type of cuisine.
    /// This could be enhanced to include more criteria like location
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cuisine"></param>
    /// <returns>Collection of available restuarant records.  Returns empty list if there are no records found matching criteria</returns>
    public async Task<List<Restuarant>> FindRestuarants(string name, string cuisine)
    {
        string query = $"SELECT * FROM c WHERE CONTAINS(c.name, '{name}') AND CONTAINS(c.CuisineType, '{cuisine}')";

        logger.LogInformation("Finding restuarants by name and cuisine type");
        return await cosmosDb.QueryItems<Restuarant>(query);
    }

    /// <summary>
    /// Retrieves a restuarant record based on the matching id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Restuarant record if found.  Returns new Restuarant if not found</returns>
    public async Task<Restuarant> GetRestuarant(string id)
    {
        string query = $"SELECT * FROM c WHERE c.id = {id}";

        logger.LogInformation("Finding restuarant by id");
        List<Restuarant> items = await cosmosDb.QueryItems<Restuarant>(query);

        if(items == null || items.Count == 0)
        {
            return new Restuarant();
        }

        return items.First();
    }

    /// <summary>
    /// Inserts a new Restuarant Record
    /// </summary>
    /// <param name="rest"></param>
    /// <returns>HttpStatusCode</returns>
    public async Task<HttpStatusCode> InsertRestuarant(Restuarant rest)
    {
        logger.LogInformation("Adding new restuarant");
        ItemResponse<Restuarant> result = await cosmosDb.CreateItem(rest);

        return result.StatusCode;
    }

    /// <summary>
    /// Updates and existing restuarant record
    /// </summary>
    /// <param name="rest"></param>
    /// <returns>HttpStatusCode</returns>
    public async Task<HttpStatusCode> UpdateRestuarant(Restuarant rest)
    {
        logger.LogInformation("replacing restuarant document");
        ItemResponse<Restuarant> result = await cosmosDb.ReplaceItem(rest, rest.Id);

        return result.StatusCode;
    }
}
