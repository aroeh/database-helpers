using MongoDbHelpers.Models;

namespace MongoDbHelpers.Repos;

public interface IRestuarantRepo
{
    /// <summary>
    /// Retrieves all Restuarant from the database
    /// </summary>
    /// <returns>List of Restuarant objects</returns>
    Task<List<Restuarant>> GetAllRestuarants();

    /// <summary>
    /// Retrieves all Restuarant from the database matching search criteria
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cuisine"></param>
    /// <returns>List of Restuarant objects</returns>
    Task<List<Restuarant>> FindRestuarants(string name, string cuisine);

    /// <summary>
    /// Retrieves a Restuarant from the database by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Restuarant</returns>
    Task<Restuarant> GetRestuarant(string id);

    /// <summary>
    /// Inserts a new Restuarant record
    /// </summary>
    /// <param name="rest">Restuarant object to insert</param>
    /// <returns>Success status of the insert operation</returns>
    Task<bool> InsertRestuarant(Restuarant rest);

    /// <summary>
    /// Inserts a new Restuarant record
    /// </summary>
    /// <param name="restuarants">Restuarant array with many items to insert</param>
    /// <returns>Success status of the insert operation</returns>
    Task<bool> InsertRestuarants(Restuarant[] restuarants);

    /// <summary>
    /// Updates a Restuarant record
    /// </summary>
    /// <param name="rest">Restuarant object to update</param>
    /// <returns>Success status of the update operation</returns>
    Task<bool> UpdateRestuarant(Restuarant rest);
}
