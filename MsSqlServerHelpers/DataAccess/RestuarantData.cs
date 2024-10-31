using MsSqlServerHelpers.Constants;
using MsSqlServerHelpers.Mappers;
using MsSqlServerHelpers.Models;
using System.Data;
using System.Data.SqlClient;

namespace MsSqlServerHelpers.DataAccess;

// class setup using a primary constructor
public class RestuarantData(ILogger<RestuarantData> log, ISqlHelperService sql) : IRestuarantData
{
    private readonly ILogger<RestuarantData> logger = log;
    private readonly ISqlHelperService sqlHelper = sql;


    /// <summary>
    /// Returns a collection of all restuarants in the database
    /// </summary>
    /// <returns>Collection of available restuarant records.  Returns empty array if there are no records</returns>
    public Restuarant[] GetAllRestuarants()
    {
        DataTable table = sqlHelper.Select(DataAccessConstants.DefaultSchema, DataAccessConstants.GetAllRestuarants);

        bool hasData = sqlHelper.DateTableHasData(table);

        // if there is no data then return an empty list
        if (!hasData)
        {
            return [];
        }

        return RestuarantMapper.MapRestuarantsAndLocation(table);
    }

    /// <summary>
    /// This is an example showing how to write SQL and execute it
    /// Using the stored proc command is the preferred method for security and overall performance
    /// But this method may offer more flexibility if changing the database is difficult
    /// </summary>
    /// <returns>Collection of available restuarant records.  Returns empty array if there are no records</returns>
    public Restuarant[] GetAllRestuarantsByStatement()
    {
        string query = @"SELECT
			        r.Id,
                    r.Name,
                    r.CuisineType,
                    r.Website,
                    r.Phone,
                    rl.Street,
                    rl.City,
                    rl.[State],
                    rl.ZipCode,
                    rl.Country
		        FROM [dbo].[Restuarants] r
			        INNER JOIN [dbo].[RestuarantLocation] rl
				        ON rl.RestuarantId = r.Id";

        DataTable table = sqlHelper.Select(query);

        bool hasData = sqlHelper.DateTableHasData(table);

        // if there is no data then return an empty list
        if (!hasData)
        {
            return [];
        }

        return RestuarantMapper.MapRestuarantsAndLocation(table);
    }

    /// <summary>
    /// Simple method for finding restuarants by name and type of cuisine.
    /// This could be enhanced to include more criteria like location
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cuisine"></param>
    /// <returns>Collection of available restuarant records.  Returns empty array if there are no records found matching criteria</returns>
    public Restuarant[] FindRestuarants(string name, string cuisine)
    {
        SqlParameter[] parameters =
        [
            new("@Name", name),
            new("@Cuisine", cuisine)
        ];
        DataTable table = sqlHelper.Select(DataAccessConstants.DefaultSchema, DataAccessConstants.FindRestuarants, parameters);

        bool hasData = sqlHelper.DateTableHasData(table);

        // if there is no data then return an empty list
        if (!hasData)
        {
            return [];
        }

        return RestuarantMapper.MapRestuarantsAndLocation(table);
    }

    /// <summary>
    /// Simple method for finding restuarants by name and type of cuisine using a coded query
    /// 
    /// This is an example showing how to write SQL that takes parameters and execute it
    /// Using the stored proc command is the preferred method for security and overall performance
    /// But this method may offer more flexibility if changing the database is difficult
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cuisine"></param>
    /// <returns>Collection of available restuarant records.  Returns empty array if there are no records found matching criteria</returns>
    public Restuarant[] FindRestuarantsByStatement(string name, string cuisine)
    {
        string query = @"SELECT
			        r.Id,
                    r.Name,
                    r.CuisineType,
                    r.Website,
                    r.Phone,
                    rl.Street,
                    rl.City,
                    rl.[State],
                    rl.ZipCode,
                    rl.Country
		        FROM [dbo].[Restuarants] r
			        INNER JOIN [dbo].[RestuarantLocation] rl
				        ON rl.RestuarantId = r.Id
                WHERE r.Name LIKE '%' + @Name +'%'
                    AND r.CuisineType LIKE '%' + @Cuisine +'%'";

        SqlParameter[] parameters =
        [
            new("@Name", name),
            new("@Cuisine", cuisine)
        ];
        DataTable table = sqlHelper.Select(query, parameters);

        bool hasData = sqlHelper.DateTableHasData(table);

        // if there is no data then return an empty list
        if (!hasData)
        {
            return [];
        }

        return RestuarantMapper.MapRestuarantsAndLocation(table);
    }

    /// <summary>
    /// Retrieves a restuarant record based on the matching id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Restuarant record if found.  Returns new Restuarant if not found</returns>
    public Restuarant GetRestuarant(string id)
    {
        SqlParameter[] parameters =
        [
            new("@Id", id)
        ];
        DataRow row = sqlHelper.SelectOne(DataAccessConstants.DefaultSchema, DataAccessConstants.GetRestuarantById, parameters);

        bool hasData = sqlHelper.DateRowHasData(row);

        // if there is no data then return an empty list
        if (!hasData)
        {
            return new Restuarant();
        }

        return RestuarantMapper.MapRestuarantAndLocation(row);
    }

