using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CosmosDbNoSqlHelpers.Models
{
    /// <summary>
    /// Here we've got a sample model for outlining basic information tracking a restuarant
    /// the model could be made more complex, but for demonstration purposes, we're keeping it simple
    /// </summary>
    public class Restuarant
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string CuisineType { get; set; } = string.Empty;

        public Uri? Website { get; set; }

        [Phone]
        public string Phone { get; set; } = string.Empty;

        public Location Address { get; set; } = new Location();
    }
}
