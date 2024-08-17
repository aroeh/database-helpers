using MsSqlServerHelpers.DataAccess;
using MsSqlServerHelpers.Models;

namespace MsSqlServerHelpers.Repos
{
    // class setup using a primary constructor
    public class RestuarantRepo(ILogger<RestuarantRepo> log, IRestuarantData data) : IRestuarantRepo
    {
        private readonly ILogger<RestuarantRepo> logger = log;
        private readonly IRestuarantData restuarantData = data;

        /// <summary>
        /// Retrieves all Restuarant from the database
        /// </summary>
        /// <returns>List<Restuarant></returns>
        public Task<List<Restuarant>> GetAllRestuarants()
        {
            logger.LogInformation("Initiating get all restuarants");
            List<Restuarant> restuarants = restuarantData.GetAllRestuarants();

            return Task.FromResult(restuarants);
        }

        /// <summary>
        /// Retrieves all Restuarant from the database matching search criteria
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cuisine"></param>
        /// <returns>List<Restuarant></returns>
        public Task<List<Restuarant>> FindRestuarants(string name, string cuisine)
        {
            logger.LogInformation("Initiating find restuarants");

            // call the data layer using a stored procedure
            List<Restuarant> restuarants = restuarantData.FindRestuarants(name, cuisine);

            // call the data layer using a query
            //List<Restuarant> restuarants = restuarantData.FindRestuarantsByStatement(name, cuisine);

            return Task.FromResult(restuarants);
        }

        /// <summary>
        /// Retrieves all Restuarant from the database using a query coded on the API
        /// </summary>
        /// <returns>List<Restuarant></returns>
        public Task<List<Restuarant>> GetAllRestuarantsUsingQuery()
        {
            logger.LogInformation("Initiating get all restuarants using query");
            List<Restuarant> restuarants = restuarantData.GetAllRestuarantsByStatement();

            return Task.FromResult(restuarants);
        }

        /// <summary>
        /// Retrieves all Restuarant from the database matching search criteria using a query coded on the API
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cuisine"></param>
        /// <returns>List<Restuarant></returns>
        public Task<List<Restuarant>> FindRestuarantsUsingQuery(string name, string cuisine)
        {
            logger.LogInformation("Initiating find restuarants using query");
            List<Restuarant> restuarants = restuarantData.FindRestuarantsByStatement(name, cuisine);

            return Task.FromResult(restuarants);
        }

        /// <summary>
        /// Retrieves a Restuarant from the database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Restuarant</returns>
        public Task<Restuarant> GetRestuarant(string id)
        {
            logger.LogInformation("Initiating get restuarant by id");
            Restuarant restuarant = restuarantData.GetRestuarant(id);

            if (restuarant == null || string.IsNullOrWhiteSpace(restuarant.Id) || restuarant.Id == "0")
            {
                return Task.FromResult(new Restuarant());
            }

            return Task.FromResult(restuarant);
        }

        /// <summary>
        /// Inserts a new Restuarant record
        /// </summary>
        /// <param name="rest"></param>
        /// <returns>bool</returns>
        public Task<bool> InsertRestuarant(Restuarant rest)
        {
            logger.LogInformation("Adding new restuarant");
            int newRestuarantId = restuarantData.InsertRestuarant(rest);

            logger.LogInformation("Checking insert operation result");
            // we could return the id as part of the result and there are many cases where you might need to do that
            // but depending on the scenario that could potentially expose data you might not want a client seeing
            return Task.FromResult(newRestuarantId > 0);
        }

        /// <summary>
        /// Updates a Restuarant record
        /// </summary>
        /// <param name="rest"></param>
        /// <returns>bool</returns>
        public Task<bool> UpdateRestuarant(Restuarant rest)
        {
            logger.LogInformation("Updating restuarant");
            int rowsAffected = restuarantData.UpdateRestuarant(rest);

            logger.LogInformation("Checking update operation result");
            return Task.FromResult(rowsAffected > 0);
        }
    }
}
