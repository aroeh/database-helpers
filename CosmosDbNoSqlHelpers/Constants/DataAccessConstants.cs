namespace CosmosDbNoSqlHelpers.Constants;

public static class DataAccessConstants
{
    // endpoint of the CosmosDb service to connect to
    public const string CosmosEndpointUri = "CosmosEndpointUri";

    // Primary Key of the CosmosDb instance
    public const string CosmosPrimaryKey = "CosmosPrimaryKey";

    // CosmosDb Database Name to connect to
    public const string DatabaseName = "samples";

    // Name of the container with items and data
    public const string ContainerName = "restuarants";

    public const string PartitionKey = "/CuisineType";
}
