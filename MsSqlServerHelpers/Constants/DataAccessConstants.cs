using System.Data;

namespace MsSqlServerHelpers.Constants;

public static class DataAccessConstants
{
    public const string DefaultSchema = "dbo";
    public const string MsSqlConn = "MsSqlConn";

    #region Stored Procedure Names

    public const string GetAllRestuarants = "sp_GetAllRestuarants";
    public const string FindRestuarants = "sp_FindRestuarants";
    public const string GetRestuarantById = "sp_GetRestuarantById";
    public const string InsertRestuarant = "sp_InsertRestuarant";
    public const string InsertRestuarants = "sp_InsertRestuarants";
    public const string GetAndInsertRestuarants = "sp_GetAndInsertRestuarants";
    public const string InsertRestuarantAddresses = "sp_InsertRestuarantAddresses";
    public const string UpdateRestuarant = "sp_UpdateRestuarant";

    #endregion Stored Procedure Names

    #region Table Types

    public const string IntCollection = "[dbo].[IntCollection]";
    public const string RestuarantType = "[dbo].[RestuarantType]";
    public const string RestuarantLocationType = "[dbo].[RestuarantLocationType]";

    #endregion Table Types

    #region Data Tables

    public static DataTable IntCollectionTable()
    {
        DataTable table = new();
        table.Columns.Add("Id");

        return table;
    }

    public static DataTable RestuarantTypeTable()
    {
        DataTable table = new();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("CuisineType", typeof(string));
        table.Columns.Add("Website", typeof(string));
        table.Columns.Add("Phone", typeof(string));

        return table;
    }

    public static DataTable RestuarantLocationTypeTable()
    {
        DataTable table = new();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("RestuarantId", typeof(int));
        table.Columns.Add("Street", typeof(string));
        table.Columns.Add("City", typeof(string));
        table.Columns.Add("State", typeof(string));
        table.Columns.Add("Country", typeof(string));
        table.Columns.Add("ZipCode", typeof(string));

        return table;
    }

    #endregion Data Tables
}
