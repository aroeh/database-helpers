using MsSqlServerHelpers.Constants;
using MsSqlServerHelpers.Models;
using System.Data;
using System.Data.SqlClient;

namespace MsSqlServerHelpers.DataAccess
{
    // class setup using a primary constructor
    public class RestuarantData(ILogger<RestuarantData> log, ISqlHelperService sql) : IRestuarantData
    {
        private readonly ILogger<RestuarantData> logger = log;
        private readonly ISqlHelperService sqlHelper = sql;


        /// <summary>
        /// Returns a list of all restuarants in the database
        /// </summary>
        /// <returns>Collection of available restuarant records.  Returns empty list if there are no records</returns>
        public List<Restuarant> GetAllRestuarants()
        {
            DataTable table = sqlHelper.Select(DataAccessConstants.DefaultSchema, DataAccessConstants.GetAllRestuarants);

            bool hasData = sqlHelper.DateTableHasData(table);

            // if there is no data then return an empty list
            if (!hasData)
            {
                return [];
            }

            // A Better practice would be to use a mapper or move this into a mapper class to centralize the code
            return table.AsEnumerable().Select(row =>
                new Restuarant
                {
                    Id = row.Field<int>("Id").ToString(),
                    Name = row.Field<string>("Name"),
                    CuisineType = row.Field<string>("CuisineType"),
                    Website = string.IsNullOrWhiteSpace(row.Field<string>("Website")) ? null : new Uri(row.Field<string>("Website")),
                    Phone = row.Field<string>("Phone"),
                    Address = new Location
                    {
                        Street = row.Field<string>("Street"),
                        City = row.Field<string>("City"),
                        State = row.Field<string>("State"),
                        ZipCode = row.Field<string>("ZipCode"),
                        Country = row.Field<string>("Country")
                    }
                }).ToList();
        }

        /// <summary>
        /// This is an example showing how to write SQL and execute it
        /// Using the stored proc command is the preferred method for security and overall performance
        /// But this method may offer more flexibility if changing the database is difficult
        /// </summary>
        /// <returns>Collection of available restuarant records.  Returns empty list if there are no records</returns>
        public List<Restuarant> GetAllRestuarantsByStatement()
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

            // A Better practice would be to use a mapper or move this into a mapper class to centralize the code
            return table.AsEnumerable().Select(row =>
                new Restuarant
                {
                    Id = row.Field<int>("Id").ToString(),
                    Name = row.Field<string>("Name"),
                    CuisineType = row.Field<string>("CuisineType"),
                    Website = string.IsNullOrWhiteSpace(row.Field<string>("Website")) ? null : new Uri(row.Field<string>("Website")),
                    Phone = row.Field<string>("Phone"),
                    Address = new Location
                    {
                        Street = row.Field<string>("Street"),
                        City = row.Field<string>("City"),
                        State = row.Field<string>("State"),
                        ZipCode = row.Field<string>("ZipCode"),
                        Country = row.Field<string>("Country")
                    }
                }).ToList();
        }

        /// <summary>
        /// Simple method for finding restuarants by name and type of cuisine.
        /// This could be enhanced to include more criteria like location
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cuisine"></param>
        /// <returns>Collection of available restuarant records.  Returns empty list if there are no records found matching criteria</returns>
        public List<Restuarant> FindRestuarants(string name, string cuisine)
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

            // A Better practice would be to use a mapper or move this into a mapper class to centralize the code
            return table.AsEnumerable().Select(row =>
                new Restuarant
                {
                    Id = row.Field<int>("Id").ToString(),
                    Name = row.Field<string>("Name"),
                    CuisineType = row.Field<string>("CuisineType"),
                    Website = string.IsNullOrWhiteSpace(row.Field<string>("Website")) ? null : new Uri(row.Field<string>("Website")),
                    Phone = row.Field<string>("Phone"),
                    Address = new Location
                    {
                        Street = row.Field<string>("Street"),
                        City = row.Field<string>("City"),
                        State = row.Field<string>("State"),
                        ZipCode = row.Field<string>("ZipCode"),
                        Country = row.Field<string>("Country")
                    }
                }).ToList();
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
        /// <returns>Collection of available restuarant records.  Returns empty list if there are no records found matching criteria</returns>
        public List<Restuarant> FindRestuarantsByStatement(string name, string cuisine)
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

            // A Better practice would be to use a mapper or move this into a mapper class to centralize the code
            return table.AsEnumerable().Select(row =>
                new Restuarant
                {
                    Id = row.Field<int>("Id").ToString(),
                    Name = row.Field<string>("Name"),
                    CuisineType = row.Field<string>("CuisineType"),
                    Website = string.IsNullOrWhiteSpace(row.Field<string>("Website")) ? null : new Uri(row.Field<string>("Website")),
                    Phone = row.Field<string>("Phone"),
                    Address = new Location
                    {
                        Street = row.Field<string>("Street"),
                        City = row.Field<string>("City"),
                        State = row.Field<string>("State"),
                        ZipCode = row.Field<string>("ZipCode"),
                        Country = row.Field<string>("Country")
                    }
                }).ToList();
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
            DataRow row = sqlHelper.SelectSingle(DataAccessConstants.DefaultSchema, DataAccessConstants.GetRestuarantById, parameters);

            bool hasData = sqlHelper.DateRowHasData(row);

            // if there is no data then return an empty list
            if (!hasData)
            {
                return new Restuarant();
            }

            // A Better practice would be to use a mapper or move this into a mapper class to centralize the code
            return new Restuarant
            {
                Id = row.Field<int>("Id").ToString(),
                Name = row.Field<string>("Name"),
                CuisineType = row.Field<string>("CuisineType"),
                Website = string.IsNullOrWhiteSpace(row.Field<string>("Website")) ? null : new Uri(row.Field<string>("Website")),
                Phone = row.Field<string>("Phone"),
                Address = new Location
                {
                    Street = row.Field<string>("Street"),
                    City = row.Field<string>("City"),
                    State = row.Field<string>("State"),
                    ZipCode = row.Field<string>("ZipCode"),
                    Country = row.Field<string>("Country")
                }
            };
        }

        /// <summary>
        /// Inserts a new Restuarant Record
        /// </summary>
        /// <param name="rest"></param>
        /// <returns>Restuarant object updated with the new id</returns>
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
                new("@Country", rest.Address.Country),
            ];
            int newRestuarantId = sqlHelper.Insert(DataAccessConstants.DefaultSchema, DataAccessConstants.InsertRestuarant, parameters);

            return newRestuarantId;
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
}