    /// <summary>
    /// Retrieves a restuarant record based on the matching id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Restuarant record if found.  Returns new Restuarant if not found</returns>
    public Restuarant GetRestuarantByStatement(string id)
    {
        string query = @"SELECT
			        r.Id,
                    r.Name,
                    r.CuisineType,
                    r.Website,
                    r.Phone,
                    rl.Street,
                    rl.City,
                    rl.[State],
                    rl.ZipCode,
                    rl.Country
		        FROM [dbo].[Restuarants] r
			        INNER JOIN [dbo].[RestuarantLocation] rl
				        ON rl.RestuarantId = r.Id
                WHERE r.Id = @Id";

        SqlParameter[] parameters =
        [
            new("@Id", id)
        ];
        DataRow row = sqlHelper.SelectOne(query, parameters);

        bool hasData = sqlHelper.DateRowHasData(row);

        // if there is no data then return an empty list
        if (!hasData)
        {
            return new Restuarant();
        }

        return RestuarantMapper.MapRestuarantAndLocation(row);
    }

    /// <summary>
    /// Inserts a new Restuarant Record with Location
    /// </summary>
    /// <param name="rest">Restuarant object with data to add</param>
    /// <returns>id of the newly inserted restuarant</returns>
    public int InsertRestuarant(Restuarant rest)
    {
        logger.LogInformation("Adding new restuarant");

        SqlParameter[] parameters =
        [
            new("@Name", rest.Name),
            new("@Cuisine", rest.CuisineType),
            new("@Website", rest.Website is null ? DBNull.Value : rest.Website.ToString()),
            new("@Phone", rest.Phone),
            new("@Street", rest.Address.Street),
            new("@City", rest.Address.City),
            new("@State", rest.Address.State),
            new("@ZipCode", rest.Address.ZipCode),
            new("@Country", rest.Address.Country)
        ];
        int newRestuarantId = sqlHelper.InsertOne(DataAccessConstants.DefaultSchema, DataAccessConstants.InsertRestuarant, parameters);

        return newRestuarantId;
    }

    /// <summary>
    /// Inserts many new Restuarant Records
    /// </summary>
    /// <param name="restuarants">Array of new restuarant objects to add</param>
    /// <returns>Restuarant objects updated with the new id</returns>
    public Restuarant[] InsertRestuarants(Restuarant[] restuarants)
    {
        logger.LogInformation("Adding new restuarants");

        DataTable table = RestuarantMapper.MapRestuarantsToDataTable(restuarants);

        SqlParameter[] parameters =
        [
            new()
            {
                ParameterName = "@NewRestuarants",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Structured,
                TypeName = DataAccessConstants.RestuarantType,
                Value = table
            }
        ];

        /*
         * For a simple bulk insert operation, the return would not be needed.
         * But, if needing the ids of the newly created objects, then there might need to be some creative
         * solutions to get those values.  The stored procedure in this case does that, but there might be a better way than what is demonstrated here
         */
        DataTable results = sqlHelper.Select(DataAccessConstants.DefaultSchema, DataAccessConstants.GetAndInsertRestuarants, parameters);

        return RestuarantMapper.MapRestuarants(results);
    }

    /// <summary>
    /// Inserts many new Restuarant Address Records
    /// </summary>
    /// <param name="restuarants">Array of new restuarant objects to add</param>
    /// <returns>Number of address records inserted</returns>
    public int InsertRestuarantAddresses(Restuarant[] restuarants)
    {
        logger.LogInformation("Adding new restuarants");

        DataTable table = RestuarantMapper.MapRestuarantAddressesToDataTable(restuarants);

        SqlParameter[] parameters =
        [
            new()
            {
                ParameterName = "@NewAddresses",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Structured,
                TypeName = DataAccessConstants.RestuarantLocationType,
                Value = table
            }
        ];

        /*
         * For a simple bulk insert operation, the return would not be needed.
         * But, if needing the ids of the newly created objects, then there might need to be some creative
         * solutions to get those values.  The stored procedure in this case does that, but there might be a better way than what is demonstrated here
         */
        return sqlHelper.Insert(DataAccessConstants.DefaultSchema, DataAccessConstants.InsertRestuarantAddresses, parameters);
    }

    /// <summary>
    /// Updates and existing restuarant record
    /// </summary>
    /// <param name="rest"></param>
    /// <returns>int - number of rows affected</returns>
    public int UpdateRestuarant(Restuarant rest)
    {
        logger.LogInformation("replacing restuarant document");
        SqlParameter[] parameters =
        [
            new("@Id", rest.Id),
            new("@Name", rest.Name),
            new("@Cuisine", rest.CuisineType),
            new("@Website", rest.Website is null ? DBNull.Value : rest.Website.ToString()),
            new("@Phone", rest.Phone),
            new("@Street", rest.Address.Street),
            new("@City", rest.Address.City),
            new("@State", rest.Address.State),
            new("@ZipCode", rest.Address.ZipCode),
            new("@Country", rest.Address.Country),
        ];
        return sqlHelper.Update(DataAccessConstants.DefaultSchema, DataAccessConstants.UpdateRestuarant, parameters);
    }
}
