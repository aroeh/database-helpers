using MsSqlServerHelpers.Models;

namespace MsSqlServerHelpers.Repos;

public interface IRestuarantRepo
{
    /// <summary>
    /// Retrieves all Restuarant from the database
    /// </summary>
    /// <returns>Array of Restuarant objects</returns>
    Task<Restuarant[]> GetAllRestuarants();

    /// <summary>
    /// Retrieves all Restuarant from the database matching search criteria
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cuisine"></param>
    /// <returns>Array of Restuarant objects</returns>
    Task<Restuarant[]> FindRestuarants(string name, string cuisine);

    /// <summary>
    /// Retrieves all Restuarant from the database using a query coded on the API
    /// </summary>
    /// <returns>Array of Restuarant objects</returns>
    Task<Restuarant[]> GetAllRestuarantsUsingQuery();

    /// <summary>
    /// Retrieves all Restuarant from the database matching search criteria using a query coded on the API
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cuisine"></param>
    /// <returns>Array of Restuarant objects</returns>
    Task<Restuarant[]> FindRestuarantsUsingQuery(string name, string cuisine);

    /// <summary>
    /// Retrieves a Restuarant from the database by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>bool indicating the success of the operatoin</returns>
    Task<Restuarant> GetRestuarant(string id);

    /// <summary>
    /// Inserts a new Restuarant record
    /// </summary>
    /// <param name="rest"></param>
    /// <returns>bool indicating the success of the operatoin</returns>
    Task<bool> InsertRestuarant(Restuarant rest);

    /// <summary>
    /// Inserts many new Restuarant records
    /// </summary>
    /// <param name="restuarants">Array of new restuarant objects to add</param>
    /// <returns>bool indicating the success of the operatoin</returns>
    Task<bool> InsertRestuarants(Restuarant[] restuarants);

    /// <summary>
    /// Updates a Restuarant record
    /// </summary>
    /// <param name="rest"></param>
    /// <returns>bool indicating the success of the operatoin</returns>
    Task<bool> UpdateRestuarant(Restuarant rest);
}
