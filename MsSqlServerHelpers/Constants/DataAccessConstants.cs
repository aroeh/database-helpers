using System.Data;

namespace MsSqlServerHelpers.Constants
{
    public static class DataAccessConstants
    {
        public const string DefaultSchema = "dbo";
        public const string MsSqlConn = "MsSqlConn";

        #region Stored Procedure Names

        public const string GetAllRestuarants = "sp_GetAllRestuarants";
        public const string FindRestuarants = "sp_FindRestuarants";
        public const string GetRestuarantById = "sp_GetRestuarantById";
        public const string InsertRestuarant = "sp_InsertRestuarant";
        public const string UpdateRestuarant = "sp_UpdateRestuarant";

        #endregion Stored Procedure Names

        #region Table Types

        public const string IntCollection = "dbo.IntCollection";

        #endregion Table Types

        #region Data Tables

        public static DataTable IntCollectionTable()
        {
            var table = new DataTable();
            table.Columns.Add("Id");

            return table;
        }

        #endregion Data Tables
    }
}
