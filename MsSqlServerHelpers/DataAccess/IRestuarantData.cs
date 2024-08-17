using MsSqlServerHelpers.Models;

namespace MsSqlServerHelpers.DataAccess
{
    public interface IRestuarantData
    {
        /// <summary>
        /// Returns a list of all restuarants in the database
        /// </summary>
        /// <returns>Collection of available restuarant records.  Returns empty list if there are no records</returns>
        List<Restuarant> GetAllRestuarants();

        /// <summary>
        /// This is an example showing how to write SQL and execute it
        /// Using the stored proc command is the preferred method for security and overall performance
        /// But this method may offer more flexibility if changing the database is difficult
        /// </summary>
        /// <returns>Collection of available restuarant records.  Returns empty list if there are no records</returns>
        List<Restuarant> GetAllRestuarantsByStatement();

        /// <summary>
        /// Simple method for finding restuarants by name and type of cuisine.
        /// This could be enhanced to include more criteria like location
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cuisine"></param>
        /// <returns>Collection of available restuarant records.  Returns empty list if there are no records found matching criteria</returns>
        List<Restuarant> FindRestuarants(string name, string cuisine);

        /// <summary>
        /// Simple method for finding restuarants by name and type of cuisine using a coded query
        /// This could be enhanced to include more criteria like location
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cuisine"></param>
        /// <returns>Collection of available restuarant records.  Returns empty list if there are no records found matching criteria</returns>
        List<Restuarant> FindRestuarantsByStatement(string name, string cuisine);

        /// <summary>
        /// Retrieves a restuarant record based on the matching id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Restuarant record if found.  Returns new Restuarant if not found</returns>
        Restuarant GetRestuarant(string id);

        /// <summary>
        /// Inserts a new Restuarant Record
        /// </summary>
        /// <param name="rest"></param>
        /// <returns>int - new restuarant id</returns>
        int InsertRestuarant(Restuarant rest);

        /// <summary>
        /// Updates and existing restuarant record
        /// </summary>
        /// <param name="rest"></param>
        /// <returns>int - number of rows affected</returns>
        int UpdateRestuarant(Restuarant rest);
    }
}
